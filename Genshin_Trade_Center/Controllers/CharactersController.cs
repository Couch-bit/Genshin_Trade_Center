using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Genshin_Trade_Center.Models;
using Microsoft.AspNet.Identity;

namespace Genshin_Trade_Center.Controllers
{
    /// <summary>
    /// Authorize only controller responsible for managing
    /// all <see cref="Character"/> requests made by the website. 
    /// </summary>
    /// <remarks></remarks>
    [Authorize]
    public class CharactersController : BaseController
    {
        private readonly ApplicationDbContext db = new
            ApplicationDbContext();

        // GET: Characters
        /// <summary>
        /// Returns a view containing the list of 
        /// all <see cref="Character" /> objects stored in the database
        /// not sold by the current <see cref="User"/> with
        /// the option to buy or display details.
        /// </summary>
        /// <returns>
        /// Index View containing all
        /// <see cref="Character" /> objects not sold by
        /// the current <see cref="User" />.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult Index()
        {
            IQueryable<Character> characters = db.Products
                .Where(i => i is Character).AsEnumerable()
                .Cast<Character>().AsQueryable()
                .Where(i => i.SellerId != User.Identity.GetUserId())
                .Include(i => i.Seller).Include(i => i.Archetype);

            return View(characters.ToList());
        }

        // GET: Items/MyStore
        /// <summary>
        /// Returns a view containing the list of 
        /// all <see cref="Character" /> objects stored in the database
        /// sold by the current user with CRUD operations.
        /// </summary>
        /// <returns>
        /// View containing all
        /// <see cref="Character" /> objects sold by
        /// the current <see cref="User"/>.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult MyStore()
        {
            IQueryable<Character> characters = db.Products
                .Where(i => i is Character).AsEnumerable()
                .Cast<Character>().AsQueryable()
                .Where(i => i.SellerId == User.Identity.GetUserId())
                .Include(i => i.Seller).Include(i => i.Archetype);

            return View(characters.ToList());
        }

        // GET: Characters/Create
        /// <summary>
        /// Returns a form which allows for 
        /// <see cref="Character" /> creation.
        /// </summary>
        /// <returns>
        /// Form which allows for <see cref="Character" /> creation.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult Create()
        {
            ViewBag.ArchetypeId = new
                SelectList(db.CharacterArchetypes,
                "Id", "Name");
            return View();
        }

        // POST: Characters/Create
        /// <summary>
        /// Adds the given
        /// <see cref="Character" /> to the database.
        /// Returns HTTP 400 if the model
        /// sent to the method is invalid.
        /// If successful redirects to <see cref="MyStore" />.
        /// </summary>
        /// <param name="character">
        /// The <see cref="Character" /> to be added.
        /// </param>
        /// <returns>
        /// The <see cref="MyStore" /> View.
        /// HTTP 400 if the model is invalid.
        /// </returns>
        /// <remarks></remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
                    [Bind(Include = "Id,Name,Price,Level," +
            "Friendship,ArchetypeId,Constellation")]
            Character character)
        {
            if (!ModelState.IsValid)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            character.SellerId = User.Identity.GetUserId();

            db.Products.Add(character);
            db.SaveChanges();
            return RedirectToAction("MyStore");
        }

        // GET: Characters/DetailsClient/5
        /// <summary>
        /// Returns the details of the given <see cref="Character" />.
        /// Returns HTTP 400 if the id provided was null.
        /// Returns HTTP 404 if the id provided didn't correspond
        /// to a <see cref="Character" /> in the database.
        /// </summary>
        /// <param name="id">
        /// the id of the <see cref="Character" /> to display.
        /// </param>
        /// <returns>
        /// The details view for the client.
        /// HTTP 400, 404 on failure.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult DetailsClient(int? id)
        {
            if (id == null)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Character character = (Character)db.Products.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        // GET: Characters/DetailsSeller/5
        /// <summary>
        /// Returns the details of the given <see cref="Character" />.
        /// Returns HTTP 400 if the id provided was null.
        /// Returns HTTP 404 if the id provided didn't correspond
        /// to a <see cref="Character" /> in the database.
        /// Returns HTTP 403 if the current <see cref="User" />
        /// doesn't match the <see cref="Product.Seller".
        /// </summary>
        /// <param name="id">
        /// the id of the <see cref="Character" /> to display.
        /// </param>
        /// <returns>
        /// The details view for the seller.
        /// HTTP 400, 403, 404 on failure.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult DetailsSeller(int? id)
        {
            if (id == null)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Character character = (Character)db.Products.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            if (character.SellerId != User.Identity.GetUserId())
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return View(character);
        }

        // GET: Characters/Edit/5
        /// <summary>
        /// Returns a form which allows for 
        /// <see cref="Character" /> edition.
        /// Returns HTTP 400 if no id was given.
        /// Returns HTTP 404 if the given id
        /// didn't correspond to a <see cref="Character" />
        /// in the database. 
        /// </summary>
        /// <param name="id">
        /// The id of the <see cref="Character" />.
        /// </param>
        /// <returns>
        /// Form which allows for character edition.
        /// HTTP 400, 404 on failure.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Character character = (Character)db.Products.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            if (character.SellerId != User.Identity.GetUserId())
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            EditCharacterViewModel characterView = new
                EditCharacterViewModel()
            {
                Id = character.Id,
                Name = character.Name,
                Price = character.Price,
                Level = character.Level,
                Friendship = character.Friendship,
                Constellation = character.Constellation,
            };

            return View(characterView);
        }

        // POST: Characters/Edit/5
        /// <summary>
        /// Edits the given <see cref="Character"/>
        /// in the database.
        /// Returns HTTP 400 if the model state is invalid.
        /// Redirects to the <see cref="MyStore" /> view if successful.
        /// </summary>
        /// <param name="characterView">
        /// View model used in the view.
        /// </param>
        /// <returns>
        /// The <see cref="MyStore" /> View.
        /// </returns>
        /// <remarks></remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCharacterViewModel characterView)
        {
            if (!ModelState.IsValid)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Character character =
                (Character)db.Products.Find(characterView.Id);

            character.Name = characterView.Name;
            character.Price = characterView.Price;
            character.Level = characterView.Level;
            character.Friendship = characterView.Friendship;
            character.Constellation = characterView.Constellation;

            db.Entry(character).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("MyStore");
        }

        // GET: Characters/Delete/5
        /// <summary>
        /// Returns a form which allows for <see cref="Character" /> deletion.
        /// Returns HTTP 400 if id was null.
        /// Returns HTTP 404 if the character couldn't be found.
        /// Returns HTTP 403 if the <see cref="Product.Seller" />
        /// doesn't match the current <see cref="User" />.
        /// </summary>
        /// <param name="id">
        /// The id of the <see cref="Character" /> to delete.
        /// </param>
        /// <returns>
        /// The delete form.
        /// HTTP 400, 403, 404 on failure.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Character character = (Character)db.Products.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            if (character.SellerId != User.Identity.GetUserId())
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return View(character);
        }

        // POST: Characters/Delete/5
        /// <summary>
        /// Deletes the given <see cref="Character" />
        /// from the database.
        /// Redirects to <see cref="MyStore" /> view.
        /// </summary>
        /// <param name="id">
        /// The id of the <see cref="Character" />.
        /// </param>
        /// <returns>
        /// The <see cref="MyStore" /> view.
        /// </returns>
        /// <remarks></remarks>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Character character = (Character)db.Products.Find(id);

            db.Products.Remove(character);
            db.SaveChanges();
            return RedirectToAction("MyStore");
        }

        // GET: Characters/Buy/5
        /// <summary>
        /// Returns a form which allows for
        /// <see cref="Character" /> purchase.
        /// Returns HTTP 400 if id was null.
        /// Returns HTTP 404 if the
        /// <see cref="Character" /> couldn't be found.
        /// </summary>
        /// <param name="id">
        /// The id of the <see cref="Character" /> to delete.
        /// </param>
        /// <returns>
        /// The purchase form.
        /// HTTP 400, 404 on failure.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult Buy(int? id)
        {
            if (id == null)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Character character = (Character)db.Products.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        // POST: Characters/Buy/5
        /// <summary>
        /// Deletes the given <see cref="Character" />
        /// from the database.
        /// Redirects to <see cref="Index" /> view.
        /// </summary>
        /// <param name="id">
        /// The id of the <see cref="Character" /> to delete.
        /// </param>
        /// <returns>
        /// The <see cref="MyStore" /> view.
        /// </returns>
        /// <remarks></remarks>
        [HttpPost, ActionName("Buy")]
        [ValidateAntiForgeryToken]
        public ActionResult BuyConfirmed(int id)
        {
            Character character = (Character)db.Products.Find(id);

            db.Products.Remove(character);
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
