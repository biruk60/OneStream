using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FrontendWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FrontEndController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FrontEndController> _logger;
        private readonly IConfiguration _iConfig;

        public FrontEndController(ILogger<FrontEndController> logger, HttpClient httpClient, IConfiguration iConfig)
        {
            _logger = logger;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _iConfig = iConfig;
        }

               
        [HttpGet(Name = "CallAPI2")]
        public async Task<IActionResult> CallAPI2()
        {
           
            string api2Url = _iConfig.GetSection("customSettings").GetSection("APIUrl2").Value + "Get";
            var toast = _httpClient.GetAsync(api2Url);

            await Task.WhenAll(toast);
            var response1 = await toast;

            if (response1.IsSuccessStatusCode)
            {
                return Ok(new
                {
                    DataFromAPI2 = await response1.Content.ReadFromJsonAsync<List<User>>()
                });
            }
            else
            {
                return StatusCode(500, "Error invoking external APIs");
            }
        }

        [HttpPost("PostToAPI2")]
        public async Task<IActionResult> PostToAPI2(User user)
        {
            
            string api2Url = _iConfig.GetSection("customSettings").GetSection("APIUrl2").Value + "Send";

            try
            {
                               
                // Convert data to JSON
                var json = System.Text.Json.JsonSerializer.Serialize(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send POST request
                var response = await _httpClient.PostAsync(api2Url, content);

                // Check if request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Process successful response
                    var responseData = await response.Content.ReadAsStringAsync();
                    return Ok(responseData);
                }
                else
                {
                    // Handle unsuccessful response
                    return StatusCode((int)response.StatusCode, "Error sending POST request");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet(Name = "CallAPI3")]
        public async Task<IActionResult> CallAPI3()
        {

            string api3Url = _iConfig.GetSection("customSettings").GetSection("APIUrl3").Value + "Get";
            var toast = _httpClient.GetAsync(api3Url);

            await Task.WhenAll(toast);
            var response1 = await toast;

            if (response1.IsSuccessStatusCode)
            {
                return Ok(new
                {
                    DataFromAPI3 = await response1.Content.ReadFromJsonAsync<List<Address>>()
                });
            }
            else
            {
                return StatusCode(500, "Error invoking external APIs");
            }
        }

        [HttpPost("PostToAPI3")]
        public async Task<IActionResult> PostToAPI3(Address address)
        {

            string api3Url = _iConfig.GetSection("customSettings").GetSection("APIUrl3").Value + "Send";

            try
            {

                // Convert data to JSON
                var json = System.Text.Json.JsonSerializer.Serialize(address);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send POST request
                var response = await _httpClient.PostAsync(api3Url, content);

                // Check if request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Process successful response
                    var responseData = await response.Content.ReadAsStringAsync();
                    return Ok(responseData);
                }
                else
                {
                    // Handle unsuccessful response
                    return StatusCode((int)response.StatusCode, "Error sending POST request");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
