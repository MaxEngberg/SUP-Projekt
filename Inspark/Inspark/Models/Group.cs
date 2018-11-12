using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Inspark.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsIntroGroup { get; set; }
        public Section Section { get; set; }
        public int SectionId { get; set; }
        public byte[] GroupPic { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
