using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shaska.Models
{
    public class Size
    {
        public int Id { get; set; }
        public string Sixe { get; set; }
        public int Stock { get; set; }
        public int ProductId { get; set; }
    }
}