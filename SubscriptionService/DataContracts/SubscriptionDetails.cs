using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SubscriptionService.DataContract
{
    [DataContract]
    public  class SubscriptionDetails
    {
        [DataMember]
        public string SubscriptionId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public decimal VatAmount { get; set; }

        [DataMember]
        public decimal CallMinutes { get; set; }

        //[DataMember]
        //public int? UserId { get; set; }
    }
}
