using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace dotNET.Dto.WebConfig
{
    /// <summary>
    /// 
    /// </summary>
    public class WebConfigOption 
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int PageNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual int RowsPrePage { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public virtual string ConfigKey { get; set; }
    }
}
