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
using ResidualCenter.Models.ViewModels;
using System.Net;
using System.Data.Entity;
using System.Web.Routing;

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

        [Authorize(Roles = "Client")]
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();
            Entity entity = residual.Entities.Where(user => user.UserId == userID).FirstOrDefault();
            @ViewBag.userName = entity.Name;
            return View();
        }


        // GET: Client/Details/5
        [Authorize(Roles = "Client")]
        public ActionResult RequestServices()
        {
            ViewBag.LocationID = new SelectList(residual.Locations, "ID", "Name");
            ViewBag.ServicesTypesID = new SelectList(residual.ServicesTypes, "ID", "Name");
            ViewBag.ResidueTypeID = new SelectList(residual.ResidueTypes, "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client")]
        public ActionResult RequestServices(ClientViewModel.RequestService model)
        {

            if (ModelState.IsValid)
            {
                DateTime today = DateTime.Today;
                if (model.ServiceDate <= today)
                {
                    ViewBag.error = 1;
                    ModelState.AddModelError("CustomError", "A data do Serviço tem de ser superior a " + today.ToShortDateString());
                    ViewBag.LocationID = new SelectList(residual.Locations, "ID", "Name");
                    ViewBag.ServicesTypesID = new SelectList(residual.ServicesTypes, "ID", "Name");
                    ViewBag.ResidueTypeID = new SelectList(residual.ResidueTypes, "ID", "Name");
                    return View();

                }


                var userID = User.Identity.GetUserId();
                Entity entity = residual.Entities.Where(user => user.UserId == userID).FirstOrDefault();
                Location location = residual.Locations.Find(model.LocationID);
                ServiceRequest rs = new ServiceRequest()
                {
                    RequestDate = model.ServiceDate,
                    ResidueTypeID = model.ResidueTypeID,
                    ServiceTypeID = model.ServicesTypesID,
                    Adress = model.Adress,
                    Description = model.Description,
                    Location = location.Name,
                    ServiceRequestStatusID = 1,

                };

                entity.ServiceRequest.Add(rs);

                residual.SaveChanges();
                return View("Index");
            }

            return View(model);
        }

        // GET: Client/Details/5
        [Authorize(Roles = "Client")]
        public ActionResult ListRequestedServices()
        {

            var userID = User.Identity.GetUserId();
            Entity entity = residual.Entities.Where(user => user.UserId == userID).FirstOrDefault();
            var requestList = entity.ServiceRequest;

            return View(requestList);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client")]
        public ActionResult CancelRequest(int? id)
        {
            if (id != null)
            {
                ServiceRequest sr = residual.ServiceRequests.Find(id);
                residual.ServiceRequests.Remove(sr);
                residual.SaveChanges();
                return RedirectToAction("ListRequestedServices");
            }


            return View();
        }
        // GET: Client/Details/5
        [Authorize(Roles = "Client")]
        public ActionResult CreateReview(int? id)
        {

            if (id != null)
            {
                Review rw = residual.Reviews.Where(r => r.ServiceRequestID == id).FirstOrDefault();
                if (rw != null)
                {

                    return RedirectToAction("EditReview", new RouteValueDictionary(    new { controller = "Client", action = "EditReview", Id = rw.ID }));
                }
                ReviewViewModel.CreateReview review = new ReviewViewModel.CreateReview();
                var userID = User.Identity.GetUserId();
                Entity entity = residual.Entities.Where(user => user.UserId == userID).FirstOrDefault();
                review.EntityID = entity.ID;
                review.ServiceRequestID = (int)id;
                return View(review);

            }
            else
            {
                return RedirectToAction("ListRequestedServices");
            }
        }


        [HttpPost, ActionName("CreateReview")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client")]
        public ActionResult ConfirmCreateReview(ReviewViewModel.CreateReview review)
        {
            if (ModelState.IsValid)
            {
                Review r = new Review()
                {
                    Content = review.Content,
                    Rating = review.Rating,
                    ServiceRequestID = review.ServiceRequestID,
                    EntityID = review.EntityID,
                    CreationDate = DateTime.Today,

                };
                residual.Reviews.Add(r);

                residual.SaveChanges();
                return RedirectToAction("ListRequestedServices");
            }
            return View(review);

        }


        [Authorize(Roles = "Client")]
        public ActionResult ListReviews()
        {
            var userID = User.Identity.GetUserId();
            Entity entity = residual.Entities.Where(user => user.UserId == userID).FirstOrDefault();
            var reviewList = residual.Reviews.Where(x => x.EntityID == entity.ID);


            return View();

        }






        public ActionResult EditReview(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = residual.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }

            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditReview([Bind(Include = "ID,EntityID,ServiceRequestID,Content,CreationDate,Rating")] Review review)
        {
            if (ModelState.IsValid)
            {
                residual.Entry(review).State = EntityState.Modified;
                residual.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(review);
        }

    }
}
