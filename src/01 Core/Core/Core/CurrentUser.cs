using Newtonsoft.Json;

namespace CompanyName.ProjectName.Core
{
    /// <summary>
    /// 当前用户
    /// </summary>
    public class CurrentUser
    {
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public bool IsSys { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// ip地址
        /// </summary>
        public string LoginIPAddress { get; set; }

        /// <summary>
        ///
        /// </summary>
        public long AgentId { get; set; }

        /// <summary>
        /// 登录类型 Saas,Agent,Member
        /// </summary>
        public string UserType { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //public string LoginToken { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //public DateTime LoginTime { get; set; }

        /// <summary>
        ///
        /// </summary>
        //   public List<RoleAuthorize> RoleAuthorizelist  { get; set; }

        public override string ToString()
        {
            return Des.EncryptDES(Base64.StringToBase64(JsonConvert.SerializeObject(this)));
        }

        public static CurrentUser FromJson(string json)
        {
            return JsonHelper.DeserializeObject<CurrentUser>(Base64.Base64ToString(Des.DecryptDES(json)));
        }
    }
}