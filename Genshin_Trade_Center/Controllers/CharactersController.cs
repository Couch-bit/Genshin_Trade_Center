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
    /// 
    /// </summary>
    /// <remarks></remarks>
    [Authorize]
    public class CharactersController : BaseController
    {
        private readonly ApplicationDbContext db = new
            ApplicationDbContext();

        // GET: Characters
        public ActionResult Index()
        {
            IQueryable<Character> characters = db.Products
                .Where(i => i is Character).AsEnumerable()
                .Cast<Character>().AsQueryable()
                .Where(i => i.SellerId != User.Identity.GetUserId())
                .Include(i => i.Seller).Include(i => i.Archetype);

            return View(characters.ToList());
        }

        public ActionResult MyStore()
        {
            IQueryable<Character> characters = db.Products
                .Where(i => i is Character).AsEnumerable()
                .Cast<Character>().AsQueryable()
                .Where(i => i.SellerId == User.Identity.GetUserId())
                .Include(i => i.Seller).Include(i => i.Archetype);

            return View(characters.ToList());
        }

        // GET: Characters/Details/5
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

        // GET: Characters/Details/5
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
