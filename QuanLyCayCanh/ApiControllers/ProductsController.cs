using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DoAn_LTWeb_TRINHCHITAI.Models;

namespace DoAn_LTWeb_TRINHCHITAI.ApiControllers
{
    public class ProductsController : ApiController
    {
        public List<Product> Get()
        {
            MyDBContext db = new MyDBContext();
            List<Product> products = db.Products.ToList();
            return products;
        }
       public Product GetProductByID(long id)
        {
            MyDBContext db = new MyDBContext();
            Product product = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            return product;
        }
    }
}
