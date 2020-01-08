/**************************************************************************
 * 作者：X
 * 日期：2017.01.18
 * 描述：
 * 修改记录：
 * ***********************************************************************/

using dotNET.Dto;
using dotNET.Dto.Enum;
using System;
using System.ComponentModel;

namespace dotNET.Domain.Entities.Sys
{
    public class Role : Entity, IEntity
    {
        /// <summary>
        ///
        /// </summary>
        [Description("名称")]
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Description("允许编辑")]
        public bool? AllowEdit { get; set; } = false;

        /// <summary>
        ///
        /// </summary>
        [Description("允许删除")]
        public bool? AllowDelete { get; set; } = false;

        /// <summary>
        ///
        /// </summary>
        [Description("排序")]
        public int? SortCode { get; set; } = 0;

        /// <summary>
        ///
        /// </summary>
        [Description("描述")]
        public string Description { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Description("创建时间")]
        public DateTime CreatorTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 生成ID
        /// </summary>
        /// <returns></returns>

        public long CreateId()
        {
            return base.CreateId(EntityEnum.Role);
        }
    }
}