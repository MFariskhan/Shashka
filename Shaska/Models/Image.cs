using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shaska.Models
{
    public class Image
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public String ImagePath { get; set; }
    }
}