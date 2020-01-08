using Microsoft.AspNetCore.Builder;

namespace dotNET.Web.Host.Framework
{
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// 页面的执行时间
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExecuteTime(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExecuteTimeMiddleware>();
        }
    }
}