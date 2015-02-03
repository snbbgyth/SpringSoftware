using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpringSoftware.Core.DbModel;

namespace SpringSoftware.Web.Models
{
    public class OrderItemViewModel
    {
        public OrderItem OrderItem { get; set; }

        public ShopCartItem ShopCartItem { get; set; }
    }
}