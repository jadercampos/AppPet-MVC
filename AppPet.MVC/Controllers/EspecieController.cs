﻿using System;
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
    public class EspecieController : Controller
    {
        private AppPetContext db = new AppPetContext();

        // GET: Especie
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? pages)
        {
            int pageSize = 3;
            if (pages.HasValue)
            {
                pageSize = pages.Value;
            }
            ViewBag.pages = pageSize;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Espécie" : "";
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
            var especies = from e in db.Especies select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                especies = especies.Where(e => e.Nome.ToUpper().Contains(searchString.ToUpper())
                                       || e.Descricao.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Espécie":
                    especies = especies.OrderByDescending(e => e.Nome);
                    break;
                case "Descrição":
                    especies = especies.OrderBy(e => e.Descricao);
                    break;
                case "Descrição_desc":
                    especies = especies.OrderByDescending(e => e.Descricao);
                    break;
                default:
                    especies = especies.OrderBy(e => e.Nome);
                    break;
            }
            int pageNumber = (page ?? 1);
            return View(especies.ToPagedList(pageNumber, pageSize));
        }

        // GET: Especie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especie especie = db.Especies.Find(id);
            if (especie == null)
            {
                return HttpNotFound();
            }
            return View(especie);
        }

        // GET: Especie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Especie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EspecieId,Nome,Descricao,DtInc,DtAlt,UserInc,UserAlt")] Especie especie)
        {
            if (ModelState.IsValid)
            {
                especie.DtInc = DateTime.Now;
                db.Especies.Add(especie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(especie);
        }

        // GET: Especie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especie especie = db.Especies.Find(id);
            if (especie == null)
            {
                return HttpNotFound();
            }
            return View(especie);
        }

        // POST: Especie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EspecieId,Nome,Descricao,DtInc,DtAlt,UserInc,UserAlt")] Especie especie)
        {
            if (ModelState.IsValid)
            {
                especie.DtAlt = DateTime.Now;
                db.Entry(especie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(especie);
        }

        // GET: Especie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especie especie = db.Especies.Find(id);
            if (especie == null)
            {
                return HttpNotFound();
            }
            return View(especie);
        }

        // POST: Especie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Especie especie = db.Especies.Find(id);
            db.Especies.Remove(especie);
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
