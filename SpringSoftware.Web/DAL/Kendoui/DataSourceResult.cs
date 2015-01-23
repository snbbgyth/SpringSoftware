using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpringSoftware.Web.DAL.Kendoui
{
    public class DataSourceResult
    {
        public object ExtraData { get; set; }

        public IEnumerable Data { get; set; }

        public object Errors { get; set; }

        public int Total { get; set; }
    }
}