using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpringSoftware.Core.Model;

namespace SpringSoftware.Core.DbModel
{
   public  class NewsType:BaseTable
    {
       public virtual string Name { get; set; }
    }
}
