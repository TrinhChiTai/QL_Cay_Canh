using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using DoAn_LTWeb_TRINHCHITAI.Models;

namespace DoAn_LTWeb_TRINHCHITAI.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            MyDBContext db = new MyDBContext();
            HttpCookie authCookie = Request.Cookies["auth"];
            
            if (authCookie != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                List<Cart> cart = db.Carts.Where(i => i.UserId == id).ToList();
                return View(cart);

            }
            else
            {
                return RedirectToAction("Login","User");
            }
        }

        public ActionResult Add(int id = 0)
        {
            if( id == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                if (id != 0)
                {
                    int idCookie = Convert.ToInt32(Request.Cookies["id"].Value);

                    MyDBContext db = new MyDBContext();
                    Cart cartItem = db.Carts.Where(cart => cart.ProductID == id && cart.UserId == idCookie).FirstOrDefault();
                    if (cartItem != null)
                    {
                        cartItem.Quantity += 1;
                    }
                    else
                    {
                        Cart cart = new Cart();
                        cart.ProductID = id;
                        cart.Quantity = 1;
                        cart.UserId = idCookie;
                        db.Carts.Add(cart);
                    }
                    db.SaveChanges();
                }
           
            }

            return RedirectToAction("Index");
        }

        public ActionResult UpdateQuantity(int quan = 0, int ProductID = 0)
        {
            MyDBContext db = new MyDBContext();
            if (quan > 0)
            {
                Cart cartItem = db.Carts.Where(cart => cart.ProductID == ProductID).FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.Quantity = quan;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
