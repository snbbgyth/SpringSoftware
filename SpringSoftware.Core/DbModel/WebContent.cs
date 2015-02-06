using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using FluentNHibernate.Testing.Values;
using SpringSoftware.Core.Model;

namespace SpringSoftware.Core.DbModel
{
    public class WebContent:BaseTable
    {
        public WebContent()
        {
            WebContentTypeList = new List<WebContentType>();
        }

        public virtual int WebContentTypeId { get; set; }

        /// <summary>
        /// Each content type just display max display order
        /// </summary>
        [DisplayName("显示顺序")]
        public virtual int DisplayOrder { get; set; }


        [AllowHtml]
        [DisplayName("正文")]
        public virtual string Content { get; set; }

        public virtual WebContentType WebContentType { get; set; }


        public virtual IEnumerable<WebContentType> WebContentTypeList { get; set; }


    }
}
