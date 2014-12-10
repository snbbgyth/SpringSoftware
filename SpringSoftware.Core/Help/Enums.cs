using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringSoftware.Core.Help
{
    public enum LogMessageType
    {
        /// <summary>
        /// unknown type 
        /// </summary>
        Unknown,

        /// <summary>
        /// information type
        /// </summary>
        Information,

        /// <summary>
        /// User operation type
        /// </summary>
        UserOperation,

        /// <summary>
        /// warning type
        /// </summary>
        Warning,

        /// <summary>
        /// error type
        /// </summary>
        Error,

        /// <summary>
        /// Process runing
        /// </summary>
        Runing,

        /// <summary>
        /// success type
        /// </summary>
        Success
    }

}
