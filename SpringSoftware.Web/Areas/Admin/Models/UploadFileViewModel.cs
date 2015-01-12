using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SpringSoftware.Web.Areas.Admin.Models
{
    public class UploadFileViewModel
    {
        [DisplayName("选择上传文件")]
        public HttpPostedFileBase File { get; set; }
    }
}