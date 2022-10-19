using System;
using System.Security.Cryptography;
using System.Text;

namespace open_imsdk_for_cs
{
    public class AesUtils
    {
        public AesUtils()
        {
        }

        // <summary>
        ///  AES 加密
        /// </summary>
        /// <param name="str">明文</param>
        /// <param name="aesKey">密钥</param>
        /// <returns></returns>
        public static string AesEncrypt(string str, string aesKey)
        {
            string data = string.Empty;
            if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(aesKey))
            {
                byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);
                Aes aes = Aes.Create();
                aes.Key = Encoding.UTF8.GetBytes(aesKey);
                byte[] resultArray = aes.EncryptEcb(toEncryptArray, PaddingMode.PKCS7);
                data = Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            return data;
        }

        /// <summary>
        ///  AES 解密
        /// </summary>
        /// <param name="str">密文</param>
        /// <param name="aesKey">密钥</param>
        /// <returns></returns>
        public static string AesDecrypt(string str, string aesKey)
        {
            string data = string.Empty;
            if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(aesKey))
            {
                byte[] toEncryptArray = Convert.FromBase64String(str);
                Aes aes = Aes.Create();
                aes.Key = Encoding.UTF8.GetBytes(aesKey);
                byte[] resultArray = aes.DecryptEcb(toEncryptArray, PaddingMode.PKCS7);
                data = Encoding.UTF8.GetString(resultArray);
            }
            return data;
        }

        /// <summary>
        /// 生成字符串的MD5码
        /// </summary>
        /// <param name="sInput"></param>
        /// <returns></returns>
        public static string md5(string sInput)
        {
            try
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] bytValue, bytHash;
                bytValue = System.Text.Encoding.UTF8.GetBytes(sInput);
                bytHash = md5.ComputeHash(bytValue);
                md5.Clear();
                string sTemp = "";
                for (int i = 0; i < bytHash.Length; i++)
                {
                    sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
                }
                sInput = sTemp.ToLower();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return sInput;
        }

    }
}

