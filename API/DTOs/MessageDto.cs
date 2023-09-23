

namespace API.DTOs
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; } // sender id
        public string SenderUsername { get; set; } // sender username
        public string SenderPhotoUrl { get; set; } // sender photo url
        public int RecipientId { get; set; } // recipient id
        public string RecipientUsername { get; set; } // recipient username
        public string RecipientPhotoUrl { get; set; } // recipient photo url
        public string Content { get; set; } // content
        public DateTime? DateRead { get; set; } // date read
        public DateTime MessageSent { get; set; } // message sent

        
    }
}