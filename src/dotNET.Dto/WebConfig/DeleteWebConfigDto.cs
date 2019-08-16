using dotNET.Dto.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace dotNET.Dto.WebConfig
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteWebConfigDto
    {

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public virtual long Id { get; set; }
    }
}
