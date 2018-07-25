using SuperHeroes.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SuperHeroes
{
    public class HeroController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Hero
        public ActionResult Index()
        {
            return View(db.SuperHeroes.ToList());
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "HeroName, AlterEgo, HeroPrimaryAbility, HeroSecondaryAbility, Catchphrase")] SuperHero superHero) {
            if (ModelState.IsValid) {
                db.SuperHeroes.Add(superHero);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(superHero);
        }

        public ActionResult Delete(int id) {
            var deleteHeroes = (from h in db.SuperHeroes
                              where h.HeroId == id
                              select h).FirstOrDefault();
            return View(deleteHeroes);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id) {
            SuperHero superHero = db.SuperHeroes.Find(id);
            if (ModelState.IsValid) {
                db.SuperHeroes.Remove(superHero);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(superHero);
        }

        public ActionResult Edit(int? id) {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuperHero superHero = db.SuperHeroes.Find(id);
            
            if (superHero == null)
            {
                return HttpNotFound();
            }
            return View(superHero);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SuperHero superHero) {
            SuperHero superHeroInDB = db.SuperHeroes.Find(superHero.HeroId);
            superHeroInDB.HeroName = superHero.HeroName;
            superHeroInDB.AlterEgo = superHero.AlterEgo;
            superHeroInDB.HeroPrimaryAbility = superHero.HeroPrimaryAbility;
            superHeroInDB.HeroSecondaryAbility = superHero.HeroSecondaryAbility;
            superHeroInDB.CatchPhrase = superHero.CatchPhrase;
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}