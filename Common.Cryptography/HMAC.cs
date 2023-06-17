using System.Security.Cryptography;


namespace Common.Cryptography;


public static class HMAC
{

    public static byte[] SHA256(string data, string key)
    {
        if (string.IsNullOrEmpty(data))
        {
            throw new ArgumentIsNullOrEmptyException(nameof(data));
        }
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentIsNullOrEmptyException(nameof(key));
        }

        var bytes = Serialization.Encoding.GetBytes(data);
        var byteKey = Serialization.Encoding.GetBytes(key);


        var hmac = new HMACSHA256(byteKey);
        return hmac.ComputeHash(bytes);
    }

    public static string SHA256toBase64(string data, string key)
    {
        var bytes = SHA256(data, key);

        return Base64.To(bytes);
    }



}
