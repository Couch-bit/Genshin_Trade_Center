using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Genshin_Trade_Center.Models;
using Microsoft.AspNet.Identity;

namespace Genshin_Trade_Center.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext db =
            new ApplicationDbContext();

        // GET: Items
        public ActionResult Index()
        {
            IQueryable<Item> items = db.Products
                .Where(i => i is Item)
                .AsEnumerable()
                .Cast<Item>().AsQueryable()
                .Where(i => i.SellerId != User.Identity.GetUserId())
                .Include(i => i.Seller).Include(i => i.Type);
            return View(items.ToList());
        }

        // GET: Items/MyStore
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

        // GET: Items/DetailsClient/5
        public ActionResult DetailsClient(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Item item = (Item)db.Products.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/DetailsSeller/5
        public ActionResult DetailsSeller(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Item item = (Item)db.Products.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.TypeId = new SelectList(db.Weapons, "Id", "Name");
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price," +
            "Level,Refinement,TypeId")]
            Item item)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            item.SellerId = User.Identity.GetUserId();

            db.Products.Add(item);
            db.SaveChanges();
            return RedirectToAction("MyStore");
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Item character = (Item)db.Products.Find(id);
            if (character == null)
            {
                return HttpNotFound();
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
                return HttpNotFound();
            }

            Item character =
                (Item)db.Products.Find(ItemView.Id);

            character.Name = ItemView.Name;
            character.Price = ItemView.Price;
            character.Level = ItemView.Level;
            character.Refinement = ItemView.Refinement;

            db.Entry(character).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("MyStore");
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = (Item)db.Products.Find(id);
            if (item == null)
            {
                return HttpNotFound();
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
                return HttpNotFound();
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
