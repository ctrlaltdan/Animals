using Animals.Domain.Animals;
using Animals.Domain.Users;
using Autofac;

namespace Animals.Infrastructure
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<AnimalRepository>().As<IAnimalRepository>();
        }
    }
}