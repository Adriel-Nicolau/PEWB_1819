using Microsoft.AspNet.Identity.EntityFramework;
using ResidualCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResidualCenter.Controllers
{
    public class AdminController : Controller
    {

        ApplicationDbContext context;
        public AdminController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        
          
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
        
    }

}