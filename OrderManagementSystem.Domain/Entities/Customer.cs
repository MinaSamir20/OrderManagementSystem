#nullable disable
using OrderManagementSystem.Domain.Entities.Identity;

namespace OrderManagementSystem.Domain.Entities
{
    public class Customer : BaseEnitiy
    {
        public string Name { get; set; }
        public string Email { get; set; }

        //--Relations--//
        public string UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
