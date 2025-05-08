using Microsoft.AspNetCore.Identity;

namespace eTickets.Models
{
    public class Users: IdentityUser
    {
        public String FullName { get; set; }
    }
}
