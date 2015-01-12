using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace SpringSoftware.Core.DbModel.Mappings
{
    public class ShopCartItemMapping : ClassMap<ShopCartItem>
    {
        public ShopCartItemMapping()
        {
            Id(x => x.Id).UniqueKey("Id").GeneratedBy.Identity();
            Map(x => x.CreateDate);
            Map(x => x.Creater);
            Map(x => x.IsDelete);
            Map(x => x.LastModifier);
            Map(x => x.LastModifyDate);


            Map(x => x.CustomerId);
            Map(x => x.Count);
            References(x => x.Product, "ProductId").Not.LazyLoad();

        }
    }
}
