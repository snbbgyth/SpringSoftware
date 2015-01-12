using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using FluentNHibernate.MappingModel.ClassBased;
using SpringSoftware.Core.Model;
using FluentNHibernate;

namespace SpringSoftware.Core.DbModel
{
   public  class News:BaseTable
    {
       [DisplayName("标题")]
       public virtual string Title { get; set; }

       [AllowHtml]
       [DisplayName("正文")]
       public virtual string Content { get; set; }

       [DisplayName("是否发布")]
       public virtual bool IsPublish { get; set; }
 
       public  virtual NewsType NewsType { get; set; }

       public virtual IEnumerable<NewsType> NewsTypeList { get; set; }

       public News()
       {
         
       }
    }
}
