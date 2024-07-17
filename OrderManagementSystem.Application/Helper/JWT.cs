using System.ComponentModel.DataAnnotations;
#nullable disable
namespace OrderManagementSystem.Application.Helper
{
    public class JWT
    {
        [Required]
        public string Key { get; set; }
        [Required]
        public string Issuer { get; set; }
        [Required]
        public string Audience { get; set; }
        [Range(1, int.MaxValue)]
        public double DurationInDays { get; set; }
    }
}
