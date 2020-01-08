using dotNET.HttpApi.Host.Code;
using Microsoft.AspNetCore.Mvc;

namespace dotNET.HttpApi.Host.Controllers
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