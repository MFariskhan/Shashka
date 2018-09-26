using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shaska.Models;
using System.ComponentModel.DataAnnotations;

namespace Shaska.ViewModel
{
    public class OrderCustomerViewModel
    {
        public string Name { get; set; }
        public string Email{ get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage="The Phone Number must have 11 digits")]
        public string PhoneNumber { get; set; }
        public List<string> ProductNames  { get; set; }
        public float Prize { get; set; }
    }
}