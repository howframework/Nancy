using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NancyBlog.Domain
{
    public class Entity
    {
        private Guid _id = Guid.NewGuid();
        public Guid Id { get { return _id; } set { _id = value; } }
    }
}
