using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Genshin_Trade_Center.Models;

namespace Genshin_Trade_Center.Controllers
{
    /// <summary>
    /// Controller responsible for managing requests related to
    /// <see cref="CharacterArchetype" /> objects.
    /// </summary>
    /// <remarks></remarks>
    public class CharacterArchetypesController : BaseController
    {
        private readonly ApplicationDbContext db = new
            ApplicationDbContext();

        // GET: CharacterArchetypes
        /// <summary>
        /// Returns a view containing the list of 
        /// all <see cref="CharacterArchetype" />
        /// objects stored in the database.
        /// If the <see cref="User" /> has 
        /// admin privileges redirects to <see cref="Admin" />.
        /// </summary>
        /// <returns>
        /// Index View containing all
        /// <see cref="CharacterArchetype" /> objects.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Admin");
            }

            return View(db.CharacterArchetypes.ToList());
        }

        // GET: CharacterArchetypes/Admin
        /// <summary>
        /// Returns a view containing the list of 
        /// all <see cref="CharacterArchetype" />
        /// objects stored in the database
        /// with CRUD operations.
        /// If the <see cref="User" /> doesn't have admin privileges. 
        /// returns HTTP 403.
        /// </summary>
        /// <returns>
        /// Admin View containing all
        /// <see cref="CharacterArchetype" /> objects.
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

            return View(db.CharacterArchetypes.ToList());
        }

        // GET: CharacterArchetypes/Create
        /// <summary>
        /// Returns a form which allows for 
        /// <see cref="CharacterArchetype" /> Creation.
        /// If the <see cref="User" /> doesn't
        /// have admin privileges returns HTTP 403.
        /// </summary>
        /// <returns>
        /// Form which allows for <see cref="CharacterArchetype" /> Creation.
        /// HTTP 403 if the <see cref="User" /> doesn't have admin
        /// privileges.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult Create()
        {
            if (!User.IsInRole("Admin"))
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(new CharacterArchetype());
        }

        // POST: CharacterArchetypes/Create
        /// <summary>
        /// Adds the given
        /// <see cref="CharacterArchetype" /> to the database.
        /// Returns HTTP 400 if the model
        /// sent to the method is invalid.
        /// If successful redirects to <see cref="Index" />.
        /// </summary>
        /// <param name="characterArchetype">
        /// The <see cref="CharacterArchetype" /> to be added.
        /// </param>
        /// <returns>
        /// The <see cref="Index" /> view.
        /// HTTP 400 if the model is invalid.
        /// </returns>
        /// <remarks></remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Quality," +
            "WeaponType,VisionType")]
            CharacterArchetype characterArchetype)
        {
            if (!ModelState.IsValid)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            db.CharacterArchetypes.Add(characterArchetype);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: CharacterArchetypes/Edit/5
        /// <summary>
        /// Returns a form which allows for 
        /// <see cref="CharacterArchetype" /> edition.
        /// Returns HTTP 403 if the <see cref="User" /> doesn't have
        /// admin Privileges (or isn't logged in).
        /// Returns HTTP 400 if no id was given.
        /// Returns HTTP 404 if the given id
        /// didn't correspond to a <see cref="CharacterArchetype" />
        /// in the database. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Form which allows for
        /// <see cref="CharacterArchetype" /> edition.
        /// HTTP 403, HTTP 400, HTTP 404 on failure.
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

            CharacterArchetype characterArchetype =
                db.CharacterArchetypes.Find(id);
            if (characterArchetype == null)
            {
                return HttpNotFound();
            }
            return View(characterArchetype);
        }

        // POST: CharacterArchetypes/Edit/5
        /// <summary>
        /// Edits the given <see cref="CharacterArchetype"/>
        /// in the database.
        /// Returns HTTP 400 if the model state is invalid.
        /// Redirects to <see cref="Index"/> if successful.
        /// </summary>
        /// <param name="characterArchetype">
        /// The <see cref="CharacterArchetype" />.
        /// </param>
        /// <returns>
        /// The <see cref="Index"/> view.
        /// HTTP 400 if the model is invalid.
        /// </returns>
        /// <remarks></remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Quality," +
            "WeaponType,VisionType")]
            CharacterArchetype characterArchetype)
        {
            if (!ModelState.IsValid)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            db.Entry(characterArchetype).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: CharacterArchetypes/Delete/5
        /// <summary>
        /// Returns a form which allows for
        /// <see cref="CharacterArchetype" /> deletion.
        /// Returns HTTP 403 if the <see cref="User"/> doesn't have
        /// admin privileges.
        /// Returns HTTP 400 if no id was given.
        /// Returns HTTP 404 if the id given
        /// didn't correspond to a <see cref="CharacterArchetype" />
        /// in the database.
        /// Returns the delete view if successful.
        /// </summary>
        /// <param name="id">
        /// The id of the <see cref="CharacterArchetype" />.
        /// </param>
        /// <returns>
        /// The delete view.
        /// HTTP 400, HTTP 403, HTTP 404 on failure.
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

            CharacterArchetype characterArchetype =
                db.CharacterArchetypes.Find(id);
            if (characterArchetype == null)
            {
                return HttpNotFound();
            }
            return View(characterArchetype);
        }

        // POST: CharacterArchetypes/Delete/5
        /// <summary>
        /// Deletes the given <see cref="CharacterArchetype" />
        /// from the database.
        /// Redirects to <see cref="Index"/>.
        /// </summary>
        /// <param name="id">
        /// The id of the <see cref="CharacterArchetype"/>.
        /// </param>
        /// <returns>
        /// The <see cref="Index"/> view.
        /// </returns>
        /// <remarks></remarks>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CharacterArchetype characterArchetype =
                db.CharacterArchetypes.Find(id);
            db.CharacterArchetypes.Remove(characterArchetype);
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
