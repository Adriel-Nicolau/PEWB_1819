using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using ResidualCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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

        //AQUI recebo um id e encontro a role que pertence a role usando Roles.* pode ter o mesmo efeito
        //       public ActionResult EditRole(int? id)
        //       {

        //           var roles = context.Roles.ToList();
        //           roles.ForEach((x) =>
        //              {
        //                  if (x.Id.Any())
        //                  {
        //                      if (x.Id.Equals(id))
        //                      {
        //                          var role = roles.FirstOrDefault(r => r.Id == x.Id);
        //                          if (role != null)
        //                              ViewBag.role = role;


        //                      }


        //                  }
        //              });
        //return View();

        //       }

        public ActionResult DeleteRole(string id)
        {
            if (id == null)
            {
                //return 
            }
            var role = context.Roles.Find(id);
            return View(role);
           

        }
      //  [HttpPost/*, ActionName("DeleteRole")*/]
      //  //[ValidateAntiForgeryToken]
      ////  [Authorize(Roles = "Pais, Administrador")]
      //  public ActionResult DeleteRole(IdentityRole Role)
      //  {
      //      context.Roles.Remove(Role);
      //      context.SaveChanges();
      //      return RedirectToAction("ListRole");
      //  }
        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var role = context.Roles.Find(id);
            context.Roles.Remove(role);
            context.SaveChanges();
            return RedirectToAction("ListRole");
        }
    }



}