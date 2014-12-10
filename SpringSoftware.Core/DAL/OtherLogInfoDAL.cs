using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpringSoftware.Core.DbModel;
using SpringSoftware.Core.IDAL;

namespace SpringSoftware.Core.DAL
{
    public class OtherLogInfoDAL : DataOperationActivityBase<OtherLogInfo>, IOtherLogInfo
    {
        public static OtherLogInfoDAL Current
        {
            get { return new OtherLogInfoDAL(); }
        }
    }
}
