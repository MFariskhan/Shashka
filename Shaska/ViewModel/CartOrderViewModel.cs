using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shaska.Models;

namespace Shaska.ViewModel
{
    public class CartOrderViewModel
    {
        public Cart cart { get; set; }
        public Product product { get; set; }
    }
}