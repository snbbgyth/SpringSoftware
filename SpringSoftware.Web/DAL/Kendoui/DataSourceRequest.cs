using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpringSoftware.Web.DAL.Kendoui
{
    public class DataSourceRequest
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public DataSourceRequest()
        {
            this.Page = 1;
            this.PageSize = 10;
        }
    }
}