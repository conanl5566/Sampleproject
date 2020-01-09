using CompanyName.ProjectName.HttpApi.Host.Code;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.ProjectName.HttpApi.Host.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    [LogAttribute]
    [CustomExceptionFilterAttribute]
    public class DefaultController : Controller
    {
    }
}