using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class PhotoDto
    {
        public int Id { get; set; } // Id
        public string Url { get; set; } // Url
        public bool IsMain { get; set; } // IsMain
        
    }
}