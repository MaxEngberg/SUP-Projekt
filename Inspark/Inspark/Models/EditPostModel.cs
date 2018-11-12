using System;
using System.Collections.Generic;
using System.Text;

namespace Inspark.Models
{
    public class EditPostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public bool Pinned { get; set; }
    }
}
