using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SubscriptionService.Data;

namespace SubscriptionService
{ 
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                 Component.For<ISubscriptionService, SubscriptionService>(),
                 Component.For<IDbAccess, DbAccess>(),
                 Component.For<PhoneSubscriptionDBEntities, PhoneSubscriptionDBEntities>()
              );
        }
    }

}