using Cryptography;


namespace Net.Examles.ExamplesCryptography;



public record CryptographyExample(ILogger<CryptographyExample> logger) : Handler
{
    void print(string message) => logger.Info(message);

    static string StringForTests = "string for tests";

    public async Task Handle(CancellationToken token)
    {
        await Task.Delay(1, token);
        print("CryptographyExample");

        var HMACResult = HMAC.SHA256toBase64(StringForTests, StringForTests);
        print($"HMACResult: {HMACResult}");
        //Iiv5SlTNzkJ4HTJIhE80jCUPvfqv3eTIF3sbPsMaUdU=

        var MD5Result = MD5.Compute(StringForTests);
        print($"MD5Result: {MD5Result}");
        //0d1087f225917e069af42119f81bfdbd

        var sha256Result = SHA.SHA256(StringForTests);
        print($"sha256Result: {sha256Result}");
        //027B23DC863EBDBB680AE94ECD83CD4232E519EF7C263F332CA9A32DD9B54C21

        var sha512Result = SHA.SHA512(StringForTests);
        print($"sha512Result: {sha512Result}");
        //F3DED6C1254A6E901B121A85957312A967F2DCBC40D6CE2F7E70C86B48749CB58CD0C6832DC8A735AC7551EDE60F0D99C667A7A9572095178C46E435A55FD58A


        var key = MD5.Compute(StringForTests);
        print($"key: {key}");


        var EncToBase64 = Aes.EncryptFromStringToBase64(StringForTests, key);
        print($"EncryptFromStringToBase64: {EncToBase64}");
        var DecFromBase64 = Aes.DecryptFromBase64ToString(EncToBase64, key);
        print($"DecryptFromBase64ToString: {DecFromBase64}");


    }


}

