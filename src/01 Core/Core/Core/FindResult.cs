using System.Collections.Generic;

namespace CompanyName.ProjectName.Core
{
    /// <summary>
    /// 分页返回数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResult<T>
    {
        public int ItemCount { get; set; }
        public List<T> Data { get; set; }
    }
}