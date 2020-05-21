using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;

namespace CompanyName.ProjectName.Web.Host.Framework
{
    public class AjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        public bool Ignore { get; set; }

        public AjaxOnlyAttribute(bool ignore = false)
        {
            Ignore = ignore;
        }

        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            if (Ignore)
                return true;

            var request = routeContext.HttpContext.Request;
            if (request != null && request.Headers != null && request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return true;

            return false;
        }
    }
}