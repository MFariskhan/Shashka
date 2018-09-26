using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shaska.Models
{
    public class ProductCategory
    {
        public byte Id { get; set; }
        public string CategoryName { get; set; }
        public int Priority { get; set; }
        public string CategoryDescription { get; set; }
        public string ImagePath { get; set; }
    }
}