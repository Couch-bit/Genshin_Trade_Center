using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Genshin_Trade_Center.Models;

namespace Genshin_Trade_Center.Controllers
{
    public class WeaponsController : BaseController
    {
        private readonly ApplicationDbContext db =
            new ApplicationDbContext();

        // GET: Weapons
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Admin");
            }
            return View(db.Weapons.ToList());
        }

        // GET: CharacterArchetypes/Admin
        public ActionResult Admin()
        {
            if (!User.IsInRole("Admin"))
            {
                return new
                    HttpStatusCodeResult
                    (HttpStatusCode.Forbidden);
            }
            return View(db.Weapons.ToList());
        }

        // GET: Weapons/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }
            Weapon weapon = db.Weapons.Find(id);
            if (weapon == null)
            {
                return HttpNotFound();
            }
            return View(weapon);
        }

        // GET: Weapons/Create
        public ActionResult Create()
        {
            if (!User.IsInRole("Admin"))
            {
                return new
                    HttpStatusCodeResult
                    (HttpStatusCode.Forbidden);
            }

            if (!User.IsInRole("Admin"))
            {
                return new
                    HttpStatusCodeResult
                    (HttpStatusCode.Forbidden);
            }

            return View(new Weapon());
        }

        // POST: Weapons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,MainStat,Type,Description,Quality")] Weapon weapon)
        {
            if (!User.IsInRole("Admin"))
            {
                return new
                    HttpStatusCodeResult
                    (HttpStatusCode.Forbidden);
            }

            if (ModelState.IsValid)
            {
                db.Weapons.Add(weapon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(weapon);
        }

        // GET: Weapons/Edit/5
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
            Weapon weapon = db.Weapons.Find(id);
            if (weapon == null)
            {
                return HttpNotFound();
            }
            return View(weapon);
        }

        // POST: Weapons/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,MainStat,Type,Description,Quality")] Weapon weapon)
        {
            if (!User.IsInRole("Admin"))
            {
                return new
                    HttpStatusCodeResult
                    (HttpStatusCode.Forbidden);
            }

            if (ModelState.IsValid)
            {
                db.Entry(weapon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(weapon);
        }

        // GET: Weapons/Delete/5
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weapon weapon = db.Weapons.Find(id);
            if (weapon == null)
            {
                return HttpNotFound();
            }
            return View(weapon);
        }

        // POST: Weapons/Delete/5
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

            Weapon weapon = db.Weapons.Find(id);
            db.Weapons.Remove(weapon);
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
