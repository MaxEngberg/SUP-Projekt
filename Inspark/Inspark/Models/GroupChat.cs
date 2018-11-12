using System;
using System.Collections.Generic;
using System.Text;

namespace Inspark.Models
{
    public class GroupChat
    {
        public string GroupName { get; set; }
        public byte[] GroupChatPic { get; set; }
        public virtual Group Group { get; set; }
        public int Id { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<View> Views { get; set; }
    }
}
