using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringSoftware.Core.Help
{
    public class UtilHelper
    {

        public static string SqliteFilePath
        {
            get
            {
                string dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.ConfigFolderTags);
                if (!Directory.Exists(dataDirectory))
                    Directory.CreateDirectory(dataDirectory);
                return Path.Combine(dataDirectory, Constants.SqliteFileNameTags);
            }
        }
    }
}
