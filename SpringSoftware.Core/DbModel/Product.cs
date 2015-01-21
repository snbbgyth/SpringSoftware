using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SpringSoftware.Core.Model;

namespace SpringSoftware.Core.DbModel
{
    public class Product : BaseTable
    {
        [DisplayName("名称")]
        public virtual string Name { get; set; }

        public virtual ProductType ProductType { get; set; }

        public virtual int ProductTypeId { get; set; }

        [AllowHtml]
        [DisplayName("描述")]
        public virtual string Discrption { get; set; }

        [DisplayName("价格")]
        public virtual double Price { get; set; }

    }
}
