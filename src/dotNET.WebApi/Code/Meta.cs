//namespace dotNET.HttpApi.Host
//{
//    /// <summary>
//    /// 返回数据
//    /// </summary>
//    public class Meta
//    {
//        /// <summary>
//        /// 状态 成功:200,错误:500,失败:501,未授权:401
//        /// </summary>
//        public int State { get; set; }

//        /// <summary>
//        /// 提示
//        /// </summary>
//        public string Msg { get; set; }

//        /// <summary>
//        /// 成功状态
//        /// </summary>
//        /// <param name="msg"></param>
//        /// <returns></returns>
//        public static Meta Suc(string msg = "操作成功")
//        {
//            Meta m = new Meta();
//            m.State = 200;
//            m.Msg = msg;
//            return m;
//        }

//        /// <summary>
//        /// 错误状态
//        /// </summary>
//        /// <param name="msg"></param>
//        /// <returns></returns>
//        public static Meta Err(string msg = "操作错误")
//        {
//            Meta m = new Meta();
//            m.State = 500;
//            m.Msg = msg;
//            return m;
//        }

//        /// <summary>
//        /// 失败状态
//        /// </summary>
//        /// <param name="msg"></param>
//        /// <returns></returns>
//        public static Meta Fail(string msg = "操作失败")
//        {
//            Meta m = new Meta();
//            m.State = 501;
//            m.Msg = msg;
//            return m;
//        }
//    }
//    /// <summary>
//    /// 
//    /// </summary>
//    public class R<T>
//    {
//        /// <summary>
//        /// 状态
//        /// </summary>
//        public Meta Meta { get; set; }

//        /// <summary>
//        /// 数据
//        /// </summary>
//        public T Data { get; set; }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="msg"></param>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public static R<T> Suc(string msg, T data)
//        {
//            return new R<T> { Meta = Meta.Suc(msg), Data = data };
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="msg"></param>
//        /// <returns></returns>
//        public static R<T> Suc(string msg)
//        {
//            return new R<T> { Meta = Meta.Suc(msg) };
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public static R<T> Suc(T data)
//        {
//            return new R<T> { Meta = Meta.Suc("操作成功"), Data = data };
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        public static R<T> Suc()
//        {
//            return new R<T> { Meta = Meta.Suc() };
//        }
//        /// <summary>
//        /// 错误
//        /// </summary>
//        /// <param name="msg"></param>
//        /// <returns></returns>
//        public static R<T> Err(string msg)
//        {
//            return new R<T> { Meta = Meta.Err(msg) };
//        }
//        /// <summary>
//        /// 失败
//        /// </summary>
//        /// <param name="msg"></param>
//        /// <returns></returns>
//        public static R<T> Fail(string msg)
//        {
//            return new R<T> { Meta = Meta.Fail(msg) };
//        }
//    }
//}
