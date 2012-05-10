using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ServiceStack.OrmLite;
using NancyBlog.Domain;

namespace NancyBlog.Infra
{
    public class DbSetup
    {
        protected IDbCommand cmd;

        public DbSetup(IDbCommand command)
        {
            cmd = command;
        }

        public void Run(bool dropExisting)
        {
            cmd.CreateTables(dropExisting, typeof(UserAccount));

            CreateSampleUsers();
        }

        private void CreateSampleUsers()
        {
            new[] {
                new UserAccount {
                    Username = "admin",
                    Email = "admin@localhost",
                    FullName = "Admin User"
                },
                new UserAccount {
                    Username = "user1",
                    Email = "user1@localhost",
                    FullName = "User 1"
                }
            }.ToList().ForEach(user =>
            {
                user.SetPassword("qwe");
                cmd.Insert(user);
            });
            
        }
    }
}