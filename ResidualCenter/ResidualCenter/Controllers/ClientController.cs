using ResidualCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace ResidualCenter.Controllers
{
    public class ClientController : Controller
    {
        ResidualCenterContext residual = new ResidualCenterContext();
        /// <summary>
        /// Application DB context
        /// </summary>
        protected ApplicationDbContext ApplicationDbContext { get; set; }

        /// <summary>
        /// User manager - attached to application DB context
        /// </summary>
        protected UserManager<ApplicationUser> UserManager { get; set; }
        public ClientController()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: Client/Details/5
        public ActionResult ListServices()
        {
            return View();
        }


        // GET: Client/Details/5
        public ActionResult RequestServices()
        {


            ViewBag.LocationID = new SelectList(residual.Locations, "ID", "Name");
            ViewBag.ServicesTypesID = new SelectList(residual.ServicesTypes, "ID", "Name");
            ViewBag.ResidueTypeID = new SelectList(residual.ResidueTypes, "ID", "Name");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RequestServices(ClientViewModel.RequestService model)
        {
            var userID = User.Identity.GetUserId();
            Entity entity = residual.Entities.Where(user => user.UserId == userID).FirstOrDefault();
            Location location = residual.Locations.Find(model.LocationID);
            ServiceRequest rs = new ServiceRequest()
            {
                RequestDate = model.ServiceDate,
                ResidueTypeID = model.ResidueTypeID,
                ServiceTypeID = model.ServicesTypesID,
                Adress=model.Adress,
                Description = model.Description,
                Location = location.Name,
                ServiceRequestStatusID = 1,

            };
          
            entity.ServiceRequest.Add(rs);
      
            residual.SaveChanges();
            return View("Index");
        }

        // GET: Client/Details/5
        public ActionResult ListRequestedServices()
        {

            var userID = User.Identity.GetUserId();
            Entity entity = residual.Entities.Where(user => user.UserId == userID).FirstOrDefault();
            var requestList = entity.ServiceRequest;
            //foreach (var item in requestList)
            //{

            //    ViewData.Add(new KeyValuePair<string, object>(item.ID.ToString(), item.ServiceRequestStatus.Name));
            //}

            return View(requestList);
        }
        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Client/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }
}
