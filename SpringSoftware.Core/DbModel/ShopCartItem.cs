using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpringSoftware.Core.Model;

namespace SpringSoftware.Core.DbModel
{
    public class ShopCartItem : BaseTable
    {
        public virtual string CustomerId { get; set; }

        public virtual int Count { get; set; }

        public virtual Product Product { get; set; }
    }
}
