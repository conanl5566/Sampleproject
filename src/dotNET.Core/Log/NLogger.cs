using NLog;
using System;
using System.Text;

namespace dotNET.Core
{
    /// <summary>
    /// 日志
    /// </summary>
    public class NLogger
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #region "等级1-Debug"
        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="msg">具体信息</param>
        public static void Debug(string msg, string userInfo = "")
        {
            string info = string.Empty;
            if (userInfo != "")
            {
                info = userInfo;
            }
            info = info + "\r\n" + msg;

            Logger.Debug(info);
        }

        #endregion

        #region "等级2-Info"


        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="msg">具体信息</param>
        public static void Info(string msg, string userInfo = "")
        {
            string info = string.Empty;
            if (userInfo != "")
            {
                info = userInfo;
            }
            info = info + "\r\n" + msg;
            Logger.Info(info);

        }

        #endregion

        #region "等级3-Warn"



        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="msg">具体信息</param>
        public static void Warn(string msg, string userInfo = "")
        {
            string info = string.Empty;
            if (userInfo != "")
            {
                info = userInfo;
            }
            info = info + "\r\n" + msg;
            Logger.Warn(info);

        }

        #endregion

        #region "等级4-Error"


        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="msg">具体信息</param>
        public static void Error(string msg, string userInfo = "")
        {
            string info = string.Empty;
            if (userInfo != "")
            {
                info = userInfo;
            }
            info = info + "\r\n" + msg;
            Logger.Error(info);

        }



        /// <summary>
        /// 记录异常信息
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="ex">具体异常</param>
        public static void Error(Exception ex, string userInfo = "")
        {
            string info = "";
            if (userInfo != "")
            {
                info = userInfo;
            }
            info = info + "\r\n" + ErrorDetails(ex);
            Logger.Error(info);

        }

        /// <summary>
        /// 将异常转成字符串
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static string ErrorDetails(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            string appString = "";
            while (ex != null)
            {
                if (count > 0)
                {
                    appString += "　";
                }
                sb.AppendLine(appString + " <br>异常消息：" + ex.Message);
                sb.AppendLine(appString + " <br>异常类型：" + ex.GetType().FullName);
                //  sb.AppendLine(appString + " <br>异常方法：" + (ex.TargetSite == null ? null : ex.TargetSite.Name));
                sb.AppendLine(appString + " <br>异常源：" + ex.Source);
                if (ex.StackTrace != null)
                {
                    sb.AppendLine(appString + "<br>异常堆栈：" + ex.StackTrace);
                }
                if (ex.InnerException != null)
                {
                    sb.AppendLine(appString + "<br>内部异常：");
                    count++;
                }
                ex = ex.InnerException;
            }

            return sb.ToString().Replace("位置:", "<br>位置");
        }

        #endregion

        #region "等级5-Fatal "



        /// <summary>
        /// 记录致命错误
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="msg">具体信息</param>
        public static void Fatal(string msg, string userInfo = "")
        {
            string info = string.Empty;
            if (userInfo != "")
            {
                info = userInfo;
            }
            info = info + "\r\n" + msg;
            Logger.Fatal(info);

        }

        #endregion
    }
}