namespace NancyBlog.Web
{
    using Nancy;
    using NancyBlog.Web.Models;
    using Nancy.Security;

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