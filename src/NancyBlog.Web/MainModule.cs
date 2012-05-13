namespace NancyBlog.Web
{
    using System;
    using System.Dynamic;
    using Nancy;
    using Nancy.Authentication.Forms;
    using Nancy.Extensions;
    using NancyBlog.Domain;
    using NancyBlog.Infra;

    public class MainModule : NancyModule
    {
        Auth auth;

        public MainModule(Auth authentication)
        {
            auth = authentication;

            Get["/"] = x => {
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

            Get["/logout"] = x => {
                return this.LogoutAndRedirect("~/");
            };
        }
    }
}