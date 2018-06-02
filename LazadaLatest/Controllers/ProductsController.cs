using LazadaLatest.Models;
using LazadaLatest.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LazadaLatest.Controllers
{
    public class ProductsController : Controller
    {
        private LazadaEntities db = new LazadaEntities();

        [AllowAnonymous]
        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        [AllowAnonymous]
        // GET: Products/Details/5
        public ActionResult Details(int? id)
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

        [Authorize(Roles = "Admin")]
        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "prodId,prodName,prodDesc,prodPrice,prodQuan")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [Authorize(Roles = "Admin")]
        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
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


        [Authorize(Roles = "Admin")]
        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "prodId,prodName,prodDesc,prodPrice,prodQuan")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [Authorize(Roles = "Admin")]

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
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

        [Authorize(Roles = "Admin")]
        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public static List<Product> cart = new List<Product>();
        [Authorize(Roles = "Customer")]
        public ActionResult AddToCart(int? id)
        {
            
            ViewBag.ErrorMsg = null;

            if(id == -1)
            {
                if (cart.Count == 0)
                {
                    ViewBag.ErrorMsg = "Shopping cart is currently empty";
                }
                    return View(cart);
            }
            else
            {
                var product = (from pro in db.Products where (pro.prodId == id) select pro).FirstOrDefault();
                Product prod = (from x in cart where x.prodId == id select x).FirstOrDefault();

                if (prod != null)
                {
                    int i = cart.IndexOf(prod);
                    cart[i].prodQuan += 1;
                }

                else
                {
                    product.prodQuan = 1;
                    cart.Add(product);
                }

                return View(cart);
            }
        }

        ProductCartVM pcVM;

        [Authorize(Roles = "Customer")]
        public ActionResult CheckOut()
        {
            int _userId = (from s in db.Sessions select s.Id).FirstOrDefault();
            if (_userId != 0)
            {
                double totalPrice = 0.0;
                int totalQuantity = 0;

                foreach (var c in cart)
                {
                    
                    totalQuantity += (int)c.prodQuan;
                    totalPrice += (double)c.prodPrice * (int)c.prodQuan;
                    var prodChange = (from x in db.Products where x.prodId == c.prodId select x).FirstOrDefault();
                    if (prodChange != null)
                    {
                        prodChange.prodQuan -= (int)c.prodQuan;
                    }
                }

                var query = (from x in db.Carts where x.Id == _userId select x);
                foreach (var _c in query)
                {
                    _c.totalPrice = totalPrice;
                    _c.totalQuantity = totalQuantity;
                }
                var pcs = new List<ProductCart>();
                foreach (var c in cart)
                {
                    ProductCart pc = new ProductCart()
                    {
                        productId = c.prodId,
                        cartId = _userId,
                       // Product = c,
                        checkoutDate = System.DateTime.Now,
                        quantityPurchased = c.prodQuan
                    };
                    pcs.Add(pc);
                }
                db.ProductCarts.AddRange(pcs);
                db.SaveChanges();

                User _user = (from u in db.Users where u.userId == _userId select u).FirstOrDefault();
                //Buat ViewModels
                pcVM = new ProductCartVM()
                {
                    User = _user,
                    Products = cart.ToList(),
                    totalPrice = totalPrice,
                    totalQuantity = totalQuantity
                };


                //clear cart after checkout success
                cart.Clear();
                return View("Invoice", pcVM);

            }
            else
            {
                return RedirectToAction("AddToCart");
            }
        }

        [Authorize(Roles = "Customer")]
        // GET: Products/Edit/5
        public ActionResult EditC(int? id)
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


        [Authorize(Roles = "Customer")]
        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult EditC(Product product)
        {
            var _p = (from x in cart where x.prodId == product.prodId select x).FirstOrDefault();
            if (_p != null)
            {
                _p.prodQuan = product.prodQuan;
                return View("AddToCart",cart);
            }
            else
            {
                ViewBag.ErrorMsg = "Something has occured please try again later";
                return View("AddToCart");
            }
        }

        [Authorize(Roles = "Customer")]
        public ActionResult DeleteFromCart(int? id)
        {

            var product = from pro in db.Products.ToList() where (pro.prodId == id) select pro;
            cart = cart.Where(u => u.prodId != id).ToList();
            if(cart.Count() == 0)
            {
                ViewBag.ErrorMsg = "Shopping cart is currently empty";
            }
            return View("AddToCart", cart);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        
        public List<DateTime> purchaseDates = new List<DateTime>();
        [Authorize(Roles = "Customer")]
        public ActionResult History()
        {

            int _userId = (from s in db.Sessions select s.Id).FirstOrDefault();
            var all = from x in db.ProductCarts where x.cartId == _userId select x;

            foreach(var a in all)
            {
                if (!purchaseDates.Contains(a.checkoutDate))
                {
                    purchaseDates.Add(a.checkoutDate);
                }
            }

            return View(purchaseDates);

        }

       
        public ActionResult HistoryDetails(DateTime date)
        {
            HistoryVM histVM = new HistoryVM() {
                totalPrice = 0,
                totalQuantity = 0,
                Products = new List<ProductCart>(),
            };
            

            int _userId = (from s in db.Sessions select s.Id).FirstOrDefault();
            var all = from x in db.ProductCarts where x.cartId == _userId select x;

            

            foreach(var a in all)
            {
                //if (DateTime.Compare(Convert.ToDateTime(a.checkoutDate), Convert.ToDateTime(date)) == 0)
                if(a.checkoutDate.ToString() == date.ToString())
                {
                    histVM.Products.Add(a);
                    histVM.totalPrice += a.getTotalPrice();
                    histVM.totalQuantity += a.quantityPurchased;
                }
                
            }
            if (histVM.Products.Count==0)
            {
                ViewBag.ErrorMsg = "Invalid date!";
                return View("History", purchaseDates);
            }

            else
            {
                histVM.User = (from s in db.Users where s.userId == _userId select s).FirstOrDefault();
                histVM.purchaseDate = date;
                return View("HistoryDetails",histVM);
            }

            
        }
    }
}