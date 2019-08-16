using Microsoft.AspNetCore.Mvc;
using dotNET.HttpApi.Host.Code;

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