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
    public class TiposServicosController : Controller
    {
        private AppPetContext db = new AppPetContext();

        // GET: TiposServicos
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? pages)
        {
            int pageSize = 3;
            if (pages.HasValue)
            {
                pageSize = pages.Value;
            }
            ViewBag.pages = pageSize;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Tipo de Serviço" : "";
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
            var tipoServico = from e in db.TipoServico select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                tipoServico = tipoServico.Where(e => e.Nome.ToUpper().Contains(searchString.ToUpper())
                                       || e.Descricao.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Tipo de Serviço":
                    tipoServico = tipoServico.OrderByDescending(e => e.Nome);
                    break;
                case "Descrição":
                    tipoServico = tipoServico.OrderBy(e => e.Descricao);
                    break;
                case "Descrição_desc":
                    tipoServico = tipoServico.OrderByDescending(e => e.Descricao);
                    break;
                default:
                    tipoServico = tipoServico.OrderBy(e => e.Nome);
                    break;
            }
            int pageNumber = (page ?? 1);
            return View(tipoServico.ToPagedList(pageNumber, pageSize));
        }

        // GET: TiposServicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoServico tipoServico = db.TipoServico.Find(id);
            if (tipoServico == null)
            {
                return HttpNotFound();
            }
            return View(tipoServico);
        }

        // GET: TiposServicos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiposServicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoServicoId,Nome,Descricao")] TipoServico tipoServico)
        {
            if (ModelState.IsValid)
            {
                db.TipoServico.Add(tipoServico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoServico);
        }

        // GET: TiposServicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoServico tipoServico = db.TipoServico.Find(id);
            if (tipoServico == null)
            {
                return HttpNotFound();
            }
            return View(tipoServico);
        }

        // POST: TiposServicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoServicoId,Nome,Descricao")] TipoServico tipoServico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoServico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoServico);
        }

        // GET: TiposServicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoServico tipoServico = db.TipoServico.Find(id);
            if (tipoServico == null)
            {
                return HttpNotFound();
            }
            return View(tipoServico);
        }

        // POST: TiposServicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoServico tipoServico = db.TipoServico.Find(id);
            db.TipoServico.Remove(tipoServico);
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
