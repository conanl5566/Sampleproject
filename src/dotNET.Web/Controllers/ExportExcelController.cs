#region using

using dotNET.ICommonServer;
using dotNET.ICommonServer.Sys;
using dotNET.Core;
using dotNET.Web.Host.Framework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

#endregion using

namespace dotNET.Web.Host.Controllers
{
    public class ExportExcelController : CustomController
    {
        #region ini

        public IUserApp UserApp { get; set; }

        #endregion ini

        #region 用户导出

        [HttpPost]
        public async Task<ActionResult> ExportCommonOrder(UserOption filter)
        {
            DefaultPageSize = 100000;
            var currentPageNum = 1;
            var result = await UserApp.GetPageAsync(currentPageNum, DefaultPageSize, filter);
            var list = result.Data.ToList();
            //string dddd = "";
            //dddd += "a|1,";
            //dddd += "b|11;";

            //dddd += "a|12,";
            //dddd += "b|112;";

            //string dddd2 = "";
            //dddd2 += "a|1,";
            //dddd2 += "b|11;";

            //dddd2 += "a|12,";
            //dddd2 += "b|112;";

            //List<ss> dd = new List<ss>();
            //dd.Add(new Controllers.ss() { StoreName = "辅导辅导", DepartmentName = "sdds4343", ww = dddd });

            //dd.Add(new Controllers.ss() { StoreName = "辅导辅导2", DepartmentName = "sdds43432", ww = dddd2 });

            string format = "Id|Id;Account|帐号;RealName|名称;Tel|联系电话;RoleName|角色;deptname|部门;LastLoginTime|上次登录时间;State|状态";
            //string str = "0-2|合计;3-4|合计;5-5|500.00";
            //string str2 = "4-4|合计;5-5|500.00";
            //List<string> format2 = new List<string>();
            //format2.Add(str);
            //format2.Add(str2);
            string reportName = "用户";
            string path = "";
            string gp = Path.Combine("Export", Guid.NewGuid().ToString());
            var newFile = Path.Combine(Path.Combine("wwwroot", gp), reportName + @".xlsx");
            new FileHelper().CreateFiles(Path.Combine("wwwroot", gp), true);
            path = Path.Combine(gp, reportName + @".xlsx");
            TaskEx.Run(() =>
           {
               Export.ExportExcel<UserSunpleDto>(list, format, reportName, newFile);
           });
            ViewBag.test = path;
            return Operation(true, Base64.StringToBase64(path));
        }

        #endregion 用户导出

        #region 加载弹窗（loader）

        public ActionResult SelectFile(string File)
        {
            ViewBag.File = File;
            return View();
        }

        #endregion 加载弹窗（loader）

        #region 判断文件是否存在

        [HttpPost]
        public ActionResult FileExists(string File)
        {
            var contentRoot = Directory.GetCurrentDirectory();
            var webRoot = Path.Combine(contentRoot, "wwwroot");
            if (System.IO.File.Exists(Path.Combine(webRoot, Base64.Base64ToString(File))))
            {
                if (!IsFileInUse(Path.Combine(webRoot, Base64.Base64ToString(File))))
                {
                    return Json(new { Exists = true, File = "\\" + Base64.Base64ToString(File) });
                }
                else
                {
                    return Json(new { Exists = false });
                }
            }
            else
            {
                return Json(new { Exists = false });
            }
        }

        /// <summary>
        ///判断文件是否在生成中
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsFileInUse(string fileName)
        {
            bool inUse = true;
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read,
                FileShare.None);
                inUse = false;
            }
            catch
            {
            }
            finally
            {
                fs?.Dispose();
            }
            return inUse;//true表示正在使用,false没有使用
        }

        #endregion 判断文件是否存在
    }
}