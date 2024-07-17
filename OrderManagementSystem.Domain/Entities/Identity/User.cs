using Microsoft.AspNetCore.Identity;

namespace OrderManagementSystem.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public User() => Customer = new HashSet<Customer>();
        public List<RefreshToken>? RefreshTokens { get; set; }

        /*-------- Relations --------*/
        public virtual ICollection<Customer> Customer { get; set; }
    }
}
