using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;
using NancyBlog.Domain;
using NancyBlog.Infra;

namespace NancyBlog.Web
{
    public class Auth : IUserMapper
    {
        NancyBlogDbContext db;

        public Auth(NancyBlogDbContext dbContext)
        {
            db = dbContext;
        }

        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            var user = db.Users.Find(identifier);
            return user == null
                       ? null
                       : new BlogUserIdentity { UserName = user.Username };
        }

        public Guid? ValidateUser(string username, string password)
        {
            var user = db.Users.FirstOrDefault(x => x.Username == username);

            if (user == null || !user.CheckPassword(password))
                return null;

            return user.Id;
        }
    }
}