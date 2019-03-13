using SubscriptionApi.Models;
using SubscriptionApi.SubscriptionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace SubscriptionApi.Controllers
{
    public class UserController : BaseController
    {
        public UserController(ISubscriptionService service) : base(service)
        {

        }

        // GET: api/User
        public async Task<IHttpActionResult> Get()
        {

            UserDetails[] userDetails = await _service.GetUsersDataAsync();
            try
            {
                if (userDetails.Count() > 0)
                {
                    return Ok(userDetails.Select(x => new Users
                    {
                        id = x.Id,
                        firstname = x.FirstName,
                        lastname = x.LastName,
                        email = x.Email,
                        subscriptions = x.Subscriptions.Select(y => new Subscriptions { id = y.SubscriptionId, name = y.Name, price = y.Price, priceIncVatAmount = y.VatAmount, callminutes = y.CallMinutes }).ToList(),
                        totalcallminutes = x.Subscriptions.Sum(s => s.CallMinutes),
                        totalPriceIncVatAmount = x.Subscriptions.Sum(c => c.VatAmount),

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

        // GET: api/User/5
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                Users user = new Users();
                UserDetails data = await _service.GetUserDataAsync(id);
                if (data != null)
                {
                    user.id = data.Id;
                    user.firstname = data.FirstName;
                    user.lastname = data.LastName;
                    user.email = data.Email;
                    user.subscriptions = data.Subscriptions.Select(x => new Subscriptions { id = x.SubscriptionId, name = x.Name, price = x.Price, priceIncVatAmount = x.VatAmount, callminutes = x.CallMinutes }).ToList();
                    user.totalcallminutes = data.Subscriptions.Sum(s => s.CallMinutes);
                    user.totalPriceIncVatAmount = data.Subscriptions.Sum(c => c.VatAmount);
                    return Ok(user);
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

        // POST: api/User
        public async Task<IHttpActionResult> Post([FromBody]Users requestData)
        {
            try
            {
                UserDetails user = new UserDetails();
                user.Id = requestData.id;
                user.FirstName = requestData.firstname;
                user.LastName = requestData.lastname;
                user.Email = requestData.email;
                user.Subscriptions = requestData.subscriptions.Select(x => new SubscriptionDetails { SubscriptionId = x.id, Name = x.name, Price = x.price, VatAmount = x.priceIncVatAmount, CallMinutes = x.callminutes }).ToArray();

                return Ok(await _service.AddUserAsync(user));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/User/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]Users requestData)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != requestData.id)
            {
                return BadRequest();
            }
            try
            {
                UserDetails user = new UserDetails();
                user.Id = requestData.id;
                user.FirstName = requestData.firstname;
                user.LastName = requestData.lastname;
                user.Email = requestData.email;
                user.Subscriptions = requestData.subscriptions.Select(x => new SubscriptionDetails { SubscriptionId = x.id, Name = x.name, Price = x.price, VatAmount = x.priceIncVatAmount, CallMinutes = x.callminutes }).ToArray();

                return Ok(await _service.UpdateUserAsync(user));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/User/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                return Ok(await _service.DeleteUserAsync(id));
            }

            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
