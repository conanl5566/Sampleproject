using dotNET.Application;
using dotNET.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace dotNET.HttpApi.Host.Code
{
    /// <summary>
    ///
    /// </summary>
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var request = context.HttpContext.Request;
            string url = request.Path + (request.QueryString.HasValue ? $"?{request.QueryString.Value}" : "");
            NLogger.Error("" + url + "\r\n" + context.Exception.Message + "\r\n" + context.Exception.StackTrace + "");
            //context.ExceptionHandled = true;
            R Meta = R.Err();
            JsonResult json = new JsonResult(new
            {
                Meta
            }
            );
            JsonSerializerSettings jsetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            jsetting.Converters.Add(
                new Newtonsoft.Json.Converters.IsoDateTimeConverter()
                {
                    DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
                }
            );
            json.SerializerSettings = jsetting;
            json.ContentType = "application/json; charset=utf-8";
            context.Result = json;
            return base.OnExceptionAsync(context);
        }
    }
}