using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; } // Username property
        [Required]
        public string knownAs { get; set; } // KnownAs property
        [Required]
        public string Gender { get; set; }
        [Required]
        public string City { get; set; } // City property
        public string Country { get; set; } // Country property
        [Required]
        public DateOnly? DateOfBirth { get; set; } // DateOfBirth property//optional to make required work

        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; } // Password property
    }
}