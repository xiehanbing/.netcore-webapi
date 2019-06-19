using System;
using General.Core.Extension;
using NETCore.Encrypt;
using NETCore.Encrypt.Internal;

namespace General.Core.Encrypt
{
    /// <summary>
    /// 加解密帮助类 扩展
    /// </summary>
    public static class EncryptExtension
    {
        /// <summary>
        /// sha256 加密
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static string Sha256Encry(this string input)
        {
            return EncryptProvider.Sha256(input);
        }
        /// <summary>
        /// sha256 加密  base64 编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Sha256Base64Encry(this string input)
        {
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(EncryptProvider.Sha256(input));
            return  Convert.ToBase64String(byteArray);
        }

        /// <summary>
        /// aes加密
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        public static string AesEncry(this string input, string key = null)
        {
            if (key.IsNull()) key = EncryptContext.AesKey;
            return EncryptProvider.AESEncrypt(input, key);
        }
        /// <summary>
        /// AesEncry 加密 带偏移量
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="key">秘钥</param>
        /// <param name="iv">偏移量</param>
        /// <returns></returns>
        public static string AesEncry(this string input, string key = null, string iv = null)
        {
            if (key.IsNull()) key = EncryptContext.AesKey;
            if (iv.IsNull()) iv = EncryptContext.AesIv;
            return EncryptProvider.AESEncrypt(input, key,iv);
        }

        /// <summary>
        /// aes加密
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        public static string AesDescry(this string input, string key = null)
        {
            if (key.IsNull()) key = EncryptContext.AesKey;
            return EncryptProvider.AESDecrypt(input, key);
        }
        /// <summary>
        /// AesEncry 加密 带偏移量
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="key">秘钥</param>
        /// <param name="iv">偏移量</param>
        /// <returns></returns>
        public static string AesDescry(this string input, string key = null, string iv = null)
        {
            if (key.IsNull()) key = EncryptContext.AesKey;
            if (iv.IsNull()) iv = EncryptContext.AesIv;
            return EncryptProvider.AESDecrypt(input, key, iv);
        }
    }
}