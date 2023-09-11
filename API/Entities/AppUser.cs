

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; } // byte array for password hash
        public byte[] PasswordSalt { get; set; } // byte array for password salt
    }
}