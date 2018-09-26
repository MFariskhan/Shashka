using Shaska.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shaska.Dto
{
    public class CartDto

    {
        public int Id { get; set; }
        public Product product { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }

    }
}