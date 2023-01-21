using Cryptography;


namespace Tests;


public class TestCryptography
{
    static readonly string StringForTests = "string for tests";

    [Fact]
    public void HMACTest() => HMAC.SHA256toBase64(StringForTests, StringForTests)
        .Should().Be("Iiv5SlTNzkJ4HTJIhE80jCUPvfqv3eTIF3sbPsMaUdU=");

    [Fact]
    public void MD5Test() => MD5.Compute(StringForTests)
        .Should().Be("0d1087f225917e069af42119f81bfdbd");


    [Fact]
    public void SHA256Test() => SHA.SHA256(StringForTests)
        .Should().Be("027B23DC863EBDBB680AE94ECD83CD4232E519EF7C263F332CA9A32DD9B54C21");

    [Fact]
    public void SHA512Test() => SHA.SHA512(StringForTests)
        .Should().Be("F3DED6C1254A6E901B121A85957312A967F2DCBC40D6CE2F7E70C86B48749CB58CD0C6832DC8A735AC7551EDE60F0D99C667A7A9572095178C46E435A55FD58A");


    [Fact]
    public void AesTest()
    {
        var key = MD5.Compute(StringForTests);

        var EncToBase64 = Aes.EncryptFromStringToBase64(StringForTests, key);

        var DecFromBase64 = Aes.DecryptFromBase64ToString(EncToBase64, key);

        DecFromBase64.Should().Be(StringForTests);
    }
}