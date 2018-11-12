using System;
using System.Collections.Generic;
using System.Text;

namespace Inspark.Models
{
    public class Event
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public string Location { get; set; }
        public DateTime TimeForEvent { get; set; }
        public IEnumerable<User> Invited { get; set; }
        public IEnumerable<User> Attending { get; set; }
        public string Description { get; set; }
        public string SenderId { get; set; }
    }
}
