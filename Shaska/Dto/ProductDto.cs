using Shaska.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shaska.Dto
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        [Required]
        [StringLength(255)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Product Price")]
        public float ProductPrice { get; set; }
        [Required]
        [StringLength(255)]
        public string Manufacturer { get; set; }
        public int NoOfSizes { get; set; }
        [Display(Name = "Update Date")]
        public DateTime ProductDate { get; set; }

        public List<Size> Size { get; set; }
        public ProductCategoryDto ProductCategory { get; set; }
        [Required]
        [Display(Name = "Category Feild")]
        public byte ProductCategoryId { get; set; }

        public List<Image> Image { get; set; }
       
    }
}