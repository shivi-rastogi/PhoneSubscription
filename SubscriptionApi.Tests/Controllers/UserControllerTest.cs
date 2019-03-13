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
    public class UserControllerTest
    {
        ISubscriptionService _svc;
        int _userId;
        public UserControllerTest()
        {
            _svc = new SubscriptionServiceClient();
            _userId = 99;
        }
        #region User Tests
        string DummyUser()
        {
            return "{\"id\": \"" + _userId + "\",\"firstname\": \"test99\",\"lastname\": \"lastname\",\"email\": \"testlast1@mail.com\",\"subscriptions\": [{\"id\": \"2712e86a-d519-48af-b50b-194e9a2102dg\",\"name\": \"50 min deal11\",\"price\": \"24\",\"priceIncVatAmount\": \"30\",\"CallMinutes\": \"50\"}]}";
        }

        async Task<Users> AddUser(UserController controller)
        {
            Users value = JsonConvert.DeserializeObject<Users>(DummyUser());
            var result = await controller.Post(value);
            var output = await controller.Get(value.id) as OkNegotiatedContentResult<Users>;
            return output.Content;
        }
        async Task DeleteUser(UserController controller)
        {
            await controller.Delete(_userId);

        }

        [TestMethod]
        public async Task PostUsers_ShouldbeCreated()
        {
            UserController controller = new UserController(_svc);
            Assert.IsNotNull(await AddUser(controller));
            await DeleteUser(controller);
        }

        [TestMethod]
        public async Task GetUsers_ShouldReturnDetails()
        {
            UserController controller = new UserController(_svc);
            var  result = await controller.Get() as OkNegotiatedContentResult<IEnumerable<Users>>; 

            Assert.IsNotNull(result.Content);
            Assert.IsTrue(result.Content.Count() > 0);
        }

        [TestMethod]
        public async Task GetUsersById_ShouldReturnSameID()
        {

            UserController controller = new UserController(_svc);
            Users result = await AddUser(controller);

            Assert.IsNotNull(result);
            Assert.AreEqual(_userId, result.id);
            await DeleteUser(controller);
        }



        [TestMethod]
        public async Task PutUsers_ShouldbeUpdated()
        {
            UserController controller = new UserController(_svc);
            await AddUser(controller);
            Users value = JsonConvert.DeserializeObject<Users>(DummyUser());
            value.firstname = "test999";
            var result = await controller.Put(_userId, value);
            var output = await controller.Get(value.id) as OkNegotiatedContentResult<Users>;
            Assert.IsNotNull(output);
            Assert.AreEqual(value.firstname, output.Content.firstname);
            await DeleteUser(controller);
        }

        [TestMethod]
        public async Task DeleteUsers_ShouldbeDeleted()
        {
            UserController controller = new UserController(_svc);
            await AddUser(controller);
            await DeleteUser(controller);
            var output = await controller.Get(_userId) as OkNegotiatedContentResult<Users>;
            Assert.IsNull(output);

        }

        [TestMethod]
        public async Task AttachUserSubscription_ShouldbeAdded()
        {
            SubscriptionControllerTest SubTest = new SubscriptionControllerTest();

            UserController userController = new UserController(_svc);
            SubscriptionController subController = new SubscriptionController(_svc);

            Subscriptions newSub = await SubTest.AddSubscription(subController);

            await AddUser(userController);

            Users value = JsonConvert.DeserializeObject<Users>(DummyUser());
            value.subscriptions.Add(newSub);
            var result = await userController.Put(_userId, value);
            var output = await userController.Get(value.id) as OkNegotiatedContentResult<Users>;
            Assert.IsNotNull(output);
            Assert.AreEqual(value.subscriptions.Count(), output.Content.subscriptions.Count());
            await DeleteUser(userController);
            await SubTest.DeleteSubscription(subController);

        }
        #endregion
    }
}
