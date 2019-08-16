using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace dotNET.Core
{
    public enum CutMode { None = 1, WH, W, H, Cut };
    public class IMGUtility
    {

        #region 根据路径 生成 缩略图
        /// <summary>
        /// 根据路径 生成 缩略图
        /// </summary>
        /// <param name="p_strSource">传回的图片路径</param>
        /// <param name="p_strSave">图片保存的新路径</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="mode"></param>
        /// <param name="q">质量</param>
        public static void Thumbnail(string p_strSource, string p_strSave, int width, int height, CutMode mode, long q = 100L)
        {
            FileInfo fileInfo = new FileInfo(p_strSave);
            if (!fileInfo.Directory.Exists)
                fileInfo.Directory.Create();

            Image image = Image.FromFile(p_strSource);
            int num = width;
            int num2 = height;
            int x = 0;
            int y = 0;
            int num3 = image.Width;
            int num4 = image.Height;

            switch (mode)
            {
                case CutMode.WH:
                    break;
                case CutMode.W:
                    num2 = image.Height * width / image.Width;
                    break;
                case CutMode.H:
                    num = image.Width * height / image.Height;
                    break;

                case CutMode.Cut:
                    if ((double)image.Width / (double)image.Height < (double)num / (double)num2)
                    {
                        num4 = image.Height;
                        num3 = image.Height * num / num2;
                        y = 0;
                        x = (image.Width - num3) / 2;
                        break;
                    }
                    num3 = image.Width;
                    num4 = image.Width * height / num;
                    x = 0;
                    y = (image.Height - num4) / 2;
                    break;
                default:
                    if (image.Width > image.Height)
                        num2 = image.Height * width / image.Width;
                    else
                        num = image.Width * height / image.Height;
                    break;
            }

            Image image2 = new Bitmap(num, num2);
            Graphics graphics = Graphics.FromImage(image2);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.Clear(Color.White);
            graphics.DrawImage(image, new Rectangle(0, 0, num, num2), new Rectangle(x, y, num3, num4), GraphicsUnit.Pixel);
            try
            {
                ImageCodecInfo encoderInfo = IMGUtility.GetEncoderInfo("image/jpeg");
                Encoder quality = Encoder.Quality;
                EncoderParameters encoderParameters = new EncoderParameters(1);
                EncoderParameter encoderParameter = new EncoderParameter(quality, q);
                encoderParameters.Param[0] = encoderParameter;
                image2.Save(p_strSave, encoderInfo, encoderParameters);
            }
            catch (Exception ex)
            {
                image.Dispose();
                image2.Dispose();
                graphics.Dispose();
                throw ex;
            }
            finally
            {
                image.Dispose();
                image2.Dispose();
                graphics.Dispose();
            }
        }
        #endregion
        #region 根据文件流 生成 缩略图 
        /// <summary>
        /// 根据文件流 生成 缩略图  
        /// </summary>
        /// <param name="s">文件流</param> 
        /// <param name="p_strSave">图片保存的新路径</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="mode"></param>
        /// <param name="q">质量</param>
        public static void Thumbnail(Stream s, string p_strSave, int width, int height, CutMode mode, long q = 100L, string ext = "jpg")
        {
            FileInfo fileInfo = new FileInfo(p_strSave);
            if (!fileInfo.Directory.Exists)
                fileInfo.Directory.Create();

            Image image = Image.FromStream(s);
            int num = width;
            int num2 = height;
            int x = 0;
            int y = 0;
            int num3 = image.Width;
            int num4 = image.Height;
            switch (mode)
            {
                case CutMode.WH:
                    break;
                case CutMode.W:
                    num2 = image.Height * width / image.Width;
                    break;
                case CutMode.H:
                    num = image.Width * height / image.Height;
                    break;

                case CutMode.Cut:
                    if ((double)image.Width / (double)image.Height < (double)num / (double)num2)
                    {
                        num4 = image.Height;
                        num3 = image.Height * num / num2;
                        y = 0;
                        x = (image.Width - num3) / 2;
                        break;
                    }
                    num3 = image.Width;
                    num4 = image.Width * height / num;
                    x = 0;
                    y = (image.Height - num4) / 2;
                    break;
                default:
                    if (image.Width > image.Height)
                        num2 = image.Height * width / image.Width;
                    else
                        num = image.Width * height / image.Height;
                    break;
            }
            Image image2 = new Bitmap(num, num2);
            Graphics graphics = Graphics.FromImage(image2);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.Clear(Color.White);
            graphics.DrawImage(image, new Rectangle(0, 0, num, num2), new Rectangle(x, y, num3, num4), GraphicsUnit.Pixel);
            try
            {
                ImageCodecInfo encoderInfo = IMGUtility.GetEncoderInfo("image/jpeg");
                Encoder quality = Encoder.Quality;
                EncoderParameters encoderParameters = new EncoderParameters(1);
                EncoderParameter encoderParameter = new EncoderParameter(quality, q);
                encoderParameters.Param[0] = encoderParameter;
                image2.Save(p_strSave, encoderInfo, encoderParameters);
            }
            catch (Exception ex)
            {
                image.Dispose();
                image2.Dispose();
                graphics.Dispose();
                throw ex;
            }
            finally
            {
                image.Dispose();
                image2.Dispose();
                graphics.Dispose();
            }
        }
        #endregion
        public static bool Crop(string p_strSource, string p_strSave, int x, int y, int w, int h, int width, int height)
        {
            FileInfo fileInfo = new FileInfo(p_strSave);
            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }
            bool result;
            using (Image image = Image.FromFile(p_strSource))
            {
                try
                {
                    Image image2 = new Bitmap(width, height);
                    Graphics graphics = Graphics.FromImage(image2);
                    graphics.InterpolationMode = InterpolationMode.High;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.Clear(Color.White);
                    graphics.DrawImage(image, new Rectangle(0, 0, width, height), new Rectangle(x, y, w, h), GraphicsUnit.Pixel);
                    image2.Save(p_strSave);
                    result = true;
                }
                catch
                {
                    result = false;
                }
            }
            return result;
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < imageEncoders.Length; i++)
            {
                if (imageEncoders[i].MimeType == mimeType)
                {
                    return imageEncoders[i];
                }
            }
            return null;
        }
    }



    public enum ImagePathType
    {
        员工头像






    }
    class ImagePath
    {
        /// <summary>
        /// 类型
        /// </summary>
        public ImagePathType Type { get; set; }
        /// <summary>
        /// 宽
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 高
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 固定路径
        /// </summary>
        public string FixedPath { get; set; }
    }
    public class IMGOperate
    {
        #region 图片固定路径数据 （注：1.图片从小到大2.请和前台设置一致）
        /// <summary>
        /// 图片固定路径数据
        /// </summary>
        private static List<ImagePath> imagePathData = new List<ImagePath>
            {
                new ImagePath { Type = ImagePathType.员工头像,FixedPath="\\user\\icon",Width=100,Height=100}




            };
        #endregion

        #region 根据图片类型获取固定路径
        /// <summary>
        /// 根据图片类型获取固定路径
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        private static ImagePath GetPathByType(ImagePathType type)
        {
            return imagePathData.Find(o => o.Type == type);
        }
        /// <summary>
        /// 根据图片类型获取固定路径
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        private static List<ImagePath> GetPathByTypes(ImagePathType type)
        {
            return imagePathData.FindAll(o => o.Type == type);
        }
        #endregion

        #region 存储图片 根据临时路径
        /// <summary>
        /// 存储图片 根据临时路径
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="tempUrl">图片临时路径  /fafafaf/afa/faf.jpg</param>
        /// <param name="mapPath">服务器虚拟路径 d:\wi\web</param>
        /// <param name="domainName">http：//+域名</param>
        /// <param name="rootPath">根路径   upload</param>
        /// <param name="tempPath">临时文件根路径 tempHSCP</param>
        /// <returns></returns>
        public static string BaseSave(ImagePathType type, string tempUrl)
        {

            //判断是不是临时图片
            if (string.IsNullOrWhiteSpace(tempUrl))
                return "";

            var contentRoot = Directory.GetCurrentDirectory();
            var webRoot = Path.Combine(contentRoot, "wwwroot");
            FileInfo file = new FileInfo(tempUrl);


            //  var parsedContentDisposition = ContentDispositionHeaderValue.Parse(tempUrl);
            var originalName = file.Name.Replace("\"", "");
            //  var ext = Path.GetExtension(Path.Combine(webRoot, originalName));





            // 获取 固定路径
            string fixedPath = GetPathByType(type).FixedPath;

            // 获取 存储图片宽高,格式为：[宽x高,宽x高,宽x高...]注：宽高中间用小写"x"隔开
            var sizeArray = tempUrl.Substring(tempUrl.LastIndexOf("[") + 1, tempUrl.IndexOf("]") - tempUrl.LastIndexOf("[") - 1).Split('-');
            var sizeAll = tempUrl.Substring(tempUrl.LastIndexOf("["), tempUrl.IndexOf("]") - tempUrl.LastIndexOf("[") + 1);
            var tempUrlNoExt = tempUrl.Substring(0, tempUrl.LastIndexOf("."));
            var ext = Path.GetExtension(tempUrl);// tempUrl.Substring(tempUrl.LastIndexOf("."));

            string savePathData = "";
            for (int i = 0; i < sizeArray.Length; i++)
            {
                //获取一条宽高
                var wh = sizeArray[i].Split('x');
                if (wh.Length == 2)
                {
                    //宽
                    int width = int.Parse(wh[0]);
                    //高
                    int height = int.Parse(wh[1]);





                    var fileName = Path.Combine("upload" + fixedPath, Guid.NewGuid().ToString() + "_" + width + "_" + height + ext);

                    savePathData += "\\" + fileName + ";";

                    //图片处理
                    IMGUtility.Thumbnail(tempUrl, Path.Combine(webRoot, fileName), width, height, CutMode.WH);
                }
            }

            if (!string.IsNullOrWhiteSpace(savePathData))
            {
                savePathData = savePathData.Substring(0, savePathData.Length - 1);
            }
            return savePathData;
        }
        #endregion

        #region 存储图片 根据文件流
        /// <summary>
        /// 存储图片 根据文件流
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="s">图片临时路径</param>
        /// <param name="mapPath">绝对路径（D:\相对路径）</param>
        /// <param name="domainName">http：//+域名</param>
        /// <param name="rootPath">根路径</param>
        /// <returns></returns>
        public static string BaseSave(ImagePathType type, Stream s, string mapPath, string domainName)
        {
            // 获取 当前所有路径
            var paths = GetPathByTypes(type);
            // 保存图片或的路径，多个路径中间用逗号","隔开
            string savePathData = "";
            foreach (var p in paths)
            {
                //宽
                int width = p.Width;
                //高
                int height = p.Height;
                //固定路径
                string fixedPath = p.FixedPath.Trim('/');

                var dt = DateTime.Now;
                var random = new Random().Next(100, 999);
                string fn = "/" + dt.ToString("HHmmssff") + random + "_" + width + "x" + height + ".jpg";
                string path = fixedPath + "/" + dt.ToString("yyyy") + "/" + dt.ToString("MM") + "/" + dt.ToString("dd");
                //图片保存相对路径
                var relativePath = path + fn;

                //图片保存路径 例："D:\"+相对路径
                var savePath = Path.Combine(mapPath, relativePath);
                //图片保存路径数据 http：//+域名+相对路径
                var saveData = domainName + "/" + relativePath;
                if (savePathData != "")
                    savePathData = savePathData + "," + saveData;
                else
                    savePathData = saveData;
                //图片处理
                IMGUtility.Thumbnail(s, savePath, width, height, CutMode.WH);
            }
            return savePathData;
        }
        #endregion

        #region 根据真实路径 存储缩略图片
        ///// <summary>
        ///// 根据真实路径  存储缩略图片///////////////////////未写完
        ///// </summary>
        ///// <param name="returnUrl">传回的路径</param>
        ///// <param name="mapPath">服务器虚拟路径</param>
        ///// <param name="domainName">http：//+域名</param>
        ///// <param name="width">宽</param>
        ///// <param name="height">高</param>
        ///// <returns></returns>
        public static string BaseSaveThumbnail(string returnUrl, string mapPath, string domainName, int width, int height)
        {
            var urlArray = returnUrl.Split(',');
            for (int i = 0; i < urlArray.Length; i++)
            {
                var realPath = urlArray[i].Replace(domainName + "/", mapPath);
                var savePath = realPath.Replace(".jpg", "_thum.jpg");

            }
            return "";
        }
        #endregion
    }



}
