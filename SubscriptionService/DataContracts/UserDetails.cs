using System.Collections.Generic;
using System.Runtime.Serialization;


namespace SubscriptionService.DataContract
{
   
    [DataContract]
    public class UserDetails
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public virtual List<SubscriptionDetails> Subscriptions { get; set; }
    }
}
