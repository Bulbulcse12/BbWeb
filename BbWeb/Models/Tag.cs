using System.ComponentModel.DataAnnotations;

namespace BbWeb.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Tag Name")]
        public string TagName { get; set; } = string.Empty;
    }
}
