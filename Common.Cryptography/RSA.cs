using System.Security.Cryptography;


namespace Common.Cryptography;


public static class RSA
{

    public static RSAParameters RSAParametersFromString(string data) => Xml.FromString<RSAParameters>(data);


    public static byte[] Decrypt(byte[] bytes, RSAParameters privateKey)
    {
        if (bytes == null || bytes.Length == 0)
        {
            throw new ArgumentIsNullOrEmptyException(nameof(bytes));
        }
        if (ReferenceEquals(privateKey, default(RSAParameters)))
        {
            throw new ArgumentException("RSAParameters is default");
        }

        var csp = new RSACryptoServiceProvider();
        csp.ImportParameters(privateKey);
        return csp.Decrypt(bytes, false);
    }
    public static byte[] DecryptBase64(string data, RSAParameters privateKey)
    {
        if (string.IsNullOrEmpty(data))
        {
            throw new ArgumentIsNullOrEmptyException(nameof(data));
        }

        var bytes = Base64.BytesFrom(data);

        return Decrypt(bytes, privateKey);
    }
    public static string DecryptBase64ToString(string data, RSAParameters privateKey)
    {
        if (string.IsNullOrEmpty(data))
        {
            throw new ArgumentIsNullOrEmptyException(nameof(data));
        }
        var bytes = Base64.BytesFrom(data);

        var result = Decrypt(bytes, privateKey);

        return Serialization.Encoding.GetString(result);
    }

    public static byte[] Decrypt(byte[] bytes, string privateKey)
        => Decrypt(bytes, RSAParametersFromString(privateKey));

    public static byte[] DecryptBase64(string data, string privateKey)
        => DecryptBase64(data, RSAParametersFromString(privateKey));
    public static string DecryptBase64ToString(string data, string privateKey)
        => DecryptBase64ToString(data, RSAParametersFromString(privateKey));


















    public static byte[] Encrypt(byte[] bytes, RSAParameters publicKey)
    {
        if (bytes == null || bytes.Length == 0)
        {
            throw new ArgumentIsNullOrEmptyException(nameof(bytes));
        }
        if (ReferenceEquals(publicKey, default(RSAParameters)))
        {
            throw new ArgumentException("RSAParameters is default");
        }

        var csp = new RSACryptoServiceProvider();
        csp.ImportParameters(publicKey);
        return csp.Encrypt(bytes, false);
    }

    public static string EncryptToBase64(byte[] bytes, RSAParameters publicKey)
    {
        return Base64.To(Encrypt(bytes, publicKey));
    }

    public static byte[] EncryptString(string data, RSAParameters publicKey)
    {
        if (string.IsNullOrEmpty(data))
        {
            throw new ArgumentIsNullOrEmptyException(nameof(data));
        }
        var bytes = Serialization.Encoding.GetBytes(data);
        return Encrypt(bytes, publicKey);
    }

    public static string EncryptStringToBase64(string data, RSAParameters publicKey)
    {
        if (string.IsNullOrEmpty(data))
        {
            throw new ArgumentIsNullOrEmptyException(nameof(data));
        }

        var bytes = Serialization.Encoding.GetBytes(data);
        return Base64.To(Encrypt(bytes, publicKey));
    }


    public static byte[] Encrypt(byte[] bytes, string publicKey)
        => Encrypt(bytes, RSAParametersFromString(publicKey));

    public static byte[] EncryptString(string data, string publicKey)
        => EncryptString(data, RSAParametersFromString(publicKey));

    public static string EncryptStringToBase64(string data, string publicKey)
        => EncryptStringToBase64(data, RSAParametersFromString(publicKey));




}
