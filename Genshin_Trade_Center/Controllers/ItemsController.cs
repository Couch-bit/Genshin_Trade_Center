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
    /// Controller which manages requests related to
    /// <see cref="Item" /> objects.
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
        /// not sold by the current <see cref="User" /> with the option to buy or
        /// display details.
        /// </summary>
        /// <returns>
        /// Index view containing all
        /// <see cref="Item" /> objects not sold by
        /// the current <see cref="User" />.
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
        /// Returns a view containing the list of 
        /// all <see cref="Item" /> objects stored in the database
        /// sold by the current <see cref="User" />.
        /// </summary>
        /// <returns>
        /// Index view containing all
        /// <see cref="Item" /> objects sold by
        /// the current <see cref="User" />.
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
        /// <see cref="Item" /> creation.
        /// </summary>
        /// <returns>
        /// Form which allows for <see cref="Item" /> creation.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult Create()
        {
            ViewBag.TypeId = new SelectList(db.Weapons, "Id", "Name");
            return View();
        }

        // POST: Items/Create
        /// <summary>
        /// Adds the given
        /// <see cref="Item" /> to the database.
        /// Returns HTTP 400 if the model
        /// sent to the method is invalid.
        /// If successful redirects to <see cref="Index" />.
        /// </summary>
        /// <param name="item">
        /// The <see cref="Item" /> to be added
        /// </param>
        /// <returns>
        /// The <see cref="Index" /> view.
        /// HTTP 400 if the model is invalid.
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
        /// Returns the details of the given <see cref="Item" />.
        /// Returns HTTP 400 if the id provided was null.
        /// Returns HTTP 404 if the id provided didn't correspond
        /// to an <see cref="Item" /> in the database.
        /// </summary>
        /// <param name="id">
        /// the id of the <see cref="Item" /> to display.
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
        /// Returns HTTP 400 if the id provided was null.
        /// Returns HTTP 404 if the id provided didn't correspond
        /// to a <see cref="Item" /> in the database.
        /// Returns HTTP 403 if the current <see cref="User" />
        /// doesn't match the <see cref="Product.Seller".
        /// </summary>
        /// <param name="id">
        /// the id of the <see cref="Item" /> to display.
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
        /// <see cref="Item" /> edition.
        /// Returns HTTP 400 if no id was given.
        /// Returns HTTP 404 if the given id
        /// didn't correspond to a <see cref="Item" />
        /// Returns HTTP 403 if the current <see cref="User" />
        /// doesn't match the <see cref="Product.Seller".
        /// in the database. 
        /// </summary>
        /// <param name="id">
        /// The id of the <see cref="Item" />.
        /// </param>
        /// <returns>
        /// Form which allows for <see cref="Item" /> edition.
        /// HTTP 400, 403, 404 on failure.
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
        /// <summary>
        /// Edits the given <see cref="Item"/>
        /// in the database.
        /// Returns HTTP 400 if the model state is invalid.
        /// Redirects to the <see cref="MyStore"/> view if successful.
        /// </summary>
        /// <param name="ItemView">
        /// View model used in the view.
        /// </param>
        /// <returns>
        /// The <see cref="MyStore"/> view.
        /// </returns>
        /// <remarks></remarks>
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
        /// <summary>
        /// Returns a form which allows for <see cref="Item"/> deletion.
        /// Returns HTTP 400 if id was null.
        /// Returns HTTP 404 if the <see cref="Item"/> couldn't be found.
        /// Returns HTTP 403 if the <see cref="Product.Seller"/> 
        /// doesn't match the current <see cref="User"/>.
        /// </summary>
        /// <param name="id">
        /// The id of the <see cref="Item"/> to delete.
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
        /// <summary>
        /// Deletes the given <see cref="Item" />
        /// from the database.
        /// Redirects to <see cref="MyStore"/> view.
        /// </summary>
        /// <param name="id">
        /// The id of the <see cref="Item" />.
        /// </param>
        /// <returns>
        /// The <see cref="MyStore"/> View.
        /// </returns>
        /// <remarks></remarks>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = (Item)db.Products.Find(id);
            db.Products.Remove(item);
            db.SaveChanges();
            return RedirectToAction("MyStore");
        }

        // GET: Items/Buy/5
        /// <summary>
        /// Returns a form which allows for <see cref="Item" /> purchase.
        /// Returns HTTP 400 if id was null.
        /// Returns HTTP 404 if the <see cref="Item" /> couldn't be found.
        /// </summary>
        /// <param name="id">
        /// The id of the <see cref="Item" /> to delete.
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

            Item character = (Item)db.Products.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        // POST: Items/Buy/5
        /// <summary>
        /// Deletes the given <see cref="Item" />
        /// from the database.
        /// Redirects to <see cref="MyStore"/> view.
        /// </summary>
        /// <param name="id">
        /// The id of the <see cref="Item" />.
        /// </param>
        /// <returns>
        /// The <see cref="MyStore"/> view.
        /// </returns>
        /// <remarks></remarks>
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
