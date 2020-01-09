using System.Collections.Generic;

namespace dotNET.Core
{
    /// <summary>
    /// Bootstrap Table 分页数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Page<T>
    {
        public Page(long total, IEnumerable<T> rows)
        {
            Total = total;
            Rows = rows;
        }

        public IEnumerable<T> Rows { get; set; }

        public long Total { get; }
    }

    /// <summary>
    /// jqGrid 分页数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JqGridPage<T> where T : class, new()
    {
        public JqGridPage(long totalRecords, int pageNumber, IEnumerable<T> rows)
        {
            Records = totalRecords;
            Total = totalRecords / pageNumber;
            if (totalRecords % pageNumber == 0)
                Total = Total + 1;
            Rows = rows;
        }

        public IEnumerable<T> Rows { get; }

        public long Total { get; }

        public int Page { get; }

        public long Records { get; }

        public string Costtime { get; set; }
    }
}