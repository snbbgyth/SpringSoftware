using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpringSoftware.Core.Model;

namespace SpringSoftware.Core.DbModel
{
    public class OtherLogInfo:BaseTable
    {
     
        public virtual string ClientName { get; set; }
        public virtual string UniqueName { get; set; }
        public virtual string TableName { get; set; }
        public virtual string ClassName { get; set; }
        public virtual string MethodName { get; set; }
        public virtual string LogType { get; set; }
        public virtual string Message { get; set; }
        public virtual string StackTrace { get; set; }
        public virtual DateTime CreateTime { get; set; }
    }
}
