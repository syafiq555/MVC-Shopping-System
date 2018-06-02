using LazadaLatest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LazadaLatest.Controllers
{
    public class AccountController : Controller
    {
        LazadaEntities db = new LazadaEntities();
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(User u)
        {
            User match = (from x in db.Users where x.userName == u.userName select x).FirstOrDefault();
            if (match == null)
            {
                u.userRoles = "Customer";
                db.Users.Add(u);
                Cart cart = new Cart()
                {
                    userId = u.userId,
                    status = "no"
                };
                db.Carts.Add(cart);
                db.SaveChanges();

                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Msg = "Please choose another username...";
                return View("Register");
            }
        }

        Session s;
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(User u)
        {
            User match = (from x in db.Users where x.userName == u.userName && x.userPass == u.userPass select x).FirstOrDefault();
            User accountExists = (from x in db.Users where x.userName == u.userName select x).FirstOrDefault();
            if (match == null)
            {
                if(accountExists != null)
                {
                    ViewBag.Msg = "Wrong password!";
                }

                else
                {
                    ViewBag.Msg = "Account not exist!";
                }
                
                return View();
            }

            else
            {
                FormsAuthentication.SetAuthCookie(u.userName, false); //false for not consistent cookie (safe password) 
                                                                      // HttpContext.Session["userId"] = (int)u.userId;                                                      //and true for consistent cookie
                                                                      // HttpContext.Session["cartId"] = (int)(from c in db.Carts where c.userId == u.userId select c.Id).FirstOrDefault();
                                                                      //Cart cart = (from c in db.Carts where c.Id == u.userId select c).FirstOrDefault();
                s = new Session()
                {
                    Id = match.userId
                };
                db.Sessions.Add(s);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }


        }
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            //Session se = db.Sessions.First();
           db.Sessions.Remove(db.Sessions.First());
            
           

            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}