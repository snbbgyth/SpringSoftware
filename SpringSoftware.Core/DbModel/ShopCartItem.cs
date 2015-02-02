using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpringSoftware.Core.Model;

namespace SpringSoftware.Core.DbModel
{
    public class ShopCartItem : BaseTable
    {
        public ShopCartItem()
        {
            //Product = new Product();
        }

        public virtual string CustomerName { get; set; }

        [DisplayName("数量")]
        public virtual int Count { get; set; }

        public virtual Product Product { get; set; }

        public virtual int ProductId { get; set; }

        [DisplayName("选择")]

        public virtual bool IsSubmit { get; set; }
    }
}
