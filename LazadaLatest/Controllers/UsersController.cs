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
    public class UsersController : Controller
    {
        private LazadaEntities db = new LazadaEntities();

        // GET: Users
        public ActionResult Index()
        {
            var carts = db.Carts.Include(c => c.User);
            return View(carts.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Cart cart = db.Carts.Find(id);
            /*if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);*/

            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);

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
