using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpringSoftware.Core.Model;

namespace SpringSoftware.Core.DbModel
{
    public class Picture : BaseTable
    {
        public virtual string MimeType { get; set; }

        public virtual byte[] PictureBinary { get; set; }

        public virtual string FileName { get; set; }

        //public virtual string FileExtension { get; set; }
    }
}
