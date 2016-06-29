using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;

namespace Animals.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<Application.AutofacModule>();
            builder.RegisterModule<Infrastructure.AutofacModule>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var lifetimeScope = builder.Build();

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(lifetimeScope);
            app.UseAutofacMiddleware(lifetimeScope);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }
    }
}
