using MediatR;
using OrderManagementSystem.Application.Base;
using OrderManagementSystem.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Application.Features.Orders.Models
{
    public class CreateOrder : IRequest<Response<string>>
    {
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public int TotalAmount { get; set; }
        [Required, AllowedValues("Credit Card", "PayPal")]
        public List<string>? PaymentMethod { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public IEnumerable<OrderItemDto>? OrderItem { get; set; }
    }
}
