using dotNET.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Security.Principal;

namespace dotNET.HttpApi.Host
{
    /// <summary>
    /// 权限
    /// </summary>
    public class BasicAuth : ActionFilterAttribute
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request != null && context.HttpContext.Request.Headers != null && context.HttpContext.Request.Headers["Authorization"].Count > 0)
            {
                var token = context.HttpContext.Request.Headers["Authorization"];
                if (string.IsNullOrWhiteSpace(token))
                {
                    ResultDto meta = ResultDto.Err("Unauthorized");
                    JsonResult json = new JsonResult(new
                    {
                        Meta = meta
                    }
                    );
                    JsonSerializerSettings jsetting = new JsonSerializerSettings();
                    jsetting.NullValueHandling = NullValueHandling.Ignore;
                    jsetting.Converters.Add(
                        new Newtonsoft.Json.Converters.IsoDateTimeConverter()
                        {
                            DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
                        }
                    );
                    json.SerializerSettings = jsetting;
                    json.ContentType = "application/json; charset=utf-8";
                    context.Result = json;
                }
                else
                {
                    GenericIdentity ci = new GenericIdentity(token);
                    ci.Label = "conan1111111";
                    context.HttpContext.User = new GenericPrincipal(ci, null);
                }
            }
            else
            {
                ResultDto meta = ResultDto.Err("Unauthorized");
                JsonResult json = new JsonResult(new
                {
                    Meta = meta
                }
                );
                JsonSerializerSettings jsetting = new JsonSerializerSettings();
                jsetting.NullValueHandling = NullValueHandling.Ignore;
                jsetting.Converters.Add(
                    new Newtonsoft.Json.Converters.IsoDateTimeConverter()
                    {
                        DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
                    }
                );
                json.SerializerSettings = jsetting;
                json.ContentType = "application/json; charset=utf-8";
                context.Result = json;
            }
            base.OnActionExecuting(context);
        }
    }
}