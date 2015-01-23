using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpringSoftware.Core.DbModel;

namespace SpringSoftware.Web.Areas.Admin.Models
{
    public class ProductPictureViewModel
    {
        public ProductPictureViewModel()
        {
            ProductPicture = new ProductPicture();
        }

        public ProductPicture ProductPicture { get; set; }

        public string PictureUrl { get; set; }
    }
}