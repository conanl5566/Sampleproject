using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace dotNET.Dto.WebConfig
{
    /// <summary>
    /// 
    /// </summary>
    public class WebConfigDto : UpdateWebConfigDto
    {
       

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatorTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? CreatorUserId { get; set; }

    }
}
