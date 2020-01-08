using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace dotNET.Web.Host.Framework
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class IgnoreAuthorizeAttribute : ActionFilterAttribute
    {
        public IgnoreAuthorizeAttribute()
        {
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AllowAttribute : ActionFilterAttribute
    {
        public AllowAttribute()
        {
        }
    }
}