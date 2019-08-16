using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace dotNET.Core
{
    /// <summary>
    /// json 操作
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string result)
        {
            return JsonConvert.DeserializeObject<T>(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static dynamic Deserialize(string input)
        {
            return (dynamic)JsonConvert.DeserializeObject(input);
        }


        /// <summary>
        /// 输到前端网页使用时要注册 camelCase/longToStr 参数
        /// </summary>
        /// <param name="data"></param>
        /// <param name="camelCase"> 名称 驼峰结构输出</param>
        /// <param name="longToStr">long转成字串（javascript 超过15长度会有精度问题）</param>
        /// <returns></returns>
        public static string SerializeObject(object data, bool camelCase = false, bool longToStr = false)
        {
            var setting = new Newtonsoft.Json.JsonSerializerSettings();
            if (longToStr)
                setting.Converters.Add(new HexLongConverter());
            if (camelCase)
                setting.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            setting.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

            return JsonConvert.SerializeObject(data, setting);
        }
    }
}
