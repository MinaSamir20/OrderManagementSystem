using MediatR;
using OrderManagementSystem.Application.Base;
using OrderManagementSystem.Application.DTOs;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Application.Features.Orders.Models
{
    public class UpdateOrder : IRequest<Response<string>>
    {
        [Required(ErrorMessage = "Id is Required"), DisplayName("OrderId")]
        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public int TotalAmount { get; set; }
        [Required, AllowedValues("Credit Card", "PayPal")]
        public string? PaymentMethod { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public IEnumerable<OrderItemDto>? OrderItems { get; set; }
    }
}
