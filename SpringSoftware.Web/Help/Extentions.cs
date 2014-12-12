using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpringSoftware.Web.Help
{
    public static class Extentions
    {
        public static string ToSummary(this string content)
        {
            if (string.IsNullOrEmpty(content) || content.Length < 50)
                return content;
            return content.Substring(0, 50) + ".........";
        }

    }
}