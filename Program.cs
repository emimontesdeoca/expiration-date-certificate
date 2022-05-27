using Newtonsoft.Json;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

var url = "https://blog.emilianomontesdeoca.com/";

static async Task<X509Certificate2> CheckCertificateAsync(string urlPath)
{
    var certificate = new X509Certificate2();
    var httpClientHandler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (request, cert, chain, policyErrors) =>
            {
                certificate = new X509Certificate2(cert);
                return true;
            }
    };

    using HttpClient httpClient = new HttpClient(httpClientHandler);
    await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, urlPath));

    return certificate;
}

var cert = await CheckCertificateAsync(url);

var serializedValue = JsonConvert.SerializeObject(cert);

Console.WriteLine(serializedValue);

Console.ReadLine();