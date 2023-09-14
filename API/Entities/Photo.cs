using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; } // Id
        public string Url { get; set; } // Url
        public bool IsMain { get; set; } // IsMain
        public string PublicId { get; set; } // PublicId

        public int AppUserId { get; set; } // AppUserId
        public AppUser AppUser { get; set; } // AppUser

    }
}