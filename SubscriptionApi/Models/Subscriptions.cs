using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SubscriptionApi.Models
{
    public class Subscriptions
    {
        public string id { get; set; }

        public string name { get; set; }

        public decimal price { get; set; }
        
        public decimal priceIncVatAmount { get; set; }
        
        public decimal callminutes { get; set; }
        
       // public int UserId { get; set; }
    }
}