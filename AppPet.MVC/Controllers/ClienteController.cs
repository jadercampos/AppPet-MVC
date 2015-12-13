using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using AppPet.MVC.Models;

namespace AppPet.MVC.Controllers
{
    public class ClienteController : Controller
    {
        private AppPetContext db = new AppPetContext();

        // GET: Cliente
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? pages)
        {
            int pageSize = 3;
            if (pages.HasValue)
            {
                pageSize = pages.Value;
            }
            ViewBag.pages = pageSize;
            ViewBag.CurrentSort = sortOrder;
            if (sortOrder == null)
            {
                sortOrder = String.IsNullOrEmpty(sortOrder) ? "Nome" : "";
            }
            else
            {
                if (sortOrder.Contains("_desc"))
                {
                    sortOrder = sortOrder.Replace("_desc", "");
                }
                else
                {
                    sortOrder = sortOrder + "_desc";
                }
            }
            ViewBag.NameSortParm = sortOrder;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var cliente = from e in db.Clientes select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                cliente = cliente.Where(e => e.Nome.ToUpper().Contains(searchString.ToUpper())
                                       || e.Descricao.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Nome_desc":
                    cliente = cliente.OrderByDescending(e => e.Nome);
                    break;
                //case "Descrição":
                //    servico = servico.OrderBy(e => e.Descricao);
                //    break;
                //case "Descrição_desc":
                //    servico = servico.OrderByDescending(e => e.Descricao);
                //    break;
                //case "Raça":
                //    servico = servico.OrderBy(e => e.Raca.Nome);
                //    break;
                //case "Raça_desc":
                //    servico = servico.OrderByDescending(e => e.Raca.Nome);
                //    break;
                //case "Tipo de Serviço":
                //    servico = servico.OrderBy(e => e.TipoServico.Nome);
                //    break;
                //case "Tipo de Serviço_desc":
                //    servico = servico.OrderByDescending(e => e.TipoServico.Nome);
                //    break;
                //case "Preço":
                //    servico = servico.OrderBy(e => e.Preco);
                //    break;
                //case "Preço_desc":
                //    servico = servico.OrderByDescending(e => e.Preco);
                //    break;
                default:
                    cliente = cliente.OrderBy(e => e.Nome);
                    break;
            }
            int pageNumber = (page ?? 1);
            return View(cliente.ToPagedList(pageNumber, pageSize));
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClienteId,Nome,Telefone,Celular,Facebook,DtNascimento,Cep,Endereco,Numero,Complemento,Bairro,Cidade,Uf,Descricao,DtInc,DtAlt,UserInc,UserAlt")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
				cliente.DtInc = DateTime.Now;
                db.Clientes.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClienteId,Nome,Telefone,Celular,Facebook,DtNascimento,Cep,Endereco,Numero,Complemento,Bairro,Cidade,Uf,Descricao,DtInc,DtAlt,UserInc,UserAlt")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
				cliente.DtAlt = DateTime.Now;
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
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
