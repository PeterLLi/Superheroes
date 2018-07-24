using SuperHeroes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View(db.superHeroes.ToList());
        }

        public ActionResult Create() {
            ViewBag.HeroID = new SelectList(db.superHeroes, "Super ID", "Name");
            return View();
        }

        [AcceptVerbs]
        [HttpPost]
        public ActionResult Create([Bind(Include ="Name, Alter Ego, Ability, Secondary Ability, Catchphrase")] SuperHero superHero) {
            if (ModelState.IsValid) {
                db.superHeroes.Add(superHero);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(superHero);
        }

        public ActionResult Delete(SuperHero superHero) {
            SuperHero hero = (from h in db.superHeroes
                              where h.HeroId == superHero.HeroId
                              select h).FirstOrDefault();
            db.superHeroes.Remove(hero);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}