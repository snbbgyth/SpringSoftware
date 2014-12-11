using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SpringSoftware.Core.Model;

namespace SpringSoftware.Core.DbModel
{
   public  class News:BaseTable
    {
       public virtual string Title { get; set; }

       [AllowHtml]
       [UIHint("tinymce_jquery_full")]
       public virtual string Content { get; set; }

       public virtual bool IsPublish { get; set; }

       public virtual int NewsTypeId { get; set; }
    }
}
