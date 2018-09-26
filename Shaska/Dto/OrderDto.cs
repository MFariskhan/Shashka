using Shaska.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shaska.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public ApplicationUserDto ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public ProductDto Product { get; set; }
        public int ProductId { get; set; }
        [Required]
        [Range(1, 10)]
        public int Quantity { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public DateTime OrderPostDate { get; set; }
        public DateTime OrderConfirmDate { get; set; }
        public ApplicationUser OrderConfirmBy { get; set; }
        public string OrderConfirmById { get; set; }
        public ApplicationUser OrderAssignTo { get; set; }
        public string OrderAssignToId { get; set; }
        public string Barcode { get; set; }
        public int Key { get; set; }
    }
}