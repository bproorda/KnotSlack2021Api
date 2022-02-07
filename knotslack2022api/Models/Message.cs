using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace knotslack2022api.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public string? UserId { get; set; }

        public string Sender { get; set; }

        public string Recipient { get; set; }

        public DateTime Date { get; set; }

        public int? ReplyId { get; set; }

        [MaxLength]
        public string? Content { get; set; }

        //Nav prop
        //public UserMessage UserMessage { get; set; }

        public Message (string sender, string recipient)
        {
            Sender = sender;
            Recipient = recipient; 
        }
    }
}
