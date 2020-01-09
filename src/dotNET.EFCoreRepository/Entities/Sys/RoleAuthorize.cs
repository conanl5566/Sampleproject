/**************************************************************************
 * 作者：X
 * 日期：2017.01.18
 * 描述：
 * 修改记录：
 * ***********************************************************************/

using dotNET.CommonServer;
using dotNET.Core;
using dotNET.Enum;
using System;

namespace dotNET.CommonServer
{
    public class RoleAuthorize : Entity, IEntity
    {
        /// <summary>
        /// 项目类型 1-模块 2-按钮 3-列表
        /// </summary>
        public int? ItemType { get; set; }

        /// <summary>
        ///
        /// </summary>
        public long ItemId { get; set; }

        /// <summary>
        /// 对象分类 1-角色 2-部门 3-用户
        /// </summary>
        public int? ObjectType { get; set; }

        /// <summary>
        ///
        /// </summary>
        public long ObjectId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 生成ID
        /// </summary>
        /// <returns></returns>
        public long CreateId()
        {
            return base.CreateId(EntityEnum.RoleAuthorize);
        }
    }
}