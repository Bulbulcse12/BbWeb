using System.ComponentModel.DataAnnotations;

namespace BbWeb.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
