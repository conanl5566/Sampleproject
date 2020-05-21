#region using

using CompanyName.ProjectName.ICommonServer.Sys;
using CompanyName.ProjectName.Core;
using CompanyName.ProjectName.Web.Host.Framework;
using CompanyName.ProjectName.Web.Host.Web.Model;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.IO;
using System.Threading.Tasks;

#endregion using

namespace CompanyName.ProjectName.Web.Host.Controllers
{
    public class demoController : CustomController
    {
        public IAreaListApp AreaListApp { get; set; }
        public IWebConfigApp WebConfigApp { get; set; }

        #region 发邮件

        public async Task<IActionResult> sendmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> sendmail(string mailFrom, string mailTo, string Text)
        {
            //  string mailFrom = "373197550@qq.com";
            //   string mailTo = "yilinglin@dj.cn";
            string mailFromAccount = "373197550@qq.com";
            string mailPassword = "boajozowmkxvcbab";
            var contentRoot = Directory.GetCurrentDirectory();
            var webRoot = Path.Combine(contentRoot, "wwwroot");
            string path = Path.Combine(webRoot, "Images/icc.png");

            //  string path = @"E:\bbbbbbbbbbbbbbbbbbbb\wwwroot\twoiconcode.jpg";

            //  string Text = @"Hey Chandler,

            //I just wanted to let you know that Monica and I were going to go play some paintball, you in?

            //-- Joey";

            Config eConfig = new Config
            {
                From = new MailAddress("conan 测试发送", mailFrom),
                Host = "smtp.qq.com",
                MailFromAccount = mailFromAccount,
                MailPassword = mailPassword,
                Port = 587,
                UseSsl = false,
                IsHtml = false
            };

            List<MailAddress> tos = new List<MailAddress>();
            tos.Add(new MailAddress("conan 测试接受", mailTo));
            await Mailhelper.SendEmailAsync(eConfig, tos, "How you doin'?", Text, path);

            return View();
        }

        #endregion 发邮件

        #region 富文本框

        public async Task<IActionResult> ck()
        {
            demock model = new demock();

            return View(model);
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult ck(demock Text)
        {
            NLogger.Info(Text.rules);
            NLogger.Info(Base64.Base64ToString(Text.rules));
            return Operation(true, Text.rules);
        }

        #endregion 富文本框

        /// <summary>
        /// 分布式服务Hangfire
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> testHangfire()
        {
            //var s=  await  _webConfigApp.CreateAsync(new Dto.WebConfig.CreateWebConfigDto() {
            //     ConfigKey="test",
            //     ConfigValue="test",
            //     ConfigType= Dto.Enum.ConfigEnvironmentEnum.Pro,
            //     ConfigDetail="test"
            //  },new CurrentUser());

            //for (int i = 0; i < 1000; i++)
            //{
            //    BackgroundJob.Enqueue(() => AreaListApp.Test2Async());
            //    BackgroundJob.Enqueue(() => AreaListApp.TestAsync());

            //}

            //  var jobId = BackgroundJob.Enqueue<IAreaListApp>(i => i.TestAsync());
            // var jobId2 = BackgroundJob.Schedule<IAreaListApp>(i => i.GetListAsync(0),TimeSpan.FromMinutes(10));
            //var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget!"));
            //var jobId2 = BackgroundJob.Schedule(() => Console.WriteLine("Delayed!"), TimeSpan.FromDays(7));
            RecurringJob.AddOrUpdate<IAreaListApp>(i => i.GetListAsync(0), "0 0/1 * * * ? ", queue: "test");
            // ViewBag.JobStr = "jobId:" + jobId + "--jobId2:" + jobId2;
            return View();
        }

        #region 二维码

        public async Task<IActionResult> qrcode()
        {
            string level = "Q";
            string url = "342434uiiiiiiiiiiiiiiiiiiiiiiiiiii3放到";
            QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)(level == "L" ? 0 : level == "M" ? 1 : level == "Q" ? 2 : 3);
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, eccLevel))
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {
                        Bitmap Image = qrCode.GetGraphic(10, Color.Black, Color.White, GetIconBitmap(), (int)8);
                        var contentRoot = Directory.GetCurrentDirectory();
                        var webRoot = Path.Combine(contentRoot, "wwwroot");

                        Image.Save(Path.Combine(webRoot, "twoiconcode.jpg"));
                    }
                }
            }

            ViewBag.iconcode = @"\twoiconcode.jpg";
            return View();
        }

        public ActionResult getqrcode()
        {
            string data = "32323dewds放到";
            string level = "L";
            QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)(level == "L" ? 0 : level == "M" ? 1 : level == "Q" ? 2 : 3);
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, eccLevel))
                {
                    using (BitmapByteQRCode qrcode = new BitmapByteQRCode(qrCodeData))
                    {
                        return File(qrcode.GetGraphic(4), "images/jpeg");
                    }
                }
            }
        }

        public ActionResult geticonqrcode()
        {
            return View();
        }

        public static Bitmap GetIconBitmap()
        {
            Bitmap img = null;
            try
            {
                var contentRoot = Directory.GetCurrentDirectory();
                var webRoot = Path.Combine(contentRoot, "wwwroot");
                string directory = Path.Combine(webRoot, "Images/icc.png");

                img = new Bitmap(directory);
            }
            catch (Exception)
            {
            }

            return img;
        }

        #endregion 二维码

        #region 图形验证码

        /// <summary>
        /// 图形验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult ValidateCode()
        {
            string code = "";
            MemoryStream ms = VierificationCodeServices.Create(out code);
            HttpContext.Session.SetString("LoginValidateCode", code);
            Response.Body.Dispose();
            return File(ms.ToArray(), @"image/png");
        }

        #endregion 图形验证码
    }
}