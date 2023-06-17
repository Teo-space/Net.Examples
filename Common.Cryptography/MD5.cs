using System.Security.Cryptography;


namespace Common.Cryptography;


public static class MD5
{
    static System.Security.Cryptography.MD5 Hash = System.Security.Cryptography.MD5.Create();
    public static string Compute(byte[] bytes)
    {
        if (bytes == null || bytes.Length == 0)
        {
            throw new ArgumentIsNullOrEmptyException(nameof(bytes));
        }
        return BitConverter.ToString(Hash.ComputeHash(bytes)).Replace("-", null).ToLowerInvariant();
    }

    public static string Compute(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            throw new ArgumentIsNullOrEmptyException(nameof(data));
        }
        return Compute(Serialization.Encoding.GetBytes(data));
    }

}
