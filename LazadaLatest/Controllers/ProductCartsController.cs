using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LazadaLatest.Models;


namespace LazadaLatest.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductCartsController : Controller
    {
        private LazadaEntities db = new LazadaEntities();

        // GET: ProductCarts
        public ActionResult Index()
        {
            var productCarts = db.ProductCarts.Include(p => p.Cart).Include(p => p.Product);
            return View(productCarts.ToList());
        }

        // GET: ProductCarts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCart productCart = db.ProductCarts.Find(id);
            if (productCart == null)
            {
                return HttpNotFound();
            }
            return View(productCart);
        }

        // GET: ProductCarts/Create
        public ActionResult Create()
        {
            ViewBag.cartId = new SelectList(db.Carts, "Id", "status");
            ViewBag.productId = new SelectList(db.Products, "prodId", "prodName");
            return View();
        }

        // POST: ProductCarts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,productId,cartId,checkoutDate,quantityPurchased")] ProductCart productCart)
        {
            if (ModelState.IsValid)
            {
                db.ProductCarts.Add(productCart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cartId = new SelectList(db.Carts, "Id", "status", productCart.cartId);
            ViewBag.productId = new SelectList(db.Products, "prodId", "prodName", productCart.productId);
            return View(productCart);
        }

        // GET: ProductCarts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCart productCart = db.ProductCarts.Find(id);
            if (productCart == null)
            {
                return HttpNotFound();
            }
            ViewBag.cartId = new SelectList(db.Carts, "Id", "status", productCart.cartId);
            ViewBag.productId = new SelectList(db.Products, "prodId", "prodName", productCart.productId);
            return View(productCart);
        }

        // POST: ProductCarts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,productId,cartId,checkoutDate,quantityPurchased")] ProductCart productCart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productCart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cartId = new SelectList(db.Carts, "Id", "status", productCart.cartId);
            ViewBag.productId = new SelectList(db.Products, "prodId", "prodName", productCart.productId);
            return View(productCart);
        }

        // GET: ProductCarts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCart productCart = db.ProductCarts.Find(id);
            if (productCart == null)
            {
                return HttpNotFound();
            }
            return View(productCart);
        }

        // POST: ProductCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductCart productCart = db.ProductCarts.Find(id);
            db.ProductCarts.Remove(productCart);
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
