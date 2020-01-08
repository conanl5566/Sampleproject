using dotNET.Application;
using dotNET.Application.Sys;
using dotNET.Core;
using dotNET.Dto.WebConfig;
using dotNET.HttpApi.Host.Code;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace dotNET.HttpApi.Host.Controllers
{
    /// <summary>
    /// 配置信息
    /// </summary>

    [Route("api/WebConfig")]
    public class WebConfigController : DefaultController
    {
        /// <summary>
        ///
        /// </summary>
        public IWebConfigApp _webConfigApp { get; set; }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns>主键id</returns>
        [BasicAuth]
        [ModelValidationAttribute]
        [ApiExplorerSettings(GroupName = "v1")]
        [HttpPost, Route("Create")]
        public async Task<R<long>> CreateAsync([FromBody]CreateWebConfigDto model)
        {
            return await _webConfigApp.CreateAsync(model, new Core.CurrentUser());
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [BasicAuth]
        [ModelValidationAttribute]
        [ApiExplorerSettings(GroupName = "v1")]
        [HttpPost, Route("Update")]
        public async Task<R> UpdateAsync([FromBody]UpdateWebConfigDto model)
        {
            return await _webConfigApp.UpdateAsync(model, new Core.CurrentUser());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [BasicAuth]
        [ModelValidationAttribute]
        [ApiExplorerSettings(GroupName = "v1")]
        [HttpPost, Route("Delete")]
        public async Task<R> DeleteAsync([FromBody]DeleteWebConfigDto model)
        {
            return await _webConfigApp.DeleteAsync(model.Id, new CurrentUser());
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [BasicAuth]
        [ModelValidationAttribute]
        [ApiExplorerSettings(GroupName = "v1")]
        [HttpGet, Route("GetPage")]
        public async Task<R<PageResult<WebConfigDto>>> GetPageAsync([FromQuery]WebConfigOption model)
        {
            return await _webConfigApp.GetPageAsync(model);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [BasicAuth]
        [ModelValidationAttribute]
        [ApiExplorerSettings(GroupName = "v1")]
        [HttpGet, Route("GetDetail")]
        public async Task<R<WebConfigDto>> GetDetailAsync([FromQuery]long id)
        {
            return await _webConfigApp.GetDetailAsync(id);
        }
    }
}