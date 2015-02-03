using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpringSoftware.Core.Model;

namespace SpringSoftware.Core.DbModel
{
    public class Order : BaseTable
    {
        [DisplayName("用户名")]
        public virtual string CustomerName { get; set; }

        [DisplayName("联系电话")]
        public virtual string CustomerPhone { get; set; }

        [DisplayName("联系地址")]
        public virtual string ReceiveAddress { get; set; }

        [DisplayName("总金额")]
        public virtual double TotalPrice { get; set; }

        [DisplayName("是否付款")]
        public virtual bool IsPay { get; set; }

        [DisplayName("订单编号")]
        public virtual string OrderNumber { get; set; }
    }
}
