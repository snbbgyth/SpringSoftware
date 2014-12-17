using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace SpringSoftware.Core.DbModel.Mappings
{
    public class OtherlogInfoMapping : ClassMap<OtherLogInfo>
    {
        public OtherlogInfoMapping()
        {
            Id(x => x.Id);
            Map(x => x.CreateDate);
            Map(x => x.Creater);
            Map(x => x.IsDelete);
            Map(x => x.LastModifier);
            Map(x => x.LastModifyDate);
            Map(x => x.ClientName);
            Map(x => x.UniqueName);
            Map(x => x.TableName);
            Map(x => x.ClassName);
            Map(x => x.MethodName);
            Map(x => x.LogType);
            Map(x => x.Message);
            Map(x => x.StackTrace);
        }
    }
}
