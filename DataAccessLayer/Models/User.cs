using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Models
{
    public class User:IdentityUser
    {
        public string FirstName {get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }

        // Nav properties
        public ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public ICollection<IdentityUserRole<string>> Roles { get; set; }
    }
}
