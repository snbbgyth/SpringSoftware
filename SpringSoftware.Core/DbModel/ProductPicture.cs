using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpringSoftware.Core.Model;

namespace SpringSoftware.Core.DbModel
{
    public class ProductPicture : BaseTable
    {
        public ProductPicture()
        {

        }

        public virtual int DisplayOrder { get; set; }

        public virtual int PictureId { get; set; }

        public virtual int ProductId { get; set; }

    }
}
