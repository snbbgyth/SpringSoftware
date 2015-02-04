using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpringSoftware.Core.Model;

namespace SpringSoftware.Core.DbModel
{
   public  class AliPayPaymentSettings:BaseTable
    {
        public virtual string SellerEmail { get; set; }
        public virtual string Key { get; set; }
        public virtual string Partner { get; set; }
        public virtual decimal AdditionalFee { get; set; }
    }
}
