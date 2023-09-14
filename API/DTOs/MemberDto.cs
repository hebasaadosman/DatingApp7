

namespace API.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string PhotoUrl { get; set; } // PhotoUrl
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }  // DateTime.Now for created
        public DateTime LastActive { get; set; } // DateTime.Now for last active
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; } // Looking for
        public string Interests { get; set; }
        public string City { get; set; } // City
        public string Country { get; set; } // Country
        public List<PhotoDto> Photos { get; set; } = new(); // Photos

       

    }
}