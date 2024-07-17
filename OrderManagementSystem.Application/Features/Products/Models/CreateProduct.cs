using MediatR;
using OrderManagementSystem.Application.Base;
using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Application.Features.Products.Models
{
    public class CreateProduct : IRequest<Response<string>>
    {
        [Required, MinLength(1)]
        public string? Name { get; set; }
        [Required, MinLength(1)]
        public decimal Price { get; set; }
    }
}
