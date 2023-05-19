using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Genshin_Trade_Center.Models;

namespace Genshin_Trade_Center.Controllers
{
    [Authorize]
    public class ResourcesController : Controller
    {
        private readonly ApplicationDbContext db =
            new ApplicationDbContext();

        // GET: Resources
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Admin");
            }
            return View(db.Resources.ToList());
        }

        // GET: Resources/Admin
        public ActionResult Admin()
        {
            if (!User.IsInRole("Admin"))
            {
                return new 
                    HttpStatusCodeResult
                    (HttpStatusCode.Forbidden);
            }
            return View(db.Resources.ToList());
        }

        // GET: Resources/Create
        public ActionResult Create()
        {
            if (!User.IsInRole("Admin"))
            {
                return new
                    HttpStatusCodeResult
                    (HttpStatusCode.Forbidden);
            }

            return View(new Resource());
        }

        // POST: Resources/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price")]
        Resource resource)
        {
            if (!User.IsInRole("Admin"))
            {
                return new
                    HttpStatusCodeResult
                    (HttpStatusCode.Forbidden);
            }

            if (ModelState.IsValid)
            {
                db.Resources.Add(resource);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resource);
        }

        // GET: Resources/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!User.IsInRole("Admin"))
            {
                return new
                    HttpStatusCodeResult
                    (HttpStatusCode.Forbidden);
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        // POST: Resources/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price")]
        Resource resource)
        {
            if (!User.IsInRole("Admin"))
            {
                return new
                    HttpStatusCodeResult
                    (HttpStatusCode.Forbidden);
            }

            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            db.Entry(resource).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Resources/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!User.IsInRole("Admin"))
            {
                return new
                    HttpStatusCodeResult
                    (HttpStatusCode.Forbidden);
            }

            if (id == null)
            {
                return new
                    HttpStatusCodeResult
                    (HttpStatusCode.BadRequest);
            }
            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        // POST: Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return new
                    HttpStatusCodeResult
                    (HttpStatusCode.Forbidden);
            }

            Resource resource = db.Resources.Find(id);
            db.Resources.Remove(resource);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
