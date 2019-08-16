using System;

namespace conan.Domain.Entitiestt
{
    /// <summary>
    /// 管理员
    /// </summary>
    public class User : Entity, IEntity
    {
        /// <summary>
        /// 帐号
        /// </summary>
        public string names { get; set; }
     

        /// <summary>
        /// 生成ID
        /// </summary>
        /// <returns></returns>
        public long CreateId()
        {
            return base.CreateId(EntityEnum.User);
        }
    }
}
