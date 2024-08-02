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
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int page = 1, string SortColumn = "")
        {
            MyDBContext pro = new MyDBContext();
            List<Product> pros = pro.Products.ToList();
            //Phan Trang
            int NoOfRecordPerPage = 4;
            int NoOfPages = Convert.ToInt32(Math.Ceiling
                (Convert.ToDouble(pros.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            pros = pros.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
            switch (SortColumn)
            {
                case "TangID":
                    pros = pros.OrderBy(row => row.ProductID).ToList();
                    break;
                case "GiamID":
                    pros = pros.OrderByDescending(row => row.ProductID).ToList();
                    break;
                case "TangName":
                    pros = pros.OrderBy(row => row.ProductName).ToList();
                    break;
                case "GiamName":
                    pros = pros.OrderByDescending(row => row.ProductName).ToList();
                    break;
            }
            return View(pros);
        }
        public ActionResult ThemSP()
        {
            Product pro = new Product();
            return View(pro);
        }
        [HttpPost]
        public ActionResult ThemSP(Product product, HttpPostedFileBase imageFile)
        {

            if (ModelState.IsValid)
            {
                MyDBContext db = new MyDBContext();

                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    if (imageFile.ContentLength > 2000000)
                    {
                        ModelState.AddModelError("Image", "Kích thước file phải nhỏ hơn 2MB.");
                        return View();
                    }

                    var allowEx = new[] { ".jpg", ".png" };
                    var fileEx = Path.GetExtension(imageFile.FileName).ToLower();
                    if (!allowEx.Contains(fileEx))
                    {
                        ModelState.AddModelError("Image", "Chỉ chấp nhận file ảnh jpg hoặc png.");
                        return View();
                    }

                    product.Image = "";
                    db.Products.Add(product);
                    db.SaveChanges();

                    Product pro = db.Products.ToList().Last();

                    var fileName = pro.ProductID.ToString() + fileEx;
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    imageFile.SaveAs(path);

                    pro.Image = fileName;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    product.Image = "";
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(product);
            }
        }
        public ActionResult SuaSP(int id)
        {
            MyDBContext db = new MyDBContext();
            Product pro = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            return View(pro);
        }
        [HttpPost]
        public ActionResult SuaSP(Product product)
        {
            MyDBContext db = new MyDBContext();
            Product pro = db.Products.Where(row => row.ProductID == product.ProductID).FirstOrDefault();
            //Update
            pro.ProductID = product.ProductID;
            pro.ProductName = product.ProductName;
            pro.ProductPrice = product.ProductPrice;
            pro.Description = product.Description;
            pro.Image = product.Image;
            pro.CategoryID = product.CategoryID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult XoaSP(int id)
        {
            MyDBContext db = new MyDBContext();
            Product pro = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            return View(pro);
        }
        [HttpPost]
        public ActionResult XoaSP(int id, Product product)
        {
            MyDBContext db = new MyDBContext();
            Product pro = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            db.Products.Remove(pro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}