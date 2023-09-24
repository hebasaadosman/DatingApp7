

using API.Extensions;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int> // IdentityUser<int> for id to be int

    {

        public DateOnly DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow; // DateTime.Now for created
        public DateTime LastActive { get; set; } = DateTime.UtcNow; // DateTime.Now for last active
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; } // Looking for
        public string Interests { get; set; }
        public string City { get; set; } // City
        public string Country { get; set; } // Country
        public List<Photo> Photos { get; set; } = new(); // Photos

        public List<UserLike> LikedByUsers { get; set; } // users who like the current user
        public List<UserLike> LikedUsers { get; set; } // users who the current user likes
        public List<Message> MessagesSent { get; set; } // messages sent by the current user
        public List<Message> MessagesReceived { get; set; } // messages received by the current user
        public ICollection<AppUserRole> UserRoles { get; set; } // user roles
    }

}