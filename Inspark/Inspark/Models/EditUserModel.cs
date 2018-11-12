using System;
using System.Collections.Generic;
using System.Text;

namespace Inspark.Models
{
    public class EditUserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string PhoneNumber { get; set; }
    }
}
