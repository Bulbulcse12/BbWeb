using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BbWeb.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Product Name")]
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]

        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; }
        public string? Colour { get; set; }
        [Required]
        [Display(Name ="Available")]
        public bool IsAvailable { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string? Image { get; set; }
        [ValidateNever]

        [Display(Name ="Category Name")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        
        public Category? Category { get; set; }
        [Display(Name ="Tag Name")]
        public int TagId { get; set; }
        [ForeignKey("TagId")]
        [ValidateNever]
        public Tag? Tag { get; set; }
    }
}
