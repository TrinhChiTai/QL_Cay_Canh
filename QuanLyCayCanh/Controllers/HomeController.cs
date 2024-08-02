using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using DoAn_LTWeb_TRINHCHITAI.Models;

namespace DoAn_LTWeb_TRINHCHITAI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(string search = "", int loai = 0, int page = 1)
        {
            MyDBContext pro = new MyDBContext();
            List<Product> pros = pro.Products.ToList();


            if (loai == 0)//loc theo ten
            {
                if (search != "")//loc theo ten
                {
                    pros = pro.Products.Where(row => row.ProductName.Contains(search)).ToList();
                }
            }
            else
            {
                pros = pros.Where(row => row.CategoryID == loai).ToList();
                if (search != "")//loc theo ten
                {
                    pros = pro.Products.Where(row => row.ProductName.Contains(search)).ToList();
                }
            }
            return View(pros);
        }
        public ActionResult ChiTietSP(int id)
        {
            MyDBContext product = new MyDBContext();
            List<Product> Products = product.Products.ToList();
            //khoi tao gia tri rong
            //khi da tim thay thi gan lai
            Product Product = null;
            foreach (var pro in Products)
            {
                if (pro.ProductID == id)
                {
                    Product = pro;
                    break;
                }
            }
            if (Product != null)
            {
                return View(Product);
            }
            return View("NotFound");
        }
        
    }
}