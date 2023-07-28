using Microsoft.AspNetCore.Identity;

namespace Restaurant.DAL.Entites
{
    public class AppUser : IdentityUser
    {
        public string Firstname { get;set; }
        public string? Lastname { get; set; }
    }
}
