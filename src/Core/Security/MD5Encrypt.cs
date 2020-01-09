using System;

namespace CompanyName.ProjectName.Core.Security
{
    public class MD5Encrypt
    {
        public static string MD5(string str, int code = 32)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] md5Data = md5.ComputeHash(data);
            string strResult = BitConverter.ToString(md5Data);

            string strEncrypt = string.Empty;
            if (code == 32)
            {
                strEncrypt = strResult.Replace("-", "").ToLower();
            }
            else if (code == 16)
            {
                strEncrypt = strResult.Replace("-", "").Substring(8, 16).ToLower();
            }
            return strEncrypt;
        }
    }
}