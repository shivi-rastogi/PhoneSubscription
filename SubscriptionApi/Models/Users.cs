using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SubscriptionApi.Models
{
    public class Users
    {
        public int id { get; set; }
        
        public string firstname { get; set; }
        
        public string lastname { get; set; }
        
        public string email { get; set; }
        
        public virtual List<Subscriptions> subscriptions { get; set; }

        public decimal totalPriceIncVatAmount { get; set; }

        public decimal totalcallminutes { get; set; }
    }
}