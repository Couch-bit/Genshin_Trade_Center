using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Genshin_Trade_Center.Models;

namespace Genshin_Trade_Center.Controllers
{
    public class CharacterArchetypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CharacterArchetypes
        public ActionResult Index()
        {
            return View(db.CharacterArchetypes.ToList());
        }

        // GET: CharacterArchetypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CharacterArchetype characterArchetype = db.CharacterArchetypes.Find(id);
            if (characterArchetype == null)
            {
                return HttpNotFound();
            }
            return View(characterArchetype);
        }

        // GET: CharacterArchetypes/Create
        public ActionResult Create()
        {
            return View(new CharacterArchetype());
        }

        // POST: CharacterArchetypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Quality,WeaponType,VisionType")] CharacterArchetype characterArchetype)
        {
            if (ModelState.IsValid)
            {
                db.CharacterArchetypes.Add(characterArchetype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(characterArchetype);
        }

        // GET: CharacterArchetypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CharacterArchetype characterArchetype = db.CharacterArchetypes.Find(id);
            if (characterArchetype == null)
            {
                return HttpNotFound();
            }
            return View(characterArchetype);
        }

        // POST: CharacterArchetypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Quality,WeaponType,VisionType")] CharacterArchetype characterArchetype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(characterArchetype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(characterArchetype);
        }

        // GET: CharacterArchetypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CharacterArchetype characterArchetype = db.CharacterArchetypes.Find(id);
            if (characterArchetype == null)
            {
                return HttpNotFound();
            }
            return View(characterArchetype);
        }

        // POST: CharacterArchetypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CharacterArchetype characterArchetype = db.CharacterArchetypes.Find(id);
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
