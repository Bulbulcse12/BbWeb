using System.ComponentModel.DataAnnotations;

namespace BbWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Category Name")]

        public string CategoryName { get; set; } = string.Empty;
    }
}
