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
    public class CharactersController : Controller
    {
        private readonly ApplicationDbContext db = new 
            ApplicationDbContext();

        // GET: Characters
        public ActionResult Index()
        {
            IQueryable<Character> characters = db.Products
                .Where(i => i is Character).AsEnumerable()
                .Cast<Character>().AsQueryable()
                .Include(i => i.Seller).Include(i => i.Archetype);

            return View(characters.ToList());
        }

        // GET: Characters/Details/5
        public ActionResult Details(int? id)
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

        // GET: Characters/Create
        public ActionResult Create()
        {
            ViewBag.ArchetypeId = new 
                SelectList(db.CharacterArchetypes,
                "Id", "Name");

            return View();
        }

        // POST: Characters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,Name,Price,Level," +
            "Friendship,ArchetypeId,Constellation")] 
            Character character)
        {
            ViewBag.SellerId = new
                SelectList(db.Users,
                "Id", "Email", character.SellerId);
            ViewBag.ArchetypeId = new
                SelectList(db.CharacterArchetypes,
                "Id", "Name", character.ArchetypeId);

            character.SellerId = User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                return View(character);
            }

            db.Products.Add(character);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Characters/Edit/5
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

            ViewBag.ArchetypeId = new 
                SelectList(db.CharacterArchetypes,
                "Id", "Name", character.ArchetypeId);

            return View(character);
        }

        // POST: Characters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,Name,Price,Level" +
            "Friendship,ArchetypeId,Constellation")]
            Character character)
        {
            if (!ModelState.IsValid)
            {
                return new
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.SellerId = new
                SelectList(db.Users,
                "Id", "Email", character.SellerId);
            ViewBag.ArchetypeId = new 
                SelectList(db.CharacterArchetypes,
                "Id", "Name", character.ArchetypeId);

            db.Entry(character).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        // GET: Characters/Delete/5
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
                return new
                    HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(character);
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
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
