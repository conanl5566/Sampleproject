using dotNET.Core;
using System;
using dotNET.Dto.Enum;

namespace dotNET.Application
{
    public class R
    {
        public static R Suc(string msg = "操作成功", CodeEnum code = CodeEnum.Ok)
        {
            return new R { Msg = msg, Code = code };
        }

        public static R Err(string msg = "操作失败", CodeEnum code = CodeEnum.Fail)
        {
            return new R { Msg = msg, Code = code };
        }

        /// <summary>
        /// 编码
        /// </summary>
        public CodeEnum Code { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        public string Msg { get; set; }

    }


    public class R<T>
    {
        public static R<T> Suc(T data = default(T), string msg = "操作成功", CodeEnum code = CodeEnum.Ok)
        {
            return new R<T> { Msg = msg, Code = code, Data = data };
        }

        public static R<T> Err(T data = default(T), string msg = "操作失败", CodeEnum code = CodeEnum.Fail)
        {
            return new R<T> { Msg = msg, Code = code, Data = data };
        }

        /// <summary>
        /// 编码
        /// </summary>
        public CodeEnum Code { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public T Data { get; set; }
    }
}
