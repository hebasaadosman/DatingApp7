

using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    // AppUserRole is a join table between AppUser and AppRole(Many to Many relationship)
    public class AppUserRole:IdentityUserRole<int>
    {
        public AppUser User { get; set; }
        public AppRole Role { get; set; }
        
    }
}