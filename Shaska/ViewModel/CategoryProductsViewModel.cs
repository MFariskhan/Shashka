using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shaska.Models;

namespace Shaska.ViewModel
{
    public class CategoryProductsViewModel
    {
       
        public class CategoryProductViewModel {
            public ProductCategory  ProductCategory { get; set; }
            public List<Product> Products { get; set; }
        }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<CategoryProductViewModel> CategoriesProductViewModel { get; set; }

    }
}