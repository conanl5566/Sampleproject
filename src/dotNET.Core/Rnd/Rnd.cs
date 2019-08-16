/**************************************************************************
 * 作者：X   
 * 日期：2017.01.19   
 * 描述：随机编号
 * 修改记录：    
 * ***********************************************************************/

using System;
using System.Text;

namespace dotNET.Core
{
    /// <summary>
    /// 随机编号
    /// </summary>
    public class Rnd
    {
   
        /// <summary>
        /// 生成不重复ID
        /// </summary>
        /// <param name="datacenterId"></param>
        /// <returns></returns>
        public static long RndId(long workerId, long datacenterId)
        {
            return new IdWorker(workerId, datacenterId).NextId();
        }

        /// <summary>
        /// 生成0-9随机数
        /// </summary>
        /// <param name="codeNum"></param>
        /// <returns></returns>
        public static string RndNum(int codeNum)
        {
            StringBuilder sb = new StringBuilder(codeNum);
            Random rand = new Random();
            for (int i = 1; i < codeNum + 1; i++)
            {
                int t = rand.Next(9);
                sb.AppendFormat("{0}", t);
            }
            return sb.ToString();
        }

        /// <summary>
        /// GuId
        /// </summary>
        /// <returns></returns>
        public static string GuId()
        {
            var tempGuid = Guid.NewGuid();
            var bytes = tempGuid.ToByteArray();
            var time = DateTime.Now;
            bytes[3] = (byte)time.Year;
            bytes[2] = (byte)time.Month;
            bytes[1] = (byte)time.Day;
            bytes[0] = (byte)time.Hour;
            bytes[5] = (byte)time.Minute;
            bytes[4] = (byte)time.Second;
            return new Guid(bytes).ToString();
            //return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string CreateNo()
        {
            Random random = new Random();
            string strRandom = random.Next(1000, 10000).ToString(); //生成编号 
            string code = DateTime.Now.ToString("yyyyMMddHHmmss") + strRandom;//形如
            return code;
        }
    }
}
