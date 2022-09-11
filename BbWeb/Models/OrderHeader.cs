using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BbWeb.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }
        public string? ApplicationUserId { get; set; }
        [ForeignKey(" ApplicationUserId")]
        [ValidateNever]

        public ApplicationUser ApplicationUser { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public double OrderTotal { get; set; }
        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Carrier { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public string Citty { get; set; }
     
        public string? Name { get; set; }
    }
}
