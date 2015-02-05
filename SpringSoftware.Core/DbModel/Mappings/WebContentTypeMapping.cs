using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using SpringSoftware.Core.Model;

namespace SpringSoftware.Core.DbModel.Mappings
{
    public class WebContentTypeMapping : ClassMap<WebContentType>
    {
       public WebContentTypeMapping()
       {
           Id(x => x.Id).UniqueKey("Id").GeneratedBy.Identity();
           Map(x => x.CreateDate);
           Map(x => x.Creater);
           Map(x => x.IsDelete);
           Map(x => x.LastModifier);
           Map(x => x.LastModifyDate);

           Map(x => x.Name);
       }
    }
}
