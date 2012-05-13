using System.Linq;
using System.Data.Entity;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using NancyBlog.Infra;
using TinyIoC;

namespace NancyBlog.Web
{
    public class FormsAuthBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoC.TinyIoCContainer container)
        {
            // Set database initializer. Database initializer is will be ran the first time 
            // a call to the database is made. We can insert sample records in the initializer
            // class.
            Database.SetInitializer<NancyBlogDbContext>(new NancyBlogDbInitializer());
            
            base.ConfigureApplicationContainer(container);
        }

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            // We are putting static files in some folders, so we need to tell Nancy where's the folders.
            // Nancy actually will automatically treat anything in Content folder as static, but we include
            // it again here just for example
            new[] { "Content", "Scripts" }.ToList()
                .ForEach(staticDir =>
                {
                    conventions.StaticContentsConventions.Add(
                        StaticContentConventionBuilder.AddDirectory(staticDir, "/" + staticDir)
                    );
                });                
        }

        //protected override void ConfigureApplicationContainer(TinyIoC.TinyIoCContainer container)
        //{
        //    // We don't call "base" here to prevent auto-discovery of
        //    // types/dependencies
        //}

        //protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        //{
        //    base.ConfigureRequestContainer(container, context);

        //    // Here we register our user mapper as a per-request singleton.
        //    // As this is now per-request we could inject a request scoped
        //    // database "context" or other request scoped services.
        //    container.Register<IUserMapper, UserDatabase>();
        //}

        protected override void RequestStartup(TinyIoCContainer requestContainer, IPipelines pipelines, NancyContext context)
        {
            // At request startup we modify the request pipelines to
            // include forms authentication - passing in our now request
            // scoped user name mapper.
            //
            // The pipelines passed in here are specific to this request,
            // so we can add/remove/update items in them as we please.
            var formsAuthConfiguration =
                new FormsAuthenticationConfiguration()
                {
                    RedirectUrl = "~/login",
                    UserMapper = requestContainer.Resolve<IUserMapper>(),
                };

            FormsAuthentication.Enable(pipelines, formsAuthConfiguration);

            pipelines.AfterRequest.AddItemToEndOfPipeline(x =>
            {
                if (x.CurrentUser != null)
                    x.ViewBag.CurrentUserName = x.CurrentUser.UserName;
                else
                    x.ViewBag.CurrentUserName = null;
            });
        }
    }
}