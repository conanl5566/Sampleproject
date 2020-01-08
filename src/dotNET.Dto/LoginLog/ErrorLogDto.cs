using System;

namespace dotNET.Dto
{
    /// <summary>
    /// 错误日志
    /// </summary>
    public class ErrorLogDto
    {
        /// <summary>
        ///
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string filename { get; set; }

        /// <summary>
        ///日期
        /// </summary>
        public DateTime CreatorTime { get; set; }
    }

    public class ErrorLogOption : Option
    {
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime? kCreatorTime { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime? eCreatorTime { get; set; }
    }
}