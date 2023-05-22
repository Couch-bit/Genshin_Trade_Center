using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Genshin_Trade_Center.Models;

namespace Genshin_Trade_Center.Controllers
{
    /// <summary>
    /// Controller responsible for managing requests related to
    /// <see cref="Weapon" /> objects.
    /// </summary>
    /// <remarks></remarks>
    public class WeaponsController : BaseController
    {
        private readonly ApplicationDbContext db =
            new ApplicationDbContext();

        // GET: Weapons
        /// <summary>
        /// Returns a view containing the List of 
        /// all <see cref="Weapon" /> objects stored in the database
        /// with the ability to display their details.
        /// If the <see cref="User" /> has admin privileges
        /// redirects to <see cref="Admin" />.
        /// </summary>
        /// <returns>
        /// Index view containing all
        /// <see cref="Weapon" /> objects.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Admin");
            }

            return View(db.Weapons.ToList());
        }

        // GET: Weapons/Admin
        /// <summary>
        /// Returns a view containing the list of 
        /// all <see cref="Weapon" />
        /// objects stored in the database
        /// with CRUD operations.
        /// If the <see cref="User" /> doesn't have admin privileges 
        /// returns HTTP 403.
        /// </summary>
        /// <returns>
        /// Index view containing all<see cref="Weapon" /> objects.
        /// HTTP 403 if the <see cref="User" /> doesn't have 
        /// admin privileges.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult Admin()
        {
            if (!User.IsInRole("Admin"))
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(db.Weapons.ToList());
        }

        // GET: Weapons/Details/5
        /// <summary>
        /// Returns the details of the given <see cref="Weapon" />.
        /// Returns HTTP 400 if the id provided was null.
        /// Returns HTTP 404 if the id provided didn't correspond
        /// to an <see cref="Weapon" /> in the database.
        /// </summary>
        /// <param name="id">
        /// the id of the <see cref="Weapon" /> to display.
        /// </param>
        /// <returns>
        /// The details view.
        /// HTTP 400, 404 on failure.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Weapon weapon = db.Weapons.Find(id);
            if (weapon == null)
            {
                return HttpNotFound();
            }
            return View(weapon);
        }

        // GET: Weapons/DetailsAdmin/5
        /// <summary>
        /// Returns the details of the given <see cref="Weapon" />
        /// with an option to modify them.
        /// Returns HTTP 403 if the <see cref="User" /> doesn't have
        /// admin privileges (or isn't logged in).
        /// Returns HTTP 400 if the id provided was null.
        /// Returns HTTP 404 if the id provided didn't correspond
        /// to a <see cref="Weapon" /> in the database.
        /// </summary>
        /// <param name="id">
        /// the id of the <see cref="Weapon" /> to display.
        /// </param>
        /// <returns>
        /// The details view for the admin.
        /// HTTP 400, 403, 404 on failure.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult DetailsAdmin(int? id)
        {
            if (!User.IsInRole("Admin"))
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (id == null)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Weapon weapon = db.Weapons.Find(id);
            if (weapon == null)
            {
                return HttpNotFound();
            }
            return View(weapon);
        }

        // GET: Weapons/Create
        /// <summary>
        /// Returns a form which allows for 
        /// <see cref="Weapon" /> creation.
        /// If the <see cref="User" /> doesn't have
        /// admin privileges returns HTTP 403.
        /// </summary>
        /// <returns>
        /// Form which allows for <see cref="Weapon" /> creation.
        /// HTTP 403 if the <see cref="User" /> doesn't have 
        /// admin privileges.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult Create()
        {
            if (!User.IsInRole("Admin"))
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(new Weapon());
        }

        // POST: Weapons/Create
        /// <summary>
        /// Adds the given
        /// <see cref="Weapon" /> to the database.
        /// Returns HTTP 400 if the model
        /// sent to the method is invalid.
        /// Redirects to <see cref="Index" />.
        /// </summary>
        /// <param name="weapon">
        /// <see cref="Weapon" /> to be added.
        /// </param>
        /// <returns>
        /// The <see cref="Index" /> view.
        /// HTTP 400 if the model is invalid.
        /// </returns>
        /// <remarks></remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,MainStat," +
            "Type,Description,Quality")] Weapon weapon)
        {
            if (!ModelState.IsValid)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            db.Weapons.Add(weapon);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Weapons/Edit/5
        /// <summary>
        /// Returns a form which allows for 
        /// <see cref="Weapon" /> edition.
        /// Returns HTTP 403 if the <see cref="User" /> doesn't have
        /// admin privileges (or isn't logged in).
        /// Returns HTTP 400 if no id was given.
        /// Returns HTTP 404 if the given id
        /// didn't correspond to a <see cref="Weapon" />
        /// in the database. 
        /// </summary>
        /// <param name="id">
        /// the id of the <see cref="Weapon" /> to edit.
        /// </param>
        /// <returns>
        /// Form which allows for <see cref="Weapon" /> edition.
        /// HTTP 400, 403, 404 on failure.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult Edit(int? id)
        {
            if (!User.IsInRole("Admin"))
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (id == null)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weapon weapon = db.Weapons.Find(id);
            if (weapon == null)
            {
                return HttpNotFound();
            }
            return View(weapon);
        }

        // POST: Weapons/Edit/5
        /// <summary>
        /// Edits the given <see cref="Weapon"/>
        /// in the database.
        /// Returns HTTP 400 if the model state is invalid.
        /// Redirects to the <see cref="Index" /> view if successful.
        /// </summary>
        /// <param name="weapon">
        /// The <see cref="Weapon" />.
        /// </param>
        /// <returns>
        /// The <see cref="Index" /> view.
        /// HTTP 400 if the model is invalid.
        /// </returns>
        /// <remarks></remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,MainStat," +
            "Type,Description,Quality")] Weapon weapon)
        {
            if (!ModelState.IsValid)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            db.Entry(weapon).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Weapons/Delete/5
        /// <summary>
        /// Returns a form which allows for
        /// <see cref="Weapon" /> deletion.
        /// Returns HTTP 403 if the <see cref="User" /> doesn't have
        /// admin privileges.
        /// Returns HTTP 400 if no id was given.
        /// Returns HTTP 404 if the id given
        /// didn't correspond to a <see cref="Weapon" />
        /// in the database.
        /// Returns the delete view if Successful.
        /// </summary>
        /// <param name="id">
        /// The id of the <see cref="Weapon" />
        /// </param>
        /// <returns>
        /// The delete view.
        /// HTTP 400, 403, 404 on failure.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult Delete(int? id)
        {
            if (!User.IsInRole("Admin"))
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (id == null)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Weapon weapon = db.Weapons.Find(id);
            if (weapon == null)
            {
                return HttpNotFound();
            }
            return View(weapon);
        }

        // POST: Weapons/Delete/5
        /// <summary>
        /// Deletes the given <see cref="Weapon" />.
        /// from the database.
        /// Redirects to <see cref="Index" /> View.
        /// </summary>
        /// <param name="id">
        /// the id of the <see cref="Weapon" /> to be deleted.
        /// </param>
        /// <returns>
        /// The <see cref="Index" /> view.
        /// </returns>
        /// <remarks></remarks>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
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
