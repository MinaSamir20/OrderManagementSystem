#nullable disable
namespace OrderManagementSystem.Application.Features.Products.Responces
{
    public class GetProductResponce
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
