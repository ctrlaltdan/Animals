using Animals.Application.Adoption;
using Animals.Application.Feeding;
using Animals.Application.Petting;
using Animals.Application.User;
using Autofac;

namespace Animals.Application
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AdoptionService>().As<IAdoptionService>();
            builder.RegisterType<FeedingService>().As<IFeedingService>();
            builder.RegisterType<PettingService>().As<IPettingService>();
            builder.RegisterType<UserService>().As<IUserService>();
        }
    }
}