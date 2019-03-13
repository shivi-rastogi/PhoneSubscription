using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubscriptionService.Data;
using SubscriptionService.DataContract;

namespace SubscriptionService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class SubscriptionService : ISubscriptionService
    {
        IDbAccess _dba;
        public SubscriptionService(IDbAccess dba)
        {
            _dba = dba;

        }

        public async Task<List<UserDetails>> GetUsersData()
        {
            List<UserDetails> lstUsrDetails = new List<UserDetails>();
            try
            {
                IEnumerable<User> lstUsr = await _dba.GetUsers();
                if (lstUsr.Count() > 0)
                {

                    foreach (User usr in lstUsr)
                    {
                        lstUsrDetails.Add(await ConvertToUserDetails(usr));
                    }
                    return lstUsrDetails;
                }

                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<UserDetails> GetUserData(int Id)
        {
            try
            {

                User usr = await _dba.GetUser(Id);
                if (usr != null)
                    return await ConvertToUserDetails(usr);
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AddUser(UserDetails userDetails)
        {
            try
            {
                if (await _dba.Insert(ConvertToUser(userDetails)))
                {

                    foreach (var data in userDetails.Subscriptions)
                    {
                        if (_dba.GetSubscription(data.SubscriptionId) == null)
                        {
                            if (!await AddSubscription(data))
                                return false;
                        }
                        if (!await _dba.Insert(new UserSubscription { UserId = userDetails.Id, SubscriptionId = data.SubscriptionId }))
                            return false;
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public async Task<bool> UpdateUser(UserDetails userDetails)
        {
            try
            {
                if (await _dba.Update(ConvertToUser(userDetails)))
                {
                    var updateUserSubscriptions = userDetails.Subscriptions.Select(s => s.SubscriptionId);

                    var UsrSub = await _dba.GetUserSubscriptions(userDetails.Id);

                    var existingUserSubscriptions = UsrSub.Select(s => s.SubscriptionId);

                    var UserSubscriptionToAdd = updateUserSubscriptions.Except(existingUserSubscriptions);

                    foreach (string subId in UserSubscriptionToAdd)
                        await _dba.Insert(new UserSubscription { UserId = userDetails.Id, SubscriptionId = subId });

                    var UserSubscriptionToDelete = existingUserSubscriptions.Except(updateUserSubscriptions);

                    foreach (string subId in UserSubscriptionToDelete)
                        await _dba.DeleteUserSubscription(subId, userDetails.Id);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public async Task<bool> DeleteUser(int Id)
        {
            try
            {
                return await _dba.DeleteUser(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SubscriptionDetails>> GetSubscriptionsData()
        {
            try
            {
                IEnumerable<Subscription> sub = await _dba.GetSubscriptions();
                return sub.Count() > 0 ? sub.Select(x => ConvertToSubscriptionDetails(x)).ToList() : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<SubscriptionDetails> GetSubscriptionData(string SubscriptionId)
        {
            try
            {
                Subscription sub = await _dba.GetSubscription(SubscriptionId);
                return sub != null ? ConvertToSubscriptionDetails(sub) : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AddSubscription(SubscriptionDetails subDetails)
        {
            try
            {
                return await _dba.Insert(ConvertToSubscription(subDetails));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateSubscription(SubscriptionDetails subDetails)
        {
            try
            {
                return await _dba.Update(ConvertToSubscription(subDetails));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteSubscription(string SubscriptionId)
        {
            try
            {
                return await _dba.DeleteSubscription(SubscriptionId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<UserDetails> ConvertToUserDetails(User user)
        {
            try
            {
                var usrSubs = await _dba.GetUserSubscriptions(user.Id);
                UserDetails userDetails = new UserDetails
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    Subscriptions = usrSubs.Select(s => ConvertToSubscriptionDetails(s.Subscription)).ToList()//user.Subscriptions.Select(s => ConvertToSubscriptionDetails(s)).ToList()
                };

                return userDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        SubscriptionDetails ConvertToSubscriptionDetails(Subscription sub)
        {
            if (sub == null)
                return null;
            SubscriptionDetails subDetails = new SubscriptionDetails
            {
                CallMinutes = sub.CallMinutes,
                Price = sub.Price,
                SubscriptionId = sub.SubscriptionId,
                Name = sub.Name,
                // UserId = sub.UserId,
                VatAmount = sub.VatAmount
            };
            return subDetails;
        }
        User ConvertToUser(UserDetails userDetails)
        {
            User user = new User
            {
                Email = userDetails.Email,
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                Id = userDetails.Id,
                // Subscriptions = userDetails.Subscriptions.Select(s => ConvertToSubscription(s)).ToList()
            };

            return user;
        }
        Subscription ConvertToSubscription(SubscriptionDetails subDetails)
        {
            Subscription sub = new Subscription
            {
                CallMinutes = subDetails.CallMinutes,
                Price = subDetails.Price,
                SubscriptionId = subDetails.SubscriptionId,
                Name = subDetails.Name,
                // UserId = subDetails.UserId,
                VatAmount = subDetails.VatAmount
            };
            return sub;
        }
    }
}
