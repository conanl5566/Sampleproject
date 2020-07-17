/**************************************************************************
 * 作者：X
 * 日期：2017.01.19
 * 描述：Entity
 * 修改记录：
 * ***********************************************************************/

using CompanyName.ProjectName.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace CompanyName.ProjectName.Core
{
    public abstract class Entity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key, Required]
        public virtual long Id { get; set; }
    }
}