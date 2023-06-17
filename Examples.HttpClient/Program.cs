using System.Net;

print("Start");



CookieContainer container = new CookieContainer();
using HttpClientHandler handler = new HttpClientHandler()
{
    CookieContainer = container,
    UseCookies = true,
    ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true,
};
{
    using HttpClient client = new HttpClient(handler);
    {
        var response = await client.GetAsync("https://example.com");
        string responseText = await response.Content.ReadAsStringAsync();

        print(responseText);
    }

}