using System;
using System.Collections.Generic;
using System.Text;

namespace Inspark.Models
{
    public class GroupEvent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TimeForEvent { get; set; }
        public string Location { get; set; }
        public byte[] Picture { get; set; }
        public User Sender { get; set; }
        public string senderId { get; set; }
        public Group Group { get; set; }
        public int GroupId { get; set; }
    }
}
