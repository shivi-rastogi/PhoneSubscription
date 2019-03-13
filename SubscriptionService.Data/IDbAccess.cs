using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Data
{
    public interface IDbAccess 
    {
        #region User table operations

         Task<List<User>> GetUsers();

         Task<User> GetUser(int id);

         Task<bool> Insert(User usr);

         Task<bool> Update(User usr);

         Task<bool> DeleteUser(int Id);

        #endregion

        #region Subscription table operations

         Task<List<Subscription>> GetSubscriptions();
         Task<Subscription> GetSubscription(string id);

         Task<bool> Insert(Subscription sub);

         Task<bool> Update(Subscription sub);

         Task<bool> DeleteSubscription(string Id);


        #endregion

        #region UserSubscription table operations
        Task<List<UserSubscription>> GetUserSubscriptions(int UserId);

         Task<bool> Insert(UserSubscription usrSub);

         Task<bool> DeleteUserSubscription(string subId, int UserId);
        #endregion
    }
}
