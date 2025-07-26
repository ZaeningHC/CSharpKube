using Microsoft.AspNetCore.Mvc;
using CSharpKube.Models;
using CSharpKube.Services;
using System.Threading.Tasks;

namespace CSharpKube.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KubeController : ControllerBase
    {
        private readonly KubeService _kubeService;

        public KubeController(KubeService kubeService)
        {
            _kubeService = kubeService;
        }

        // GET for testing purposes
        [HttpGet]
        public ActionResult<Kube> Get()
        {
            var sampleKube = new Kube
            {
                Id = 1,
                Name = "Sample Kube",
                Description = "This is a test object for the C# API."
            };

            return Ok(sampleKube);
        }

        // POST for receiving data from Java app
        [HttpPost("receive")]
        public ActionResult ReceiveFromJava([FromBody] Kube kube)
        {
            if (kube == null)
            {
                return BadRequest("Invalid data.");
            }

            return Ok($"Received Kube from Java API: {kube.Name}");
        }

        // POST for sending data to Java app
        [HttpPost("send")]
        public async Task<ActionResult> SendToJava([FromBody] Kube kube)
        {
            if (kube == null)
            {
                return BadRequest("Invalid data.");
            }

            var response = await _kubeService.SendKubeToJavaApi(kube);

            if (response.IsSuccessStatusCode)
            {
                return Ok("Data sent successfully.");
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Failed to send data to Java API.");
            }
        }
    }
}
