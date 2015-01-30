using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpringSoftware.Core.Model;

namespace SpringSoftware.Core.DbModel
{
    public class Order : BaseTable
    {
        public virtual string CustomerId { get; set; }

        public virtual string CustomerPhone { get; set; }

        public virtual string ReceiveAddress { get; set; }

        public virtual double TotalPrice { get; set; }

        public virtual bool IsPay { get; set; }

    }
}
