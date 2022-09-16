using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Commons
{
    public class Functions
    {
        public static DateTime ParseDateTimes(object obj, string format = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(format))
                {
                    return DateTime.ParseExact(obj.ToString(), format, CultureInfo.InvariantCulture);

                }
                DateTime result;
                if (obj == null) return DateTime.Now.Date;               
                if (DateTime.TryParse(obj.ToString(), out result))
                    return result;
                return DateTime.Now.Date;
            }
            catch (Exception)
            {
                return DateTime.Now.Date;
            }
        }

        /// <summary>
        /// hàm parse object to string
        /// exception return ""
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToString(object obj)
        {
            try
            {
                if (obj == null) return string.Empty;
                return obj.ToString().Trim();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// parse object to int
        /// exception return 0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ParseInt(object obj)
        {
            try
            {
                int result;
                if (obj == null) return 0;
                if (int.TryParse(obj.ToString(), out result))
                    return result;
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static double ParseDouble(object obj)
        {
            try
            {
                double result;
                if (obj == null) return 0;
                if (double.TryParse(obj.ToString(), out result))
                {
                    if (!Double.IsInfinity(result) && !Double.IsNaN(result))
                    {
                        return result;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static double ParseDouble(object obj, NumberFormatInfo provider)
        {
            try
            {
                double result;
                if (obj == null) return 0;
                if (double.TryParse(obj.ToString(), NumberStyles.Any, provider, out result))
                    if (!Double.IsInfinity(result) && !Double.IsNaN(result))
                    {
                        return result;
                    }
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static float ParseFloat(object obj)
        {
            try
            {
                float result;
                if (obj == null) return 0;
                if (float.TryParse(obj.ToString(), out result))
                    return result;
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static decimal ParseDecimal(object obj)
        {
            try
            {
                decimal result;
                if (obj == null) return 0;
                var value = obj.ToString();
                if (decimal.TryParse(value, out result))
                    return result;
                if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                    return result;
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// hàm parrse object to datetime
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime? ParseDateTime(object obj, string format = "")
        {
            try
            {
                if (obj == null) return null;
                if (!string.IsNullOrEmpty(format))
                {
                    return DateTime.ParseExact(obj.ToString(), format, CultureInfo.InvariantCulture);
                }
                DateTime result;               
                if (DateTime.TryParse(obj.ToString(), out result))
                    return result;
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private static int GetMaxDataLength()
        {
            if (Constants.RSA.fOAEP)
                return ((Constants.RSA.KEY_SIZE - 384) / 8) + 7;
            return ((Constants.RSA.KEY_SIZE - 384) / 8) + 37;
        }
        private static bool IsKeySizeValid()
        {
            return Constants.RSA.KEY_SIZE >= 384 &&
                   Constants.RSA.KEY_SIZE <= 16384 &&
                   Constants.RSA.KEY_SIZE % 8 == 0;
        }

        public static string Encrypt(string plainText, string publickey = "")
        {
            if (string.IsNullOrEmpty(publickey))
            {
                publickey = Constants.RSA.PublicKey;
            }
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentException("Can not encryt data");


            var maxLength = GetMaxDataLength();
            if (Encoding.Unicode.GetBytes(plainText).Length > maxLength)
                throw new ArgumentException("Can not encryt data");


            if (!IsKeySizeValid())
                throw new ArgumentException("Can not encryt data");


            if (string.IsNullOrWhiteSpace(publickey))
                throw new ArgumentException("Can not encryt data");


            string encryptedText;

            try
            {
                using (var rsaProvider = RSA.Create())
                {
                    rsaProvider.FromXmlString(publickey);
                    var plainBytes = Encoding.Unicode.GetBytes(plainText);
                    var encryptedBytes = rsaProvider.Encrypt(plainBytes, RSAEncryptionPadding.Pkcs1);
                    encryptedText = Convert.ToBase64String(encryptedBytes);
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Could not encryt data!");

            }
            return encryptedText;

        }
        public static string Decrypt(string encryptedText, string privatekey = "")
        {
            if (string.IsNullOrEmpty(privatekey))
            {
                privatekey = Constants.RSA.PrivateKey;
            }

            if (string.IsNullOrWhiteSpace(encryptedText))
                throw new ArgumentException("Can not Decrypt data");

            if (!IsKeySizeValid())
                throw new ArgumentException("Can not Decrypt data");

            if (string.IsNullOrWhiteSpace(privatekey))
                throw new ArgumentException("Can not Decrypt data");

            var plainText = "";


            try
            {
                using (var rsaProvider = RSA.Create())
                {
                    rsaProvider.FromXmlString(privatekey);
                    var encryptedBytes = Convert.FromBase64String(encryptedText);
                    var plainBytes = rsaProvider.Decrypt(encryptedBytes, RSAEncryptionPadding.Pkcs1);
                    plainText = Encoding.Unicode.GetString(plainBytes);
                }
            }
            catch (Exception ex)
            {              
                throw new Exception("Could not Decrypt data");
            }
            return plainText;
        }
    }
}