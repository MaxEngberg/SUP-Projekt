using System;
using System.Collections.Generic;
using System.Text;

namespace Inspark.Models
{
    public class ChatDisplayModel
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public byte[] ChatPic { get; set; }
        public string LatestMessage { get; set; }
        public DateTime LatestMessageDate { get; set; }
        public bool IsLatestMessageNotViewed { get; set; }
    }
}
