using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;

namespace dotNET.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
        /// <summary>
        /// API信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApis()
        {
            return new[]
            {
                new ApiResource("dotNETapi", "dotNET.WebApi")
                {
                    UserClaims =  { ClaimTypes.Name, JwtClaimTypes.Name }
                }
            };
        }
        /// <summary>
        /// 客户端信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients(string identityUrl)
        {
            //var host = "http://localhost";
            //if (isProduction)
            //{
            //    host = "http://demo.dotNET.me";  //切换为自己的服务器信息
            //}
            return new[]
            {
                //new Client
                //{
                //    ClientId = "dotNET.WebApi",//客户端名称
                //    ClientName = "开源版webapi认证",//客户端描述
                //    AllowedGrantTypes = GrantTypes.Implicit,//Implicit 方式
                //    AllowAccessTokensViaBrowser = true,//是否通过浏览器为此客户端传输访问令牌
                //    RedirectUris =
                //    {
                //        $"{host}:52789/swagger/oauth2-redirect.html"
                //    },
                //    AllowedScopes = { "dotNETapi" }
                //},
                new Client
                {
                    ClientId = "dotNET.Mvc",
                    ClientName = "开源版mvc认证",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    // 登录成功回调处理地址，处理回调返回的数据
                    RedirectUris = { $"{identityUrl}/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { $"{identityUrl}/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "dotNETapi"
                    }
                }
             
            };
        }
    }
}