using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Genshin_Trade_Center.Models
{
    public class CharactersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Characters
        public ActionResult Index()
        {
            var products = db.Products.Include(c => c.Seller).Include(c => ((Character)c).Archetype);
            return View(products.ToList());
        }

        // GET: Characters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
            ViewBag.SellerId = new SelectList(db.Userss, "Id", "Email");
            ViewBag.ArchetypeId = new SelectList(db.CharacterArchetypes, "Id", "Name");
            return View();
        }

        // POST: Characters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Level,SellerId,Friendship,ArchetypeId,Constellation")] Character character)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(character);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SellerId = new SelectList(db.Userss, "Id", "Email", character.SellerId);
            ViewBag.ArchetypeId = new SelectList(db.CharacterArchetypes, "Id", "Name", character.ArchetypeId);
            return View(character);
        }

        // GET: Characters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character character = (Character)db.Products.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            ViewBag.SellerId = new SelectList(db.Userss, "Id", "Email", character.SellerId);
            ViewBag.ArchetypeId = new SelectList(db.CharacterArchetypes, "Id", "Name", character.ArchetypeId);
            return View(character);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Level,SellerId,Friendship,ArchetypeId,Constellation")] Character character)
        {
            if (ModelState.IsValid)
            {
                db.Entry(character).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SellerId = new SelectList(db.Userss, "Id", "Email", character.SellerId);
            ViewBag.ArchetypeId = new SelectList(db.CharacterArchetypes, "Id", "Name", character.ArchetypeId);
            return View(character);
        }

        // GET: Characters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character character = (Character)db.Products.Find(id);
            if (character == null)
            {
                return HttpNotFound();
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
