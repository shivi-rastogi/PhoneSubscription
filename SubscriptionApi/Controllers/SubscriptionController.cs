using SubscriptionApi.Models;
using SubscriptionApi.SubscriptionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace SubscriptionApi.Controllers
{
    public class SubscriptionController : BaseController
    {

        public SubscriptionController(ISubscriptionService service) : base(service)
        {

        }
        // GET: api/Subscription
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                SubscriptionDetails[] subscriptionDetails = await _service.GetSubscriptionsDataAsync();
                if (subscriptionDetails.Count() > 0)
                {
                    return Ok(subscriptionDetails.Select(x => new Subscriptions
                    {
                        id = x.SubscriptionId,
                        name = x.Name,
                        price = x.Price,
                        priceIncVatAmount = x.VatAmount,
                        callminutes = x.CallMinutes
                    }));

                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Subscription/5
        public async Task<IHttpActionResult> Get(string id)
        {

            Subscriptions subscriptions = new Subscriptions();
            try
            {
                SubscriptionDetails data = await _service.GetSubscriptionDataAsync(id);
                if (data != null)
                {
                    subscriptions.id = data.SubscriptionId;
                    subscriptions.name = data.Name;
                    subscriptions.price = data.Price;
                    subscriptions.priceIncVatAmount = data.VatAmount;
                    subscriptions.callminutes = data.CallMinutes;
                    return Ok(subscriptions);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }
}

        // POST: api/Subscription
        public async Task<IHttpActionResult> Post([FromBody]Subscriptions requestData)
        {
           
            SubscriptionDetails subscriptionDetails = new SubscriptionDetails();
            try
            {
                subscriptionDetails.SubscriptionId = requestData.id;
                subscriptionDetails.Name = requestData.name;
                subscriptionDetails.Price = requestData.price;
                subscriptionDetails.VatAmount = requestData.priceIncVatAmount;
                subscriptionDetails.CallMinutes = requestData.callminutes;

                return CreatedAtRoute("DefaultApi", requestData, await _service.AddSubscriptionAsync(subscriptionDetails));
            }

            catch (Exception ex)
            {
                return InternalServerError(ex);

            }
}

        // PUT: api/Subscription/5
        public async Task<IHttpActionResult> Put(string id, [FromBody]Subscriptions requestData)
        {
           if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != requestData.id)
            {
                return BadRequest();
            }
            SubscriptionDetails subscription = new SubscriptionDetails();
            try
            {
                subscription.SubscriptionId = requestData.id;
                subscription.Name = requestData.name;
                subscription.Price = requestData.price;
                subscription.VatAmount = requestData.priceIncVatAmount;
                subscription.CallMinutes = requestData.callminutes;

                return Ok(await _service.UpdateSubscriptionAsync(subscription));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }

        }

        // DELETE: api/Subscription/5
        public async Task<IHttpActionResult> Delete(string id)
        {
            try
            {
                return Ok(await _service.DeleteSubscriptionAsync(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }
        }
    }
}
