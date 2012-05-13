using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using NancyBlog.Domain;

namespace NancyBlog.Infra
{
    public class NancyBlogDbInitializer : DropCreateDatabaseIfModelChanges<NancyBlogDbContext>
    {
        protected override void Seed(NancyBlogDbContext context)
        {
            new[] {
                new User {
                    Username = "admin",
                    Email = "admin@localhost",
                    FullName = "Admin User"
                },
                new User {
                    Username = "user1",
                    Email = "user1@localhost",
                    FullName = "User 1"
                }
            }.ToList().ForEach(user =>
            {
                user.SetPassword("qwe");
                context.Users.Add(user);
            });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}