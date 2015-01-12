using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace SpringSoftware.Core.DbModel.Mappings
{
    public class ProductMapping : ClassMap<Product>
    {
        public ProductMapping()
        {
            Id(x => x.Id).UniqueKey("Id").GeneratedBy.Identity();
            Map(x => x.CreateDate);
            Map(x => x.Creater);
            Map(x => x.IsDelete);
            Map(x => x.LastModifier);
            Map(x => x.LastModifyDate);
            Map(x => x.Name);
            Map(x => x.Discrption).Length(5000);
            Map(x => x.Price);
            References(x => x.ProductType, "ProductTypeId").Not.LazyLoad();
        }
    }
}
