using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SpringSoftware.Core.Model;

namespace SpringSoftware.Core.DbModel
{
   public class Product:BaseTable
    {
       public virtual string Name { get; set; }

       public virtual ProductType ProductType { get; set; }

       [AllowHtml]
       [DisplayName("描述")]
       public virtual string Discrption { get; set; }

       public virtual double Price { get; set; }

    }
}
