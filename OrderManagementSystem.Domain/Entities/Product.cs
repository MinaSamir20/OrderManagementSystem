#nullable disable
namespace OrderManagementSystem.Domain.Entities
{
    public class Product : BaseEnitiy
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
