using System;

namespace CompanyName.ProjectName.Core.Log
{
    /// <summary>
    /// 日志
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="msg">具体信息</param>
        ///   /// <param name="userInfo">用户信息</param>
        void Debug(string msg, string userInfo = "");

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="msg"></param>
        ///   /// <param name="userInfo">用户信息</param>
        void Info(string msg, string userInfo = "");

        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="msg">具体信息</param>
        ///   /// <param name="userInfo">用户信息</param>
        void Warn(string msg, string userInfo = "");

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="msg">具体信息</param>
        ///   /// <param name="userInfo">用户信息</param>
        void Error(string msg, string userInfo = "");

        /// <summary>
        /// 记录异常信息
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="ex">具体异常</param>
        void Error(Exception ex, string userInfo = "");

        /// <summary>
        /// 记录致命错误
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="msg">具体信息</param>
        ///   /// <param name="userInfo">用户信息</param>
        void Fatal(string msg, string userInfo = "");
    }
}