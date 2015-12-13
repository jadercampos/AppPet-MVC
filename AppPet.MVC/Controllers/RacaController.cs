using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppPet.MVC.Models;
using PagedList;
namespace AppPet.MVC.Controllers
{
    public class RacaController : Controller
    {
        private AppPetContext db = new AppPetContext();

        // GET: Raca
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? pages)
        {
            int pageSize = 3;
            if (pages.HasValue)
            {
                pageSize = pages.Value;
            }
            ViewBag.pages = pageSize;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Raça" : "";
            ViewBag.DescSortParm = sortOrder == "Descrição" ? "Descrição_desc" : "Descrição";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var racas = from e in db.Racas select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                racas = racas.Where(e => e.Nome.ToUpper().Contains(searchString.ToUpper())
                                       || e.Descricao.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Raça":
                    racas = racas.OrderByDescending(e => e.Nome);
                    break;
                case "Descrição":
                    racas = racas.OrderBy(e => e.Descricao);
                    break;
                case "Descrição_desc":
                    racas = racas.OrderByDescending(e => e.Descricao);
                    break;
                default:
                    racas = racas.OrderBy(e => e.Nome);
                    break;
            }
            int pageNumber = (page ?? 1);
            return View(racas.ToPagedList(pageNumber, pageSize));
        }

        // GET: Raca/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Raca raca = db.Racas.Find(id);
            if (raca == null)
            {
                return HttpNotFound();
            }
            return View(raca);
        }

        // GET: Raca/Create
        public ActionResult Create()
        {
            ViewBag.EspecieId = new SelectList(db.Especies, "EspecieId", "Nome");
            return View();
        }

        // POST: Raca/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RacaId,Nome,Descricao,EspecieId,DtInc,DtAlt,UserInc,UserAlt")] Raca raca)
        {
            if (ModelState.IsValid)
            {
                raca.DtInc = DateTime.Now;
                db.Racas.Add(raca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EspecieId = new SelectList(db.Especies, "EspecieId", "Nome", raca.EspecieId);
            return View(raca);
        }

        // GET: Raca/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Raca raca = db.Racas.Find(id);
            if (raca == null)
            {
                return HttpNotFound();
            }
            ViewBag.EspecieId = new SelectList(db.Especies, "EspecieId", "Nome", raca.EspecieId);
            return View(raca);
        }

        // POST: Raca/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RacaId,Nome,Descricao,EspecieId,DtInc,DtAlt,UserInc,UserAlt")] Raca raca)
        {
            if (ModelState.IsValid)
            {
                raca.DtAlt = DateTime.Now;
                db.Entry(raca).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EspecieId = new SelectList(db.Especies, "EspecieId", "Nome", raca.EspecieId);
            return View(raca);
        }

        // GET: Raca/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Raca raca = db.Racas.Find(id);
            if (raca == null)
            {
                return HttpNotFound();
            }
            return View(raca);
        }

        // POST: Raca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Raca raca = db.Racas.Find(id);
            db.Racas.Remove(raca);
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
