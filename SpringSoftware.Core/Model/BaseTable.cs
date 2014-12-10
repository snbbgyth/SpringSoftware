using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringSoftware.Core.Model
{
   public class BaseTable
    {
       public virtual int Id { get; set; }

       public virtual DateTime CreateDate { get; set; }

       public virtual DateTime LastModifyDate { get; set; }

       public virtual bool IsDelete { get; set; }

       public virtual string Creater { get; set; }

       public virtual string LastModifier { get; set; }
    }
}
