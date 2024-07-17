using MediatR;
using OrderManagementSystem.Application.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Application.Features.Products.Models
{
    public class UpdateProduct : IRequest<Response<string>>
    {
        [Required(ErrorMessage = "Id is Required"), DisplayName("ProductId")]
        public int Id { get; set; }
        [Required, MinLength(1)]
        public string? Name { get; set; }
        [Required, MinLength(1)]
        public decimal Price { get; set; }
    }
}
