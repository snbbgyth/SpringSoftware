using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace SpringSoftware.Core.DbModel.Mappings
{
    public class NewsMapping : ClassMap<News>
    {
        public NewsMapping()
        {
            Id(x => x.Id);
            Map(x => x.CreateDate);
            Map(x => x.Creater);
            Map(x => x.IsDelete);
            Map(x => x.LastModifier);
            Map(x => x.LastModifyDate);


            Map(x => x.IsPublish);
            Map(x => x.Content);
            //Map(x => x.NewsTypeId);
            Map(x => x.Title);
            References(x => x.NewsType, "NewsTypeId").Not.LazyLoad();

            HasManyToMany(x => x.NewsTypeList)
              .Cascade.All()
              .Inverse()
              .Table("NewsType");
        }
    }
}
