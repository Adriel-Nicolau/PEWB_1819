using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using ResidualCenter.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace ResidualCenter.Controllers
{
    public class AdminController : Controller
    {

        ApplicationDbContext context;
        ResidualCenterContext residual;
        public AdminController()
        {
            context = new ApplicationDbContext();
            residual = new ResidualCenterContext();
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
            var entities = residual.Entities.Include(e => e.Location);
            return View(entities.ToList());
        }

        //// GET: Entities/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Entity entity = db.Entities.Find(id);
        //    if (entity == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(entity);
        //}

        //// GET: Entities/Create
        //public ActionResult Create()
        //{
        //    ViewBag.LocationID = new SelectList(db.Locations, "ID", "Name");
        //    return View();
        //}

        //// POST: Entities/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Email,Name,Contact,BirthDate,Gender,Adress,LocationID")] Entity entity)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entities.Add(entity);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.LocationID = new SelectList(db.Locations, "ID", "Name", entity.LocationID);
        //    return View(entity);
        //}

        //// GET: Entities/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Entity entity = db.Entities.Find(id);
        //    if (entity == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.LocationID = new SelectList(db.Locations, "ID", "Name", entity.LocationID);
        //    return View(entity);
        //}

        //// POST: Entities/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Email,Name,Contact,BirthDate,Gender,Adress,LocationID")] Entity entity)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(entity).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.LocationID = new SelectList(db.Locations, "ID", "Name", entity.LocationID);
        //    return View(entity);
        //}

        //// GET: Entities/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Entity entity = db.Entities.Find(id);
        //    if (entity == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(entity);
        //}

        //// POST: Entities/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Entity entity = db.Entities.Find(id);
        //    db.Entities.Remove(entity);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

    }

}