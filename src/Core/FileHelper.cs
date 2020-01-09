using System;
using System.IO;

namespace CompanyName.ProjectName.Core
{
    /// <summary>
    /// 文件操作
    /// </summary>
    public class FileHelper
    {
        //public readonly IHostingEnvironment _Env;

        //public FileHelper(IHostingEnvironment Env)
        //{
        //    _Env = Env;
        //}

        //   private  string _ContentRootPath = _Env.ContentRootPath;

        /// <summary>
        /// 创建目录或文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="isDirectory">是否是目录</param>
        public void CreateFiles(string path, bool isDirectory)
        {
            try
            {
                if (!IsExist(path, isDirectory))
                {
                    if (isDirectory)
                        Directory.CreateDirectory(IsAbsolute(path) ? path : MapPath(path));
                    else
                    {
                        FileInfo file = new FileInfo(IsAbsolute(path) ? path : MapPath(path));
                        FileStream fs = file.Create();
                        fs.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 检测目录是否为空
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public bool IsEmptyDirectory(string path)
        {
            return Directory.GetFiles(IsAbsolute(path) ? path : MapPath(path)).Length <= 0 && Directory.GetDirectories(IsAbsolute(path) ? path : MapPath(path)).Length <= 0;
        }

        /// <summary>
        /// 检测指定路径是否存在
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="isDirectory">是否是目录</param>
        /// <returns></returns>
        public bool IsExist(string path, bool isDirectory)
        {
            return isDirectory ? Directory.Exists(IsAbsolute(path) ? path : MapPath(path)) : File.Exists(IsAbsolute(path) ? path : MapPath(path));
        }

        /// <summary>
        /// 是否是绝对路径
        /// windows下判断 路径是否包含 ":"
        /// Mac OS、Linux下判断 路径是否包含 "\"
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public bool IsAbsolute(string path)
        {
            return Path.VolumeSeparatorChar == ':' ? path.IndexOf(Path.VolumeSeparatorChar) > 0 : path.IndexOf('\\') > 0;
        }

        /// <summary>
        　　/// 获取文件绝对路径
        　　/// </summary>
        　　/// <param name="path">文件路径</param>
        　　/// <returns></returns>
        public string MapPath(string path)
        {
            var contentRoot = Directory.GetCurrentDirectory();
            return IsAbsolute(path) ? path : Path.Combine(contentRoot, path.TrimStart('~', '/').Replace("/", Path.DirectorySeparatorChar.ToString()));
        }
    }
}