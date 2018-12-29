using ResidualCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResidualCenter.Controllers
{
    public class ClientController : Controller
    {
        ResidualCenterContext residual = new ResidualCenterContext();
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }

        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

<<<<<<< HEAD
<<<<<<< HEAD
        // GET: Client/Details/5
        public ActionResult ListServices()
=======
        // GET: Client/Create
        public ActionResult Create()
>>>>>>> parent of 68761b7... oncemore
        {
            return View();
        }

<<<<<<< HEAD
        
        // GET: Client/Details/5
        public ActionResult RequestServices()
=======
        // GET: Client/Create
        public ActionResult Create()
>>>>>>> parent of 68761b7... oncemore
        {
            ViewBag.LocationID = new SelectList(residual.Locations, "ID", "Name");
            ViewBag.ServiceID = new SelectList(residual.Services, "ID", "Name");
            ViewBag.ResidueTypeID = new SelectList(residual.ResidueTypes, "ID", "Name");
            return View();
        }

<<<<<<< HEAD
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RequestServices(ClientViewModel.RequestService model)
        {
            var hhhhyu = model.ServiceDate;
            return View();
        }
        
=======
       

>>>>>>> parent of 68761b7... oncemore
=======
       

>>>>>>> parent of 68761b7... oncemore
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
