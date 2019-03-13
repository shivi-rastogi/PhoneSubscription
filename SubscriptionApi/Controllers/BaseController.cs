using SubscriptionApi.SubscriptionService;
using System.Web.Http;

namespace SubscriptionApi.Controllers
{
    public class BaseController : ApiController
    {
        public ISubscriptionService _service { get;  }
        public BaseController(ISubscriptionService service)
        {
            _service = service;
        }
    }
}