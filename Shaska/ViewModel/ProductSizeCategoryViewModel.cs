using Shaska.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shaska.ViewModel
{
    public class ProductSizeCategoryViewModel
    {
        public Product product { get; set; }
        public IEnumerable<ProductCategory> productCategory { get; set; }
    }
}