namespace NancyBlog.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using NancyBlog.Domain;

    internal sealed class Configuration : DbMigrationsConfiguration<NancyBlog.Infra.NancyBlogDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(NancyBlog.Infra.NancyBlogDbContext context)
        {
            //  This method will be called after migrating to the latest version.

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
                context.Users.AddOrUpdate(user);
            });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
