using System;
using System.Collections.Generic;
using System.Text;

namespace Inspark.Models
{
    public class GroupChatDisplayModel
    {
        public int Id { get; set; }
        public string ChatName { get; set; }
        public string LatestMessage { get; set; }
        public byte[] ChatPic { get; set; }
        public DateTime LatestMessageDate { get; set; }
        public bool IsLatestMessageNotViewed { get; set; }
    }
}
