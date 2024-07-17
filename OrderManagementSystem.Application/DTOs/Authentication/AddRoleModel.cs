using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Application.DTOs.Authentication
{
    public class AddRoleModel
    {
        [Required(ErrorMessage = "User ID is required")]
        public string? UserId { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string? Role { get; set; }
    }
}
