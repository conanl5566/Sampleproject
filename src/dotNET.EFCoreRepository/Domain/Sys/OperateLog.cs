/**************************************************************************
 * 作者：X
 * 日期：2017.01.18
 * 描述：
 * 修改记录：
 * ***********************************************************************/

using dotNET.Dto;
using dotNET.Dto.Enum;
using System;

namespace dotNET.Domain.Entities.Sys
{
    /// <summary>
    /// 操作日志
    /// </summary>
    public class OperateLog : Entity, IEntity
    {
        /// <summary>
        /// 操作者
        /// </summary>
        public virtual string Operator { get; set; }

        /// <summary>
        /// 操作者id
        /// </summary>
        public virtual long OperatorId { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public virtual string Tag { get; set; }

        /// <summary>
        /// ip
        /// </summary>
        public virtual string IP { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public virtual string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 生成ID
        /// </summary>
        /// <returns></returns>
        public long CreateId()
        {
            return base.CreateId(EntityEnum.OperateLog);
        }
    }
}