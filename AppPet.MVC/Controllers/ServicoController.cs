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
using System.IO;

namespace AppPet.MVC.Controllers
{
    public class ServicoController : Controller
    {
        private AppPetContext db = new AppPetContext();
        // GET: Servico
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
                sortOrder = String.IsNullOrEmpty(sortOrder) ? "Serviço" : "";
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
            var servico = from e in db.Servico select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                servico = servico.Where(e => e.Nome.ToUpper().Contains(searchString.ToUpper())
                                       || e.Descricao.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Serviço_desc":
                    servico = servico.OrderByDescending(e => e.Nome);
                    break;
                case "Descrição":
                    servico = servico.OrderBy(e => e.Descricao);
                    break;
                case "Descrição_desc":
                    servico = servico.OrderByDescending(e => e.Descricao);
                    break;
                case "Raça":
                    servico = servico.OrderBy(e => e.Raca.Nome);
                    break;
                case "Raça_desc":
                    servico = servico.OrderByDescending(e => e.Raca.Nome);
                    break;
                case "Tipo de Serviço":
                    servico = servico.OrderBy(e => e.TipoServico.Nome);
                    break;
                case "Tipo de Serviço_desc":
                    servico = servico.OrderByDescending(e => e.TipoServico.Nome);
                    break;
                case "Preço":
                    servico = servico.OrderBy(e => e.Preco);
                    break;
                case "Preço_desc":
                    servico = servico.OrderByDescending(e => e.Preco);
                    break;
                default:
                    servico = servico.OrderBy(e => e.Nome);
                    break;
            }
            int pageNumber = (page ?? 1);
            return View(servico.ToPagedList(pageNumber, pageSize));
        }

        // GET: Servico/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = db.Servico.Find(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        // GET: Servico/Create
        public ActionResult Create(string fileName)
        {
            ViewBag.ListaEspecies = new SelectList(db.Especies, "EspecieID", "Nome");
            ViewBag.NomeArquivo = fileName;
            IEnumerable<SelectListItem> listaVazia = new List<SelectListItem>();
            ViewBag.RacaId = listaVazia;
            ViewBag.TipoServicoId = new SelectList(db.TipoServico, "TipoServicoId", "Nome");
            return View();
        }

        // POST: Servico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServicoId,TipoServicoId,RacaId,Nome,Descricao,Preco,DtInc,DtAlt,UserInc,UserAlt")] Servico servico)
        {
            if (servico.RacaId == 0)
            {
                ModelState.AddModelError("RacaId", "Campo Obrigatório");
                ViewBag.ListaEspecies = new SelectList(db.Especies, "EspecieID", "Nome");
                IEnumerable<SelectListItem> listaVazia = new List<SelectListItem>();
                ViewBag.RacaId = listaVazia;
            }
            if (ModelState.IsValid)
            {
                servico.DtInc = DateTime.Now;
                db.Servico.Add(servico);
                db.SaveChanges();
                ViewBag.ListaEspecies = new SelectList(db.Especies, "EspecieID", "Nome", db.Racas.Where(r => r.RacaId == servico.RacaId).SingleOrDefault().EspecieId);
                ViewBag.RacaId = new SelectList(db.Racas, "RacaId", "Nome", servico.RacaId);
                return RedirectToAction("Index");
            }
            ViewBag.TipoServicoId = new SelectList(db.TipoServico, "TipoServicoId", "Nome", servico.TipoServicoId);
            return View(servico);
        }

        // GET: Servico/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = db.Servico.Find(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListaEspecies = new SelectList(db.Especies, "EspecieID", "Nome", db.Racas.Where(r => r.RacaId == servico.RacaId).SingleOrDefault().EspecieId);
            ViewBag.RacaId = new SelectList(db.Racas, "RacaId", "Nome", servico.RacaId);
            ViewBag.TipoServicoId = new SelectList(db.TipoServico, "TipoServicoId", "Nome", servico.TipoServicoId);
            return View(servico);
        }

        // POST: Servico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServicoId,TipoServicoId,RacaId,Nome,Descricao,Preco,DtInc,DtAlt,UserInc,UserAlt")] Servico servico)
        {
            if (servico.RacaId == 0)
            {
                ModelState.AddModelError("RacaId", "Campo Obrigatório");
                ViewBag.ListaEspecies = new SelectList(db.Especies, "EspecieID", "Nome");
                IEnumerable<SelectListItem> listaVazia = new List<SelectListItem>();
                ViewBag.RacaId = listaVazia;
            }
            if (ModelState.IsValid)
            {
                servico.DtAlt = DateTime.Now;
                db.Entry(servico).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.ListaEspecies = new SelectList(db.Especies, "EspecieID", "Nome", db.Racas.Where(r => r.RacaId == servico.RacaId).SingleOrDefault().EspecieId);
                ViewBag.RacaId = new SelectList(db.Racas, "RacaId", "Nome", servico.RacaId);
                return RedirectToAction("Index");
            }
            ViewBag.TipoServicoId = new SelectList(db.TipoServico, "TipoServicoId", "Nome", servico.TipoServicoId);
            return View(servico);
        }

        // GET: Servico/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = db.Servico.Find(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        // POST: Servico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Servico servico = db.Servico.Find(id);
            db.Servico.Remove(servico);
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
        public JsonResult ListaRacas(string Id)
        {
            int id = Convert.ToInt32(Id);
            List<Raca> Racas = db.Racas.Where(r => r.EspecieId == id).ToList();
            return Json(new SelectList(Racas, "RacaId", "Nome"), JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public ActionResult UploadFile(HttpPostedFileBase fileUpload)
        //{
        //    string path = string.Empty;
        //    string fileName = string.Empty;
        //    if (fileUpload.ContentLength > 0)
        //    {
        //        fileName = Path.GetFileName(fileUpload.FileName);
        //        path = Path.Combine(Server.MapPath("~/Upload"), fileName);
        //        fileUpload.SaveAs(path);
        //    }

        //    return RedirectToAction("Create", new { fileName = fileName });
        //}
    }
}
