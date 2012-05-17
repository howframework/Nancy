using System;
using System.Dynamic;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Extensions;
using Nancy.ModelBinding;
using NancyBlog.Domain;
using NancyBlog.Infra;
using NancyBlog.Web.Models;

namespace NancyBlog.Web
{
    public class MainModule : NancyModule
    {
        NancyBlogDbContext db;
        Auth auth;

        public MainModule(Auth authentication, NancyBlogDbContext dbContext)
        {
            auth = authentication;
            db = dbContext;

            Get["/"] = x => {
                var connstr = System.Configuration.ConfigurationManager.ConnectionStrings["NancyBlogDbContext"];
                ViewBag.ConnString = "ConnString: " + connstr.ConnectionString + " \r\nProvider: " + connstr.ProviderName;
                return View["index"];
            };

            //-------------------------------------------------------------------------------- 

            Get["/login"] = x =>
                {
                    dynamic model = new ExpandoObject();
                    model.Errored = this.Request.Query.error.HasValue;

                    return View["login", model];
                };

            Post["/login"] = x => {
                var userGuid = auth.ValidateUser((string)this.Request.Form.Username, (string)this.Request.Form.Password);

                if (userGuid == null)
                {
                    return Context.GetRedirect("~/login?error=true&username=" + (string)this.Request.Form.Username);
                }

                DateTime? expiry = null;
                if (this.Request.Form.RememberMe.HasValue)
                {
                    expiry = DateTime.Now.AddDays(7);
                }

                return this.LoginAndRedirect(userGuid.Value, expiry);
            };

            Get["/register"] = x => {
                return View["register"];
            };

            Post["/register"] = x => {
                var model = this.Bind<UserModel>();

                var domain = new User()
                {
                    Username = model.Username,
                    Email = model.Email,
                    FullName = model.FullName
                };
                domain.SetPassword(model.Password);

                db.Users.Add(domain);
                db.SaveChanges();

                return Response.AsRedirect("/admin");
            };


            Get["/logout"] = x => {
                return this.LogoutAndRedirect("~/");
            };
        }
    }
}