using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpringSoftware.Core.DbModel;
using SpringSoftware.Core.IDAL;

namespace SpringSoftware.Core.DAL
{
    public class OtherLogInfoDal : DataOperationActivityBase<OtherLogInfo>, IOtherLogInfo
    {
        public static OtherLogInfoDal Current
        {
            get { return new OtherLogInfoDal(); }
        }
    }
}
