using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CSharpKube.Models;

namespace CSharpKube.Services
{
    public class KubeService
    {
        private readonly HttpClient _httpClient;

        public KubeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> SendKubeToJavaApi(Kube kube)
        {
            var jsonContent = JsonConvert.SerializeObject(kube);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var javaApiUrl = "http://java-api:8080/api/java/receive"; // Call the "receive" endpoint in Java

            var response = await _httpClient.PostAsync(javaApiUrl, content);

            return response;
        }
    }
}
