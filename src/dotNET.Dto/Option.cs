namespace dotNET.Dto
{
    /// <summary>
    /// 分页参数
    /// </summary>
    public class Option
    {
        /// <summary>
        /// 排序
        /// </summary>
        public string OrderBy
        {
            get
            {
                Order = Order.ToLower();
                if (Order != "desc")
                    Order = "asc";

                return $"`{Sort}` {Order}";
            }
        }

        private int limit = 10;

        /// <summary>
        ///
        /// </summary>
        public int Limit
        {
            set
            {
                limit = value < 1 ? 1 : value;
            }
            get
            {
                return limit;
            }
        }

        private long offset = 0;

        /// <summary>
        ///
        /// </summary>
        public long Offset
        {
            set
            {
                offset = value < 1 ? 0 : value;
            }
            get
            {
                return offset;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public int PageIndex
        {
            get
            {
                if (Total != null && Offset > Total)
                {
                    Offset = Total.Value;
                }
                return System.Convert.ToInt32(Offset / Limit + 1);
            }
        }

        public long? Total { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Order { get; set; } = "asc";

        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; } = "Id";
    }
}