using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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
            var jsonContent = JsonSerializer.Serialize(kube, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var javaApiUrl = "http://java-api:8080/api/java/receive";

            return await _httpClient.PostAsync(javaApiUrl, content);
        }
    }
}
