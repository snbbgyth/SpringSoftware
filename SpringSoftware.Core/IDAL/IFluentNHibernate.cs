using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace SpringSoftware.Core.IDAL
{
    public interface IFluentNHibernate
    {
        ISession GetSession();
    }
}
