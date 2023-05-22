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
    /// Controller for items sold in the store.
    /// </summary>
    /// <remarks></remarks>
    [Authorize]
    public class ItemsController : BaseController
    {
        private readonly ApplicationDbContext db =
            new ApplicationDbContext();

        // GET: Items
        /// <summary>
        /// Returns a view containing the list of 
        /// all <see cref="Item" /> objects stored in the database
        /// not sold by the current user with the option to buy or
        /// dispal details.
        /// </summary>
        /// <returns>
        /// Index View containing all
        /// <see cref="Item" /> objects not sold by
        /// the current user.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult Index()
        {
            IQueryable<Item> items = db.Products
                .Where(i => i is Item).AsEnumerable()
                .Cast<Item>().AsQueryable()
                .Where(i => i.SellerId != User.Identity.GetUserId())
                .Include(i => i.Seller).Include(i => i.Type);

            return View(items.ToList());
        }

        // GET: Items/MyStore
        /// <summary>
        /// Returns a view containing the List of 
        /// all <see cref="Item" /> objects stored in the database
        /// sold by the current user.
        /// </summary>
        /// <returns>
        /// Index View containing all
        /// <see cref="Item" /> objects sold by
        /// the current user.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult MyStore()
        {
            IQueryable<Item> items = db.Products
                .Where(i => i is Item).AsEnumerable()
                .Cast<Item>().AsQueryable()
                .Where(i => i.SellerId == User.Identity.GetUserId())
                .Include(i => i.Seller)
                .Include(i => i.Type);

            return View(items.ToList());
        }

        // GET: Items/Create
        /// <summary>
        /// Returns a form which allows for 
        /// <see cref="Item" /> Creation.
        /// </summary>
        /// <returns>
        /// Form which allows for <see cref="Item" /> Creation.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult Create()
        {
            ViewBag.TypeId = new SelectList(db.Weapons, "Id", "Name");
            return View();
        }

        // POST: Items/Create
        /// <summary>
        /// Adds the Given
        /// <see cref="CharacterArchetype" /> to the Database.
        /// If Successful Redirects to Index.
        /// Returns HTTP 400 if the model
        /// sent to the method is invalid.
        /// </summary>
        /// <param name="characterArchetype">
        /// The Character Archetype to be added
        /// </param>
        /// <returns>
        /// The Index View.
        /// </returns>
        /// <remarks></remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price," +
            "Level,Refinement,TypeId")]
            Item item)
        {
            if (!ModelState.IsValid)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            item.SellerId = User.Identity.GetUserId();

            db.Products.Add(item);
            db.SaveChanges();
            return RedirectToAction("MyStore");
        }

        // GET: Items/DetailsClient/5
        /// <summary>
        /// Adds the given
        /// <see cref="Item" /> to the database.
        /// If Successful Redirects to MyStore.
        /// Returns HTTP 400 if the model
        /// sent to the method is invalid.
        /// </summary>
        /// <param name="item">
        /// The Character to be added.
        /// </param>
        /// <returns>
        /// The MyStore View.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult DetailsClient(int? id)
        {
            if (id == null)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item item = (Item)db.Products.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/DetailsSeller/5
        /// <summary>
        /// Returns the details of the given <see cref="Item" />.
        /// Return HTTP 400 if the id provided was null.
        /// Returns HTTP 404 if the id provided didn't correspond
        /// to a <see cref="Item" /> in the database.
        /// </summary>
        /// <param name="id">
        /// the id of the <see cref="Item" /> to display
        /// </param>
        /// <returns>
        /// The Details Client View.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult DetailsSeller(int? id)
        {
            if (id == null)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item item = (Item)db.Products.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            if (item.SellerId != User.Identity.GetUserId())
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return View(item);
        }

        // GET: Items/Edit/5
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
        /// </returns>
        /// <remarks></remarks>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item character = (Item)db.Products.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            if (character.SellerId != User.Identity.GetUserId())
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            EditItemViewModel ItemView = new
                EditItemViewModel()
            {
                Id = character.Id,
                Name = character.Name,
                Price = character.Price,
                Level = character.Level,
                Refinement = character.Refinement,
            };

            return View(ItemView);
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditItemViewModel ItemView)
        {
            if (!ModelState.IsValid)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item item = (Item)db.Products.Find(ItemView.Id);

            item.Name = ItemView.Name;
            item.Price = ItemView.Price;
            item.Level = ItemView.Level;
            item.Refinement = ItemView.Refinement;

            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("MyStore");
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item item = (Item)db.Products.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            if (item.SellerId != User.Identity.GetUserId())
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = (Item)db.Products.Find(id);
            db.Products.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Items/Buy/5
        public ActionResult Buy(int? id)
        {
            if (id == null)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item character = (Item)db.Products.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        // POST: Items/Buy/5
        [HttpPost, ActionName("Buy")]
        [ValidateAntiForgeryToken]
        public ActionResult BuyConfirmed(int id)
        {
            Item character = (Item)db.Products.Find(id);

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
