using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DoAn_LTWeb_TRINHCHITAI.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int ProductPrice { get; set; }
        [Required]
        //[PriceDivisibleBy1000(ErrorMessage = "Giá phải chia hết cho 1000.")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public string Description { get; set; }
        public string Image { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Cart> carts { get; set; }
    }
}