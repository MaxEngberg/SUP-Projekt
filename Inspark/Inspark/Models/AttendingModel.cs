using System;
namespace Inspark.Models
{
    public class AttendingEventModel
    {
        public bool IsComing { get; set; }
        public string UserId { get; set; }
        public int EventId { get; set; }
    }

    public class AttendingGroupEventModel
    {
        public bool IsComing { get; set; }
        public string UserId { get; set; }
        public int GroupEventId { get; set; }
    }
}
