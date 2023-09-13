

namespace API.Errors
{
    public class ApiException
    {
        public ApiException(int statusCode, string message = null, string details = null) // Constructor
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
        public int StatusCode { get; set; } // Status code property
        public string Message { get; set; } // Message property
        public string Details { get; set; } // Details property
        
    }
}