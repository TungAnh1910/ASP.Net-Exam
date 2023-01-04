using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExTest01.Models;

namespace ExTest01.Controllers
{
    public class ManagementController : Controller
    {
        private Model1 db = new Model1();

        [ChildActionOnly]
        public PartialViewResult CategoryMenu()
        {
            var list = db.Categories.ToList();
            return PartialView(list);
        }
        [Route("prod/prodbycart/{MaDM}")]
        public ActionResult ListProByCartid(string MaDM)
        {
            var list = db.Products.Where(e => e.MaDM == MaDM).ToList();
            return View(list);
        }
        // GET: Management
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Management/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Management/Create
        public ActionResult Create()
        {
            ViewBag.MaDM = new SelectList(db.Categories, "MaDM", "TenDM");
            return View();
        }

        // POST: Management/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
       

        [HttpPost]
        public ActionResult Create(Product emp)
        {
            try
            {
                db.Products.Add(emp);
                db.SaveChanges();
                return Json(new { result = true, JsonRequestBehavior.AllowGet });
            }catch(Exception e)
            {
                return Json(new { result = false, error = e.Message });
            }
        }

        // GET: Management/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDM = new SelectList(db.Categories, "MaDM", "TenDM", product.MaDM);
            return View(product);
        }

        // POST: Management/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(Product prod)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(prod).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { result = true, JsonRequestBehavior.AllowGet });
                }
                catch(Exception e)
                {
                    return Json(new { result = false, error = e.Message });
                }
            }
            ViewBag.MaDM = new SelectList(db.Categories, "MaDM", "TenDM", prod.MaDM);
            return View(prod);
        }

        // GET: Management/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Management/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Product prod = db.Products.Find(id);
                db.Products.Remove(prod);
                db.SaveChanges();
                return Json(new { result = true, JsonRequestBehavior.AllowGet });
            }catch(Exception e)
            {
                return Json(new { result = false, error = e.Message });
            }
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
