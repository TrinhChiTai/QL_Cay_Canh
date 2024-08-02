using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace DoAn_LTWeb_TRINHCHITAI.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }
        public int ProductID { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}