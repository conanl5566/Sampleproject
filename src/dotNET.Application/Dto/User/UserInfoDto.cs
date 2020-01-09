namespace dotNET.ICommonServer
{
    /// <summary>
    /// 修改用户信息
    /// </summary>
    public class UserInfoDto
    {
        public long Id { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long? DepartmentId { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public long RoleId { get; set; }

        public string Tel { get; set; }

        public string RealName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool IsSys { get; set; }

        /// <summary>
        /// 状态 1：正常，2：禁止登录
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 密码错误次数（正确一次后清0）
        /// </summary>
        public int PasswordErrorCount { get; set; } = 0;

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
    }
}