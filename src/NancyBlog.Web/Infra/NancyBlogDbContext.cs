using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using NancyBlog.Domain;

namespace NancyBlog.Infra
{
    public class NancyBlogDbContext : DbContext
    {
        public IDbSet<User> Users { get; set; }
    }
}