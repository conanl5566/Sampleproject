using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Principal;

namespace dotNET.HttpApi.Host
{
    /// <summary>
    /// 会员Id和帐号
    /// </summary>
    public class UserData
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 帐号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 返回当前会员 Id和帐号
        /// </summary>
        /// <returns></returns>
        public static UserData CurrentUser(HttpContext Current)
        {
            try
            {
                GenericPrincipal user = Current.User as GenericPrincipal;
                var gi = user.Identities.First();

                UserData ud = new UserData { Account = gi.Label, Id = Convert.ToInt32(gi.Name) };
                return ud;
            }
            catch
            {
                return null;
            }
        }
    }
}