using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shaska.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public Product product { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
    }
}