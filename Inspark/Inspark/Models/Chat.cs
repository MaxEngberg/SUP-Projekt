using System;
using System.Collections.Generic;
using System.Text;

namespace Inspark.Models
{
    public class Chat
    {
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public int Id { get; set; }
        public virtual ICollection<View> Views { get; set; }
    }
}
