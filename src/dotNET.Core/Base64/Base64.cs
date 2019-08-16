using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace dotNET.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class Base64
    {
        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringToBase64(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Base64ToString(string str)
        {
            byte[] outputb = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(outputb);
        }

        /// <summary>
        ///  img to base64
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ToBase64(string path)
        {
            Image fromImage = Image.FromFile(path);
            MemoryStream stream = new MemoryStream();
            fromImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            return Convert.ToBase64String(stream.GetBuffer());
        }

        /// <summary>
        /// base64 to img
        /// </summary>
        /// <param name="base64"></param>
        /// <param name="path"></param>
        public static void ToImage(string base64, string path)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            try
            {
                FileInfo finfo = new FileInfo(path);
                if (!finfo.Directory.Exists)
                    finfo.Directory.Create();

                MemoryStream stream = new MemoryStream();
                stream.Write(bytes, 0, bytes.Length);
                Bitmap img = new Bitmap(stream);
                img.Save(path);

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static MemoryStream ToImage(string base64)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(base64);

                MemoryStream stream = new MemoryStream();
                stream.Write(bytes, 0, bytes.Length);
                return stream;
            }
            catch
            {
                throw new Exception("Base64解码错误");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static Bitmap BytesToBitmap(MemoryStream stream)
        {
            try
            {
                return new Bitmap((Image)new Bitmap(stream));
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            finally
            {
                stream.Close();
            }
        }

    }
}
