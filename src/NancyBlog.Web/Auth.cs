using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;
using NancyBlog.Domain;

namespace NancyBlog.Web
{
    public class Auth : IUserMapper
    {
        IRepository repo;

        public Auth(IRepository repository)
        {
            repo = repository;
        }

        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            var user = repo.Find<UserAccount>(identifier);
            return user == null
                       ? null
                       : new BlogUserIdentity { UserName = user.Username };
        }

        public Guid? ValidateUser(string username, string password)
        {
            var user = repo.First<UserAccount>(x => x.Username == username);

            if (user == null || !user.CheckPassword(password))
                return null;

            return user.Id;
        }
    }
}