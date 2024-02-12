using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Models
{
    public class ApplicationRole:IdentityRole
    {
        public string? Description { get; set; }
    }
}
