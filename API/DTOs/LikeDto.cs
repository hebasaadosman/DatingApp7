

namespace API.DTOs
{
    public class LikeDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } // username of the user who is liked
        public string KnownAs { get; set; } // known as of the user who is liked
        public int Age { get; set; } // age of the user who is liked
        public string PhotoUrl { get; set; } // photo url of the user who is liked
        public string City { get; set; } // city of the user who is liked
       // public string Country { get; set; } // country of the user who is liked
        
    }
}