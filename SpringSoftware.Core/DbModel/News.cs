using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpringSoftware.Core.Model;

namespace SpringSoftware.Core.DbModel
{
   public  class News:BaseTable
    {
       public virtual string Title { get; set; }

       public virtual string Content { get; set; }

       public virtual bool IsPublish { get; set; }

       public virtual int NewsTypeId { get; set; }
    }
}
