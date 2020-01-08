#region using
using System;
using Microsoft.AspNetCore.Mvc;
using dotNET.Web.Host.Framework;
using dotNET.Core;
using System.IO;
using Microsoft.Net.Http.Headers;
using dotNET.Web.Host.Web.Model;
using Microsoft.Extensions.Configuration;
#endregion

/// <summary>
/// 文件上传
/// </summary>
namespace dotNET.Web.Host.Controllers
{
    public class ImageController : CustomController
    {

     

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="savesize"></param>
        /// <returns></returns>
        [HttpPost]
        [IgnoreAuthorize]
        public IActionResult Img(string savesize = "[]")
        {
            try
            {
                uploadfile user = new uploadfile();
                var files = Request.Form.Files;

                if (files == null || files.Count == 0)
                    return Json(new { jsonrpc = "2.0", error = new { code = 101, message = "没有上传图片" }, id = "id" });

                user.ShareImg = files[0];
                var contentRoot = Directory.GetCurrentDirectory();
                var webRoot = Path.Combine(contentRoot, "wwwroot");
                var parsedContentDisposition = ContentDispositionHeaderValue.Parse(user.ShareImg.ContentDisposition);
                var originalName = parsedContentDisposition.FileName.ToString().Replace("\"", "");
                var ext = Path.GetExtension(Path.Combine(webRoot, originalName));

                if (ext != ".jpg")
                    return Json(new { jsonrpc = "2.0", error = new { code = 101, message = "文件格式错误" }, id = "id" });

                var fileName = Path.Combine("upload", Guid.NewGuid().ToString() + "_" + savesize + ext);

                using (var stream = new FileStream(Path.Combine(webRoot, fileName), FileMode.CreateNew))
                {
                    user.ShareImg.CopyTo(stream);
                }

                FileInfo file = new FileInfo(Path.Combine(webRoot, fileName));
                //缩略图（最后一张生成缩略图）
                var sizeArray = savesize.Substring(savesize.LastIndexOf("[") + 1, savesize.IndexOf("]") - 1).Split('-');
                var thumbnail = "";
                string fn_thum = "";
                string fullname = "";
                int width_thum = 0;
                int height_thum = 0;
                for (int i = 0; i < sizeArray.Length; i++)
                {
                    if (sizeArray[i] != "")
                    {
                        var wh = sizeArray[i].Split('x'); ;
                        if (wh.Length == 2)
                        {
                            //宽
                            width_thum = int.Parse(wh[0]);
                            //高
                            height_thum = int.Parse(wh[1]);
                            fn_thum = fileName.Replace(ext, "_thum" + ext);
                            fullname = Path.Combine(webRoot, fn_thum);
                            thumbnail = "\\" + fn_thum;
                        }

                    }
                }
                ////生成缩略图
                if (thumbnail != "")
                    IMGUtility.Thumbnail(Path.Combine(webRoot, fileName), fullname, width_thum, height_thum, CutMode.None);

                return Content(JsonHelper.SerializeObject(new { jsonrpc = "2.0", result = Path.Combine(webRoot, fileName), id = "id", imgthum = thumbnail }));
            }
            catch (Exception exc)
            {
                NLogger.Error(exc.ToString());
                return Content(JsonHelper.SerializeObject(new { jsonrpc = "2.0", error = new { code = 101, message = exc.Message }, id = "id" }));
            }
        }

        /// <summary>
        /// 富文本框  上传图片  
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [IgnoreAuthorize]
        public IActionResult Ckupload()
        {
            uploadfile user = new uploadfile();
            var files = Request.Form.Files;
            if (files == null || files.Count == 0)
                ViewBag.cc = "no file";
            user.ShareImg = files[0];
            var contentRoot = Directory.GetCurrentDirectory();
            var webRoot = Path.Combine(contentRoot, "wwwroot");
            var parsedContentDisposition = ContentDispositionHeaderValue.Parse(user.ShareImg.ContentDisposition);
            var originalName = parsedContentDisposition.FileName.ToString().Replace("\"", "");
            var ext = Path.GetExtension(Path.Combine(webRoot, originalName));
            //   if (ext != ".jpg")
            //   return Json(new { jsonrpc = "2.0", error = new { code = 101, message = "文件格式错误" }, id = "id" });
            string gid = Guid.NewGuid().ToString();
            var fileName = Path.Combine("upload", gid + ext);
            var fileName2 = "upload/" + gid + ext;
            using (var stream = new FileStream(Path.Combine(webRoot, fileName), FileMode.CreateNew))
            {
                user.ShareImg.CopyTo(stream);
            }
            string output = @"<script type=""text/javascript"">window.parent.CKEDITOR.tools.callFunction({0} ,'{1}');</script>";
            string url = "http://" + Request.Host.Value;
            output = string.Format(output, Request.Query["CKEditorFuncNum"], url + "/" + fileName2);
            ViewBag.cc = output;
            return View();
        }
    }
}