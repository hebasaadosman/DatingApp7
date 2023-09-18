using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class CloudinarySettings
    {
        // These are the settings we need to get from Cloudinary
        // We need to get these from the Cloudinary dashboard
        // We need to add these to our appsettings.json file
        // We need to add these to our Startup.cs file
        public string CloudName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        
    }
}