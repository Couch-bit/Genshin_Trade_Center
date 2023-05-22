using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Genshin_Trade_Center.Models;
using Microsoft.AspNet.Identity;

namespace Genshin_Trade_Center.Controllers
{
    /// <summary>
    /// Controller for managing request
    /// related to <see cref="Resource"/> objects 
    /// stored in the database.
    /// </summary>
    /// <remarks></remarks>
    [Authorize]
    public class ResourcesController : BaseController
    {
        private readonly ApplicationDbContext db =
            new ApplicationDbContext();

        // GET: Resources
        /// <summary>
        /// Returns a view containing the List of 
        /// all <see cref="Resource" /> objects stored in the database
        /// with sell and buy operations.
        /// If the <see cref="User"/> has admin privileges
        /// redirects to <see cref="Admin"/>.
        /// </summary>
        /// <returns>
        /// Index view containing all
        /// <see cref="Resource" /> objects.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Admin");
            }

            ViewBag.Id = User.Identity.GetUserId();
            return View(db.Resources.ToList());
        }

        // GET: Resources/Admin
        /// <summary>
        /// Returns a view containing the list of 
        /// all <see cref="Resource" />
        /// objects stored in the database
        /// with CRUD operations.
        /// If the User doesn't have Admin Privileges 
        /// returns HTTP 403.
        /// </summary>
        /// <returns>
        /// Index view containing all <see cref="Resource" /> objects.
        /// HTTP 403 if the <see cref="User" /> doesn't have admin
        /// privileges.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult Admin()
        {
            if (!User.IsInRole("Admin"))
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(db.Resources.ToList());
        }

        // GET: Resources/Create
        /// <summary>
        /// Returns a form which allows for 
        /// <see cref="Resource" /> creation.
        /// If the <see cref="User"/> 
        /// doesn't have admin privileges returns HTTP 403.
        /// </summary>
        /// <returns>
        /// Form which allows for <see cref="Resource" /> creation.
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

            return View(new Resource());
        }

        // POST: Resources/Create
        /// <summary>
        /// Adds the Given
        /// <see cref="Item" /> to the database.
        /// If successful redirects to <see cref="Index" />.
        /// Returns HTTP 400 if the model
        /// sent to the method is invalid.
        /// </summary>
        /// <param name="resource">
        /// The <see cref="Resource" /> to be added.
        /// </param>
        /// <returns>
        /// Form which allows for <see cref="Resource" /> creation.
        /// HTTP 403 if the <see cref="User" /> doesn't have admin
        /// privileges.
        /// </returns>
        /// <remarks></remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price")]
        Resource resource)
        {
            if (!ModelState.IsValid)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            db.Resources.Add(resource);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Resources/Edit/5
        /// <summary>
        /// Returns a form which allows for 
        /// <see cref="Resource" /> edition.
        /// Returns HTTP 403 if the <see cref="User" /> doesn't have
        /// admin privileges (or isn't logged in).
        /// Returns HTTP 400 if no id was given.
        /// Returns HTTP 404 if the given id
        /// didn't correspond to a <see cref="Resource" />
        /// in the database. 
        /// </summary>
        /// <param name="id">
        /// the id of the <see cref="Resource" />.
        /// </param>
        /// <returns>
        /// Form which allows for <see cref="Resource" /> edition.
        /// HTTP 400, 403, 404 on failure.
        /// </returns>
        /// <remarks></remarks>
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
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        // POST: Resources/Edit/5
        /// <summary>
        /// Edits the given <see cref="Resource"/>
        /// in the database.
        /// Returns HTTP 400 if the model state is invalid.
        /// Redirects to the <see cref="Index" /> view if successful.
        /// </summary>
        /// <param name="resource">
        /// The <see cref="Resource" />.
        /// </param>
        /// <returns>
        /// The <see cref="Index" /> view.
        /// HTTP 400 if the model is invalid.
        /// </returns>
        /// <remarks></remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price")]
        Resource resource)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            db.Entry(resource).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Resources/Delete/5
        /// <summary>
        /// Returns a form which allows for
        /// <see cref="Resource" /> deletion.
        /// Returns HTTP 403 if the <see cref="User" /> doesn't have
        /// admin privileges.
        /// Returns HTTP 400 if no id was given.
        /// Returns HTTP 404 if the id given
        /// didn't correspond to a <see cref="Resource" />
        /// in the database.
        /// Returns the delete view if Successful.
        /// </summary>
        /// <param name="id">
        /// The id of the <see cref="Resource" />.
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

            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        // POST: Resources/Delete/5
        /// <summary>
        /// Deletes the given <see cref="Resource" />
        /// from the database.
        /// Redirects to <see cref="Index" /> view.
        /// </summary>
        /// <param name="id">
        /// The id of the <see cref="Resource" />.
        /// </param>
        /// <returns>
        /// The <see cref="Index" /> view.
        /// </returns>
        /// <remarks></remarks>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resource resource = db.Resources.Find(id);
            db.Resources.Remove(resource);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Resources/Sell/5
        /// <summary>
        /// Adds the current <see cref="User" /> as 
        /// the seller of the given
        /// <see cref="Resource" />. 
        /// Returns HTTP 400 if id was null.
        /// Returns HTTP 404 if the id didn't correspond to 
        /// a <see cref="Resource" /> in the database.
        /// Returns HTTP 400 if the <see cref="User" /> 
        /// is already selling this <see cref="Resource" />.
        /// Redirect to the <see cref="Index" /> view if successful.
        /// </summary>
        /// <param name="id">
        /// The id of the <see cref="Resource" />.
        /// </param>
        /// <returns>
        /// The <see cref="Index" /> view.
        /// HTTP 400, 404 on failure.
        /// </returns>
        /// <remarks></remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sell(int? id)
        {
            if (id == null)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            else if (resource.Sellers
                .Any(s => s.Id == User.Identity.GetUserId()))
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(User.Identity.GetUserId());
            user.Resources.Add(resource);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Resources/SellStop/5
        /// <summary>
        /// Removes the current <see cref="User" />
        /// from the sellers of the given <see cref="Resource" />. 
        /// Returns HTTP 400 if id was null.
        /// Returns HTTP 404 if the id didn't correspond to 
        /// a <see cref="Resource" /> in the database.
        /// Returns HTTP 400 if the user isn't selling
        /// this <see cref="Resource" />.
        /// Redirect to the <see cref="Index" /> view if successful.
        /// </summary>
        /// <param name="id">
        /// The id of the <see cref="Resource" />.
        /// </param>
        /// <returns>
        /// The <see cref="Index" /> view.
        /// </returns>
        /// <remarks></remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SellStop(int? id)
        {
            if (id == null)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            else if (!resource.Sellers
                .Any(s => s.Id == User.Identity.GetUserId()))
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(User.Identity.GetUserId());
            user.Resources.Remove(resource);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Resources/Buy/5
        /// <summary>
        /// Deletes the first available offer of a given
        /// <see cref="Resource" /> not made by the current 
        /// <see cref="User" />. 
        /// Returns HTTP 400 if id was null.
        /// Returns HTTP 404 if the id didn't correspond to 
        /// a <see cref="Resource" /> in the database.
        /// Returns HTTP 400 if no one else is selling this
        /// <see cref="Resource" />.
        /// Redirect to the Index View if successful.
        /// </summary>
        /// <param name="id">
        /// The id of the <see cref="Resource" />.
        /// </param>
        /// <returns>
        /// The <see cref="Index" /> view.
        /// </returns>
        /// <remarks></remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Buy(int? id)
        {
            if (id == null)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            else if (!resource.Sellers
                .Any(s => s.Id != User.Identity.GetUserId()))
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            resource.Sellers.Remove(resource.Sellers
                .Find(i => i.Id != User.Identity.GetUserId()));
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
