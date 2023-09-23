using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Message
    {
        public int Id {get; set;} // id
        public int SenderId {get; set;} // sender id
        public string SenderUsername {get; set;} // sender username
        public AppUser Sender {get; set;} // sender
        public int RecipientId {get; set;} // recipient id
        public string RecipientUsername {get; set;} // recipient username
        public AppUser Recipient {get; set;} // recipient
        public string Content {get; set;} // content
        public DateTime? DateRead {get; set;} // date read
        public DateTime MessageSent {get; set;} = DateTime.UtcNow; // message sent
        public bool SenderDeleted {get; set;} // sender deleted
        public bool RecipientDeleted {get; set;} // recipient deleted
        
    }
}