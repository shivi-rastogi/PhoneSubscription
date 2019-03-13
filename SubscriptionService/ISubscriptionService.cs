using SubscriptionService.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ISubscriptionService
    {
        [OperationContract]
         Task<List<UserDetails>> GetUsersData();

        [OperationContract]
        Task<UserDetails> GetUserData(int Id);

        [OperationContract]
        Task<bool> AddUser(UserDetails user);

        [OperationContract]
        Task<bool> UpdateUser(UserDetails user);

        [OperationContract]
        Task<bool> DeleteUser(int Id);

        [OperationContract]
        Task<List<SubscriptionDetails>> GetSubscriptionsData();

        [OperationContract]
        Task<SubscriptionDetails> GetSubscriptionData(string SubscriptionId);

        [OperationContract]
        Task<bool> AddSubscription(SubscriptionDetails subDetails);

        [OperationContract]
        Task<bool> UpdateSubscription(SubscriptionDetails subDetails);

        [OperationContract]
        Task<bool> DeleteSubscription(string SubscriptionId);
    }


   
}
