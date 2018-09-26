using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shaska.Models
{
    public class Recommendation
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}