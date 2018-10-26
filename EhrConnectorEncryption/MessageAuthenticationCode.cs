using System;
using System.Security.Cryptography;
using System.Text;

namespace EhrConnectorEncryption
{
    public class MessageAuthenticationCode
    {
        public string Code { get; private set; }

        HashAlgorithm _hashAlgorithm;

        Encoding _encoding;

        string _key;

        string _iv;

        string _value;

        public MessageAuthenticationCode(HashAlgorithm hashAlgorithm, Encoding encoding, string key, string iv, string value)
        {
            _hashAlgorithm = hashAlgorithm;

            _encoding = encoding;
        }

        public MessageAuthenticationCode(string key, string iv, string value):this(HashAlgorithm.Sha256, Encoding.UTF7, key, iv, value)
        {
            _key = key;
            // default to utf7 encoding
            _iv = Base64Encode(iv, _encoding);

            _value = value;
        }

        public void Hash()
        {
            Code = HashHmac(_iv + _value, _key);
        }

        //return hash_hmac('sha256', $iv.$value, $this->key);

        public static string Base64Encode(string plainText, Encoding encoding)
        {
            var plainTextBytes = encoding.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        static string HashHmac(string message, string secret)
        {
            Encoding encoding = Encoding.UTF8;
            using (HMACSHA512 hmac = new HMACSHA512(encoding.GetBytes(secret)))
            {
                var msg = encoding.GetBytes(message);
                var hash = hmac.ComputeHash(msg);
                return ByteToString(hash);
                //return BitConverter.ToString(hash).ToLower().Replace("-", string.Empty);
            }
        }

        static string ByteToString(byte[] buff)
        {
            string sbinary = "";
            for (int i = 0; i < buff.Length; i++)
                sbinary += buff[i].ToString("X2"); /* hex format */
            return sbinary;
        }
    }
}
