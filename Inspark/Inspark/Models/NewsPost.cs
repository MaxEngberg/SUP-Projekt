using System;
using System.Collections.Generic;
using System.Text;

namespace Inspark.Models
{
    public class NewsPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public byte[] Picture { get; set; }
        public string SenderId { get; set; }
        public string Author { get; set; }
        public bool Pinned { get; set; }
        public byte[] SenderPic { get; set; }
        public ICollection<User> Views { get; set; }
    }
}
