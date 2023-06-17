using System.Security.Cryptography;


namespace Common.Cryptography;


public static class SHA
{
    static SHA512CryptoServiceProvider SHA512Provider = new SHA512CryptoServiceProvider();
    static SHA256CryptoServiceProvider SHA256Provider = new SHA256CryptoServiceProvider();



    public static string SHA512(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            throw new ArgumentIsNullOrEmptyException(nameof(data));
        }
        return BitConverter.ToString(SHA512Provider.ComputeHash(Serialization.Encoding.GetBytes(data))).Replace("-", null);
    }


    public static string SHA256(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            throw new ArgumentIsNullOrEmptyException(nameof(data));
        }
        return BitConverter.ToString(SHA256Provider.ComputeHash(Serialization.Encoding.GetBytes(data))).Replace("-", null);
    }

}
