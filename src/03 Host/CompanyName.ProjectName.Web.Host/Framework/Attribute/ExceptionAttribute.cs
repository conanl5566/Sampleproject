﻿#region using

using CompanyName.ProjectName.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

#endregion using

namespace CompanyName.ProjectName.Web.Host.Framework
{
    /// <summary>
    /// 统一错误处理
    /// </summary>
    public class ExceptionAttribute : ExceptionFilterAttribute, IExceptionFilter
    {
        //private readonly ILogger _logger = null;

        //public ExceptionAttribute(ILoggerFactory loggerFactory)
        //{
        //    _logger = loggerFactory.CreateLogger("Exception");
        //}

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var request = context.HttpContext.Request;
            string url = request.Path + (request.QueryString.HasValue ? $"?{request.QueryString.Value}" : "");
            NLogger.Error("" + url + "\r\n" + context.Exception.Message + "\r\n" + context.Exception.StackTrace + "");

            //context.ExceptionHandled = true;
            context.Result = new JsonResult(new { IsSucceeded = false, Message = "操作失败" });
            return base.OnExceptionAsync(context);
        }
    }
}