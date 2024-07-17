using MediatR;
using OrderManagementSystem.Application.Base;
using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Application.Features.Customers.Models
{
    public class CreateCustomer : IRequest<Response<string>>
    {
        [Required]
        public string? Name { get; set; }
        [Required, EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? UserId { get; set; }
    }
}
