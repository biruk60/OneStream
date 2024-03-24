using Microsoft.AspNetCore.Mvc;

namespace BackendAPI3.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class API3Controller : ControllerBase
    {

        public List<Address> addressList = new List<Address>()
        {
             new Address { Street = "686 Newcastle Court", City = "Lake Villa", State = "IL", ZipCode="60046" },
              new Address { Street = "18 Albany Lane", City = "Fleming Island", State = "FL", ZipCode="32003" },
             new Address { Street = "9253 Orange Drive", City = "Mooresville", State = "NC", ZipCode="28115" },
             new Address { Street = "8201 Sussex Street", City = "Marietta", State = "GA", ZipCode="30008" }
        };

        private readonly ILogger<API3Controller> _logger;

        public API3Controller(ILogger<API3Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAsync")]
        public async Task<IEnumerable<Address>> GetAsync()
        {
            // Simulate a delay to mimic a longer running operation
            await Task.Delay(3000);
            return addressList;
        }

        [HttpPost]
        public async Task<IActionResult> Send(Address address)
        {
            //var message = "Message from API2: I have got your post!";
            await Task.Delay(3000);
            return Ok("Message from API3: I have got your post!");
        }
    }
}
