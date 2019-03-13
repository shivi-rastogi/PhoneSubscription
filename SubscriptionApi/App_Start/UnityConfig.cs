using SubscriptionApi.SubscriptionService;
using System.Web.Http;
using Unity;
using Unity.Injection;
using Unity.WebApi;

namespace SubscriptionApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<ISubscriptionService, SubscriptionServiceClient>(new InjectionConstructor());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}