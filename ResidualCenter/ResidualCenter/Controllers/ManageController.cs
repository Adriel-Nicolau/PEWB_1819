using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ResidualCenter.Models;

using System.Data.Entity;

using System.Net;
using System.Collections.Generic;

namespace ResidualCenter.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        ApplicationDbContext context;
        ResidualCenterContext residual;


        public ManageController()
        {
            context = new ApplicationDbContext();
            residual = new ResidualCenterContext();
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        [Authorize(Roles = "Admin")]
        public ActionResult Index(ManageMessageId? message)
        {
            //ViewBag.StatusMessage =
            //    message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
            //    : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
            //    : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
            //    : message == ManageMessageId.Error ? "An error has occurred."
            //    : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
            //    : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
            //    : "";

            //var userId = User.Identity.GetUserId();
            //var model = new IndexViewModel
            //{
            //    HasPassword = HasPassword(),
            //    PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
            //    TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
            //    Logins = await UserManager.GetLoginsAsync(userId),
            //    BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            //};
            return View();
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }
        /// <AdminStuff>
        /// 
        /// </summary>


        [Authorize(Roles = "Admin")]
        public ActionResult ListRole()
        {

            return View(context.Roles.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateRole()
        {
            var Role = new IdentityRole();
            return View(Role);
        }
        [HttpPost]

        [Authorize(Roles = "Admin")]
        public ActionResult CreateRole(IdentityRole Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("ListRole");
        }



        [Authorize(Roles = "Admin")]
        public ActionResult DeleteRole(string id)
        {
            if (id == null)
            {
                //return 
            }
            var role = context.Roles.Find(id);
            return View(role);


        }

        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(string id)
        {
            var role = context.Roles.Find(id);
            context.Roles.Remove(role);
            context.SaveChanges();
            return RedirectToAction("ListRole");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ListEntities()
        {

            //  ApplicationDbContext context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            var entities = residual.Entities.Include(e => e.Location);
            foreach (var item in entities)
            {
                var role = userManager.GetRoles(item.UserId).FirstOrDefault();
                ViewData.Add(new KeyValuePair<string, object>(item.Email, role));
            }

            return View(entities.ToList());
        }



        // GET: Manage/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteEntity(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Entity entity = residual.Entities.Where(x => x.UserId == id).FirstOrDefault();
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }


        // POST: Entities/Delete/
        [HttpPost, ActionName("DeleteEntity")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmedEntity(string id)
        {

            //   ApplicationDbContext context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = userManager.FindById(id);
            userManager.Delete(user);

            return View();
        }


        [Authorize(Roles = "Admin")]
        public ActionResult RegisterEmployee()
        {

            ViewBag.LocationID = new SelectList(residual.Locations, "ID", "Name");
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> RegisterEmployee(RegisterEmployeeViewModel model)
        {

            if (ModelState.IsValid)
            {
                var userAsp = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(userAsp, model.Password);
                if (result.Succeeded)
                {


                    await UserManager.AddToRoleAsync(userAsp.Id, "Employee");
                    Entity entity = new Entity
                    {
                        Email = model.Email,
                        UserId = userAsp.Id,
                        Name = model.Name,
                        LocationID = model.LocationID,
                        BirthDate = model.BirthDate,
                        Adress = model.Adress,
                        Contact = model.Contact,
                        Gender = model.Gender

                    };

                    residual.Entities.Add(entity);
                    residual.SaveChanges();

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("ListEntities", "Manage");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //SERVICE TYPE STUFF
        [Authorize(Roles = "Admin")]
        public ActionResult ListServiceType()
        {
            return View(residual.ServicesTypes.ToList());
        }

        // GET: Manage/Create
        [Authorize(Roles = "Admin")]
        public ActionResult CreateServiceType()
        {
            return View();
        }
        // POST: Manage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateServiceType([Bind(Include = "ID,Name")] ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
                residual.ServicesTypes.Add(serviceType);
                residual.SaveChanges();
                return RedirectToAction("ListServiceType");
            }

            return View(serviceType);
        }
        // GET: Manage/Edit/
        [Authorize(Roles = "Admin")]
        public ActionResult EditServiceType(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceType serviceType = residual.ServicesTypes.Find(id);
            if (serviceType == null)
            {
                return HttpNotFound();
            }
            return View(serviceType);
        }
        // POST: Manage/Edit/
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult EditServiceType([Bind(Include = "ID,Name")] ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
                residual.Entry(serviceType).State = EntityState.Modified;
                residual.SaveChanges();
                return RedirectToAction("ListServiceType");
            }
            return View(serviceType);
        }

        // GET: Manage/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteServiceType(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceType serviceType = residual.ServicesTypes.Find(id);
            if (serviceType == null)
            {
                return HttpNotFound();
            }
            return View(serviceType);
        }

        // POST: Manage/Delete/5
        [HttpPost, ActionName("DeleteServiceType")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmedServiceType(int id)
        {
            ServiceType serviceType = residual.ServicesTypes.Find(id);
            residual.ServicesTypes.Remove(serviceType);
            residual.SaveChanges();
            return RedirectToAction("ListServiceType");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ListServicesRequest()
        {


            //  ViewBag.EmployeeList = CreateEmployeeList();
            ViewBag.ServiceRequestStatusID = new SelectList(residual.ServiceRequestStatus, "ID", "Name");
            return View(residual.ServiceRequests.ToList());

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult ListServicesRequest(ChangeStatusServiceResquestViewModel changes)
        {

            ServiceRequest sr = residual.ServiceRequests.Find(changes.ID);
            sr.ServiceRequestStatusID = changes.ServiceRequestStatusID;
            residual.Entry(sr).State = EntityState.Modified;
            residual.SaveChanges();


            return RedirectToAction("ListServicesRequest");
        }
        // GET: ResidueTypes
        [Authorize(Roles = "Admin")]
        public ActionResult ListResidues()
        {
            return View(residual.ResidueTypes.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateResidue()
        {
            return View();
        }

        // POST: ResidueTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateResidue([Bind(Include = "ID,Name,Unit")] ResidueType residueType)
        {
            if (ModelState.IsValid)
            {
                residual.ResidueTypes.Add(residueType);
                residual.SaveChanges();
                return RedirectToAction("ListResidues");
            }

            return View(residueType);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditResidue(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResidueType residueType = residual.ResidueTypes.Find(id);
            if (residueType == null)
            {
                return HttpNotFound();
            }
            return View(residueType);
        }

        // POST: ResidueTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult EditResidue([Bind(Include = "ID,Name,Unit")] ResidueType residueType)
        {
            if (ModelState.IsValid)
            {
                residual.Entry(residueType).State = EntityState.Modified;
                residual.SaveChanges();
                return RedirectToAction("ListResidues");
            }
            return View(residueType);
        }

        // GET: ResidueTypes/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteResidue(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResidueType residueType = residual.ResidueTypes.Find(id);
            if (residueType == null)
            {
                return HttpNotFound();
            }
            return View(residueType);
        }

        // POST: ResidueTypes/Delete/5

        [HttpPost, ActionName("DeleteResidue")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            ResidueType residueType = residual.ResidueTypes.Find(id);
            residual.ResidueTypes.Remove(residueType);
            residual.SaveChanges();
            return RedirectToAction("ListResidues");
        }
        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}