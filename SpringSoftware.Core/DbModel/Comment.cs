using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SpringSoftware.Core.Model;

namespace SpringSoftware.Core.DbModel
{
    public class Comment:BaseTable
    {
        [DisplayName("电话")]
        [Required]
        public virtual string Phone { get; set; }

        [DisplayName("姓名")]
        [Required]
        public virtual string UserName { get; set; }

        [DisplayName("留言")]
        [Required]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public virtual string Content { get; set; }
    }
}
