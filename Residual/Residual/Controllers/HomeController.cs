using Residual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Residual.Controllers
{
    public class HomeController : Controller
    {
        ModelContext db = new ModelContext();
        public ActionResult Index()
        {
            Teste t = new Teste();
            t.id = 1;

            db.testes.Add(t);
            db.SaveChanges();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}