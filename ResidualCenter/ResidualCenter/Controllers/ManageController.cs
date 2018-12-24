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



        public ActionResult ListRole()
        {

            return View(context.Roles.ToList());
        }
        public ActionResult CreateRole()
        {
            var Role = new IdentityRole();
            return View(Role);
        }
        [HttpPost]
        public ActionResult CreateRole(IdentityRole Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("ListRole");
        }



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
        public ActionResult DeleteConfirmed(string id)
        {
            var role = context.Roles.Find(id);
            context.Roles.Remove(role);
            context.SaveChanges();
            return RedirectToAction("ListRole");
        }

        // GET: Entities
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

        // GET: Entities/Details/5
        public ActionResult DetailsEntity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entity entity = residual.Entities.Find(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }



        // GET: Manage/Delete/5
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
        public ActionResult DeleteConfirmedEntity(string id)
        {

            //   ApplicationDbContext context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = userManager.FindById(id);
            userManager.Delete(user);

            return View();
        }

        [AllowAnonymous]
        public ActionResult RegisterEmployee()
        {

            ViewBag.LocationID = new SelectList(residual.Locations, "ID", "Name");
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
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

        // GET: Manage/Equipments
        public ActionResult ListEquipments()
        {
            var equipments = residual.Equipments.Include(e => e.EquipmentState).Include(e => e.EquipmentType).Include(e => e.ServiceType);
            return View(equipments.ToList());
        }


        // GET: Manage/Create
        public ActionResult AddEquipment()
        {
            ViewBag.EquipmentStateID = new SelectList(residual.EquipmentStates, "ID", "Name");
            ViewBag.EquipmentTypeID = new SelectList(residual.EquipmentTypes, "ID", "Name");
            ViewBag.ServiceTypeID = new SelectList(residual.ServicesTypes, "ID", "Name");
            return View();
        }


        [HttpPost]
        public ActionResult AddEquipment(AddEquipmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < model.Quantity; i++)
                {
                    Equipment equipment = new Equipment
                    {
                        EquipmentTypeID = model.EquipmentTypeID,
                        EquipmentStateID = model.EquipmentStateID,
                        ServiceTypeID = model.ServiceTypeID,
                        Name = model.Name,


                    };
                    residual.Equipments.Add(equipment);
                }



                residual.SaveChanges();


                return RedirectToAction("ListEquipments", "Manage");

            }
            return RedirectToAction("Index");
        }
        // POST: Equipment
        public ActionResult AddEquipmentType()
        {
            var equipmentType = new EquipmentType();
            return View(equipmentType);
        }
        [HttpPost]
        public ActionResult AddEquipmentType(EquipmentType equipmentType)
        {

            residual.EquipmentTypes.Add(equipmentType);
            residual.SaveChanges();
            return RedirectToAction("ListEquipments", "Manage");

        } // POST: Equipment
        public ActionResult AddEquipmentState()
        {
            var equipmentState = new EquipmentState();
            return View(equipmentState);
        }
        [HttpPost]
        public ActionResult AddEquipmentState(EquipmentState equipmentState)
        {

            residual.EquipmentStates.Add(equipmentState);
            residual.SaveChanges();
            return RedirectToAction("ListEquipments", "Manage");

        }

        // GET: Equipments/Delete/5
        public ActionResult DeleteEquipment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = residual.Equipments.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // POST: Equipments/Delete/5
        [HttpPost, ActionName("DeleteEquipment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedEquipment(int id)
        {
            Equipment equipment = residual.Equipments.Find(id);
            residual.Equipments.Remove(equipment);
            residual.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Equipments/Edit/5
        public ActionResult EditEquipment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = residual.Equipments.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            ViewBag.EquipmentStateID = new SelectList(residual.EquipmentStates, "ID", "Name", equipment.EquipmentStateID);
            ViewBag.EquipmentTypeID = new SelectList(residual.EquipmentTypes, "ID", "Name", equipment.EquipmentTypeID);
            ViewBag.ServiceTypeID = new SelectList(residual.ServicesTypes, "ID", "Name", equipment.ServiceTypeID);
            return View(equipment);
        }

        // POST: Equipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEquipment([Bind(Include = "ID,EquipmentTypeID,EquipmentStateID,Name")] Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                residual.Entry(equipment).State = EntityState.Modified;
                residual.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EquipmentStateID = new SelectList(residual.EquipmentStates, "ID", "Name", equipment.EquipmentStateID);
            ViewBag.EquipmentTypeID = new SelectList(residual.EquipmentTypes, "ID", "Name", equipment.EquipmentTypeID);
            return View(equipment);
        }
        //SERVICE TYPE STUFF
        public ActionResult ListServiceType()
        {
            return View(residual.ServicesTypes.ToList());
        }

        // GET: Manage/Create
        public ActionResult CreateServiceType()
        {
            return View();
        }
        // POST: Manage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateServiceType([Bind(Include = "ID,Name")] ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
                residual.ServicesTypes.Add(serviceType);
                residual.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serviceType);
        }
        // GET: Manage/Edit/
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
        public ActionResult EditServiceType([Bind(Include = "ID,Name")] ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
                residual.Entry(serviceType).State = EntityState.Modified;
                residual.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serviceType);
        }

        // GET: Manage/Delete/5
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
        public ActionResult DeleteConfirmedServiceType(int id)
        {
            ServiceType serviceType = residual.ServicesTypes.Find(id);
            residual.ServicesTypes.Remove(serviceType);
            residual.SaveChanges();
            return RedirectToAction("Index");
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