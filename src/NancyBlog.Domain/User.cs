using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NancyBlog.Domain
{
    public class User : Entity
    {
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}
