using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shaska.Models
{
    public class Order
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        [Required]
        [Range(1,10)]
        public int Quantity { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 11 , ErrorMessage="Invalid Phone Number")]
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
        public Order()
        {
            Status = "Requested";
            OrderConfirmDate = new DateTime(1999, 09 , 09);
        }
        
    }
}