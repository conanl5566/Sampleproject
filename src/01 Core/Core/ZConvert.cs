using System;
using System.Globalization;

namespace CompanyName.ProjectName.Core
{
    /// <summary>
    /// 数据转换类
    /// </summary>
    public partial class ZConvert
    {
        #region 星期名称转换

        /// <summary>
        /// 星期名称转换
        /// </summary>
        /// <param name="objDayOfWeek"></param>
        /// <returns></returns>
        public static string ConvertWeekName(DayOfWeek objDayOfWeek)
        {
            string str = "";
            switch (objDayOfWeek)
            {
                case DayOfWeek.Sunday:
                    str = "星期日";
                    break;

                case DayOfWeek.Monday:
                    str = "星期一";
                    break;

                case DayOfWeek.Tuesday:
                    str = "星期二";
                    break;

                case DayOfWeek.Wednesday:
                    str = "星期三";
                    break;

                case DayOfWeek.Thursday:
                    str = "星期四";
                    break;

                case DayOfWeek.Friday:
                    str = "星期五";
                    break;

                case DayOfWeek.Saturday:
                    str = "星期六";
                    break;

                default:
                    break;
            }
            return str;
        }

        #endregion 星期名称转换

        /// <summary>
        /// 时间类型格式化字符串,格式为 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 时间类型格式化字符串,格式为 yyyy-MM-dd
        /// </summary>
        public const string DateTimeFormatShortDate = "yyyy-MM-dd";

        /// <summary>
        /// string型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(string strValue)
        {
            //if (!string.IsNullOrEmpty(strValue) && !Convert.IsDBNull(strValue))
            if (!string.IsNullOrWhiteSpace(strValue))
            {
                strValue = strValue.Trim();
                return (((string.Compare(strValue, "true", true) == 0) || (string.Compare(strValue, "yes", true) == 0)) || (string.Compare(strValue, "1", true) == 0));
            }
            return false;
        }

        /// <summary>
        /// object型转换为bool型
        /// </summary>
        /// <param name="obj">要转换的字符串</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(object obj)
        {
            return Convert.ToBoolean(obj);
        }

        /// <summary>
        /// string型转换为时间型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的时间类型结果</returns>
        public static DateTime StrToDateTime(object strValue, DateTime defValue)
        {
            if ((strValue == null) || string.IsNullOrWhiteSpace(strValue.ToString()) || (strValue.ToString().Length > 40))
            {
                return defValue;
            }

            //DateTime val;

            if (!DateTime.TryParse(strValue.ToString(), out DateTime val))
            {
                val = defValue;
            }
            return val;
        }

        /// <summary>
        /// 将输入的int型字符串转换为DateTime类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DateTime StrIntToDateTime(string input)
        {
            return IntToDateTime(StrToInt(input, 0));
        }

        /// <summary>
        /// 将输入的字符串int型转换为DateTime类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DateTime StrIntToDateTime(object input)
        {
            return StrIntToDateTime(input.ToString());
        }

        /// <summary>
        /// 将输入的字符串int型转换为DateTime字符串类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string StrIntToStrDateTime(string input)
        {
            return StrIntToDateTime(StrToInt(input, 0)).ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 将输入的long型转换为DateTime型
        /// </summary>
        /// <param name="inputLong"></param>
        /// <returns></returns>
        public static DateTime IntToDateTime(int inputLong)
        {
            DateTime beginTime = DateTime.Now.Date;
            DateTime.TryParse("1970-01-01", out beginTime);
            double addDays = (double)inputLong / (double)(24 * 3600);
            beginTime = beginTime.ToLocalTime();
            return beginTime.AddDays((double)addDays);
        }

        /// <summary>
        /// object型转换为decimal型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <returns>转换后的decimal类型结果</returns>
        public static decimal StrToDecimal(object strValue)
        {
            if (!string.IsNullOrWhiteSpace(strValue.ToString()) && !object.Equals(strValue, null))
            {
                return StrToDecimal(strValue.ToString());
            }
            return 0M;
        }

        /// <summary>
        /// string型转换为decimal型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <returns>转换后的decimal类型结果</returns>
        public static decimal StrToDecimal(string strValue)
        {
            decimal.TryParse(strValue, out decimal num);
            return num;
        }

        /// <summary>
        /// string型转换为decimal型
        /// </summary>
        /// <param name="input">要转换的字符串</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>转换后的decimal类型结果</returns>
        public static decimal StrToDecimal(string input, decimal defaultValue)
        {
            if (decimal.TryParse(input, out decimal num))
            {
                return num;
            }
            return defaultValue;
        }

        /// <summary>
        /// string型转换为decimal型
        /// </summary>
        /// <param name="input">要转换的字符串</param>
        /// <param name="defaultValue">缺省值</param>
        /// <param name="pointLength">要保留小数点后面的位数</param>
        /// <returns>转换后的decimal类型结果</returns>
        public static decimal StrToDecimal(string input, decimal defaultValue, int pointLength)
        {
            if (input.Contains("."))
            {
                int len = input.IndexOf(".") + (pointLength + 1);
                if (input.Length > len)
                {
                    input = input.Substring(0, len);
                }
            }

            if (!decimal.TryParse(input, out decimal num))
            {
                num = defaultValue;
            }
            return num;
        }

        /// <summary>
        /// string型转换为double型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <returns>转换后的double类型结果</returns>
        public static double StrToDouble(object strValue)
        {
            if (!string.IsNullOrWhiteSpace(strValue.ToString()) && !object.Equals(strValue, null))
            {
                return StrToDouble(strValue.ToString());
            }
            return 0.0;
        }

        /// <summary>
        /// string型转换为double型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <returns>转换后的double类型结果</returns>
        public static double StrToDouble(string strValue)
        {
            double.TryParse(strValue, out double num);
            return num;
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的float类型结果</returns>
        public static float StrToFloat(object strValue, float defValue)
        {
            if ((strValue == null) || string.IsNullOrWhiteSpace(strValue.ToString()) || (strValue.ToString() == string.Empty))
            {
                return defValue;
            }

            if (float.TryParse(strValue.ToString(), out float val))
            {
                return val;
            }
            return defValue;
        }

        /// <summary>
        /// string型转换为int型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <returns>转换后的int类型结果.如果要转换的字符串是非数字,则返回0.</returns>
        public static int StrToInt(object strValue)
        {
            return StrToInt(strValue, 0);
        }

        /// <summary>
        /// string型转换为int型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(object strValue, int defValue)
        {
            if ((strValue == null) || string.IsNullOrWhiteSpace(strValue.ToString()) || (strValue.ToString() == string.Empty))
            {
                return defValue;
            }

            string val = strValue.ToString();
            if (val.IndexOf(".") >= 0)
            {
                val = val.Split(new char[] { '.' })[0];
            }

            if (int.TryParse(val, out int intValue))
            {
                return intValue;
            }
            return defValue;
        }

        /// <summary>
        /// string型转换为Long型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的Long类型结果</returns>
        public static long StrToLong(object strValue)
        {
            return StrToLong(strValue, 0);
        }

        /// <summary>
        /// string型转换为Long型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static long StrToLong(object strValue, long defValue)
        {
            if ((strValue == null) || string.IsNullOrWhiteSpace(strValue.ToString()) || (strValue.ToString() == string.Empty))
            {
                return defValue;
            }

            string val = strValue.ToString();
            if (val.IndexOf(".") >= 0)
            {
                val = val.Split(new char[] { '.' })[0];
            }
            if (long.TryParse(val, out long intValue))
            {
                return intValue;
            }
            return defValue;
        }

        #region NPOI转换Excel日期时间格式

        /// <summary>
        /// NPOI转换Excel日期时间格式,出错时返回当前时间
        /// </summary>
        /// <param name="_datetime"></param>
        /// <returns></returns>
        public static string NPOIParseDateTime(string _datetime)
        {
            string[] arr = _datetime.Split(new char[] { ' ' });
            string strDate = "";
            try
            {
                strDate = NPOIParseDate(arr[0]);

                if (arr.Length > 1)
                {
                    strDate += " " + NPOIParseTime(arr[1]);
                }
            }
            catch
            {
                strDate = DateTime.Now.ToString();
            }
            return strDate;
        }

        /// <summary>
        /// 转换日期格式
        /// </summary>
        /// <param name="_date"></param>
        /// <returns></returns>
        private static string NPOIParseDate(string _date)
        {
            if (_date.IndexOf("/") > 0)
            {
                string tem = "";
                string[] arr = _date.Split(new char[] { '/' });
                for (int i = 0; i < arr.Length; i++)
                {
                    string str = arr[i];
                    if (i == 2)
                    {
                        if (str.Length < 3)
                        {
                            tem += "20" + str.PadLeft(2, '0');
                        }
                        else
                        {
                            tem += str.PadLeft(2, '0');
                        }
                    }
                    else
                    {
                        tem += str.PadLeft(2, '0');
                    }
                }
                _date = tem;
            }

            //這裡定義所有日期格式
            string[] dateFormats = new string[] { "MMddyy", "MMddyyyy", "MM/dd/yy","yyyy/MM/dd", "yyy/MM/dd", "yy/MM/dd", "y/MM/dd",
           "yyyy-MM-dd", "yyy-MM-dd", "yy-MM-dd", "y-MM-dd",
           "yyyy/M/dd","yyy/M/dd","yy/M/dd","y/M/dd",
           "yyyy-M-dd","yyy-M-dd","yy-M-dd","y-M-dd",
           "yyyy/MM/d","yyy/MM/d","yy/MM/d","y/MM/d",
           "yyyy-MM-d","yyy-MM-d","yy-MM-d","y-MM-d",
           "yyyy/M/d","yyy/M/d","yy/M/d","y/M/d",
           "yyyy-M-d","yyy-M-d","yy-M-d","y-M-d" };
            try
            {
                //這裡來處理傳入的格式是否為日期格式，只需要簡單一行
                DateTime datetime = DateTime.ParseExact(_date, dateFormats, null, DateTimeStyles.AllowWhiteSpaces);
                return datetime.ToString("yyyy-MM-dd");
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 转换时间格式
        /// </summary>
        /// <param name="_time"></param>
        /// <returns></returns>
        private static string NPOIParseTime(string _time)
        {
            string[] timeFormats = new string[]{ "HH:mm:ss","HH:mm","H:m", "HH:mm:s","HH:m:ss", "HH:m:s",
           "H:mm:ss","H:mm:s","H:m:ss","H:m:s"};
            try
            {
                DateTime datetime = DateTime.ParseExact(_time, timeFormats, null, DateTimeStyles.AllowWhiteSpaces);
                return datetime.ToString("HH:mm:ss");
            }
            catch (Exception)
            {
                return "";
            }
        }

        #endregion NPOI转换Excel日期时间格式
    }
}