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
            IQueryable<Item> items = db.Products.Where(i => i is Item).AsEnumerable()
                .Cast<Item>().AsQueryable()
                .Include(i => i.Seller)
                .Include(i => i.Type);
            return View(items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
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

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.SellerId = new SelectList(db.Users, "Id", "Email");
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
            item.SellerId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Products.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SellerId = new SelectList(db.Users, "Id", "Email", item.SellerId);
            ViewBag.TypeId = new SelectList(db.Weapons, "Id", "Name", item.TypeId);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.SellerId = new SelectList(db.Users, "Id", "Email", item.SellerId);
            ViewBag.TypeId = new SelectList(db.Weapons, "Id", "Name", item.TypeId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Level,Refinement,TypeId")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SellerId = new SelectList(db.Users, "Id", "Email", item.SellerId);
            ViewBag.TypeId = new SelectList(db.Weapons, "Id", "Name", item.TypeId);
            return View(item);
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
