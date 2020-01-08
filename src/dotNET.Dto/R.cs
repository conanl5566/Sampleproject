using dotNET.Dto.Enum;

namespace dotNET.Dto
{
    public class ResultDto
    {
        public static ResultDto Suc(string msg = "操作成功", CodeEnum code = CodeEnum.Ok)
        {
            return new ResultDto { Msg = msg, Code = code };
        }

        public static ResultDto Err(string msg = "操作失败", CodeEnum code = CodeEnum.Fail)
        {
            return new ResultDto { Msg = msg, Code = code };
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

    public class ResultDto<T>
    {
        public static ResultDto<T> Suc(T data = default(T), string msg = "操作成功", CodeEnum code = CodeEnum.Ok)
        {
            return new ResultDto<T> { Msg = msg, Code = code, Data = data };
        }

        public static ResultDto<T> Err(T data = default(T), string msg = "操作失败", CodeEnum code = CodeEnum.Fail)
        {
            return new ResultDto<T> { Msg = msg, Code = code, Data = data };
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