﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ResidualCenter.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResidualCenter.Controllers
{
    public class EmployeeController : Controller
    {

        private const string APPROVED = "Aceite";
        private const string DONE = "Finalizado";
        private const int PROGRESS = 5;

        ResidualCenterContext residual = new ResidualCenterContext();
        /// <summary>
        /// Application DB context
        /// </summary>
        protected ApplicationDbContext ApplicationDbContext { get; set; }

        /// <summary>
        /// User manager - attached to application DB context
        /// </summary>
        protected UserManager<ApplicationUser> UserManager { get; set; }
        public EmployeeController()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }
        // GET: Employee
        public ActionResult Index()
        {
            return RedirectToAction("ListServices");
        }



        // GET: Client/Details/5
        public ActionResult ListServices()
        {



            var requestList = residual.ServiceRequests.Where(x => x.ServiceRequestStatus.Name.Equals(APPROVED) || x.ServiceRequestStatus.Name.Equals(DONE)).ToList();
            //foreach (var item in requestList)
            //{

            //    ViewData.Add(new KeyValuePair<string, object>(item.ID.ToString(), item.ServiceRequestStatus.Name));
            //}

            return View(requestList);
        }

        //public ActionResult GetService(int? id)
        //{
        //    ServiceRequest service = residual.ServiceRequests.Find(id);
        //    return View(service);
        //}
        // @Html.ValidationSummary(true)  
        
        [AllowAnonymous]
       
        public ActionResult GetService(int? id)
        {
            int count = 0;
            if (id != null)
            {
                var userID = User.Identity.GetUserId();
                Entity entity = residual.Entities.Where(user => user.UserId == userID).FirstOrDefault();
                var serviceList = entity.ServiceRequest;
                ServiceRequest service = residual.ServiceRequests.Find(id);
                foreach (var item in serviceList)
                {
                    if (service.RequestDate == item.RequestDate)
                    {
                        count++;
                    }
                }

                if (count == 0)
                {

                    ModelState.AddModelError("", "Limite maximo de Serviços para o dia " + service.RequestDate.ToShortDateString());
                    var requestList = residual.ServiceRequests.Where(x => x.ServiceRequestStatus.Name.Equals(APPROVED) || x.ServiceRequestStatus.Name.Equals(DONE)).ToList();
                    return View(requestList);
                }
                else
                {
                    entity.ServiceRequest.Add(service);
                    residual.SaveChanges();


                    service.ServiceRequestStatusID = PROGRESS;
                    residual.Entry(service).State = EntityState.Modified;
                    residual.SaveChanges();
                    return RedirectToAction("ListServices");


                }
            }
            return RedirectToAction("ListServices");
        }

    }
}