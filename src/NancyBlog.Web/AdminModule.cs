using Nancy;
using NancyBlog.Web.Models;
using Nancy.Security;

namespace NancyBlog.Web
{
    public class AdminModule : NancyModule
    {
        public AdminModule() : base("/admin")
        {
            this.RequiresAuthentication();

            Get["/"] = x => {
                var model = new UserModel(Context.CurrentUser.UserName);
                return View["admin/index.cshtml", model];
            };
        }
    }
}