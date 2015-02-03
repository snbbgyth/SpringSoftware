using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpringSoftware.Core.DbModel;

namespace SpringSoftware.Web.Models
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            Order=new Order();
            OrderItemViewList = new List<OrderItemViewModel>();
        }

        public Order Order { get; set; }

        public IList<OrderItemViewModel> OrderItemViewList { get; set; }
    }
}