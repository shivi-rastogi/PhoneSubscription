using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SubscriptionApi.Controllers;
using SubscriptionApi.Models;
using SubscriptionApi.SubscriptionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace SubscriptionApi.Tests.Controllers
{
    [TestClass]
    public class SubscriptionControllerTest
    {
        ISubscriptionService _svc;
        string _subscritionId ;
        public SubscriptionControllerTest()
        {
            _svc = new SubscriptionServiceClient();
            _subscritionId =  Guid.NewGuid().ToString();
        }
        #region Subscription Tests
        string DummySubscription()
        {
            return "{\"id\": \"" + _subscritionId + "\",\"name\": \"100 min deal\",\"price\": \"24\",\"priceIncVatAmount\": \"30\",\"CallMinutes\": \"50\"}";
        }
        
        public async Task<Subscriptions> AddSubscription(SubscriptionController controller)
        {
            Subscriptions value = JsonConvert.DeserializeObject<Subscriptions>(DummySubscription());
            var result = await controller.Post(value);
            var output = await controller.Get(value.id) as OkNegotiatedContentResult<Subscriptions>;
            return output.Content;
        }
       public  async Task DeleteSubscription(SubscriptionController controller)
        {
            await controller.Delete(_subscritionId);
          
        }

        [TestMethod]
        public async Task PostSubscriptions_ShouldbeCreated()
        {
            SubscriptionController controller = new SubscriptionController(_svc);
            Assert.IsNotNull(await AddSubscription(controller));
            await DeleteSubscription(controller);
        }

        [TestMethod]
        public async Task GetSubscriptions_ShouldReturnDetails()
        {            
            SubscriptionController controller = new SubscriptionController(_svc);            
            var result = await controller.Get() as OkNegotiatedContentResult<IEnumerable<Subscriptions>>; ;

            Assert.IsNotNull(result.Content);
            Assert.IsTrue( result.Content.Count() > 0);            
        }

        [TestMethod]
        public async Task GetSubscriptionsById_ShouldReturnSameID()
        {
           
            SubscriptionController controller = new SubscriptionController(_svc);
            Subscriptions result = await AddSubscription(controller);

            Assert.IsNotNull(result);
            Assert.AreEqual(_subscritionId, result.id);
            await DeleteSubscription(controller);
        }

       

        [TestMethod]
        public async Task PutSubscriptions_ShouldbeUpdated()
        {
            SubscriptionController controller = new SubscriptionController(_svc);
            await AddSubscription(controller);
            Subscriptions value = JsonConvert.DeserializeObject<Subscriptions>(DummySubscription());
            value.name = "Campaign 250 min deal";
            var result = await controller.Put(_subscritionId, value);
            var output = await controller.Get(value.id) as OkNegotiatedContentResult<Subscriptions>;
            Assert.IsNotNull(output);
            Assert.AreEqual(value.name, output.Content.name);
            await DeleteSubscription(controller);
        }

        [TestMethod]
        public async Task DeleteSubscriptions_ShouldbeDeleted()
        {
            SubscriptionController controller = new SubscriptionController(_svc);
            await AddSubscription(controller);
            await DeleteSubscription(controller);
            var output = await controller.Get(_subscritionId) as OkNegotiatedContentResult<Subscriptions>;
            Assert.IsNull(output);
            
        }
        #endregion

    }
}
