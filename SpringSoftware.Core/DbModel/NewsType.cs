using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpringSoftware.Core.Model;

namespace SpringSoftware.Core.DbModel
{
    public class NewsType : BaseTable
    {
        [DisplayName("新闻类型")]
        public virtual string Name { get; set; }
    }
}
