using Microsoft.AspNetCore.Mvc;

namespace dotNET.HttpApi.Host.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>

    [Route("api/Values")]
    public class ValuesController : DefaultController
    {
        ////public IItemsDataApp _AreaListApp { get; set; }

        //////private readonly IItemsDataApp _AreaListApp;
        //////public ValuesController(IItemsDataApp AreaListApp)
        //////{
        //////    _AreaListApp = AreaListApp;
        //////}

        /////// <summary>
        /////// 帐号注册
        /////// </summary>
        /////// <remarks>
        /////// Sample request:
        ///////
        ///////     POST /Todo
        ///////     {
        ///////        "id": 1,
        ///////        "name": "Item1",
        ///////        "isComplete": true
        ///////     }
        ///////
        /////// </remarks>
        /////// <param name="model">测试</param>
        /////// <returns>测试</returns>
        ////[BasicAuth]
        ////[ModelValidationAttribute]
        ////[ApiExplorerSettings(GroupName = "v1")]
        //////[AllowAnonymous]
        ////[HttpPost, Route("register")]
        ////public async Task<R<Login>> registerAsync([FromBody]Register model)
        ////{
        ////    await _AreaListApp.GetItemsDataListAsync();
        ////    UserData ud = UserData.CurrentUser(this.HttpContext);
        ////    Login Login = new Login();
        ////    Login.Account = model.Account + "-" + model.Code;
        ////    return R<Login>.Suc(Login);

        ////}
        /////// <summary>
        /////// 检测帐号是不已存在
        /////// </summary>
        /////// <param name="account">(必填)帐号或手机号    Data=true 已存在,Data=false 不存在</param>
        /////// <returns>测试</returns>
        ////[HttpGet, Route("existAccount")]
        ////[ApiExplorerSettings(GroupName = "v2")]
        //////[HiddenApi]
        ////public R<bool> ExistAccount([FromQuery] string account)
        ////{
        ////    return R<bool>.Suc(true);
        ////}
    }
}