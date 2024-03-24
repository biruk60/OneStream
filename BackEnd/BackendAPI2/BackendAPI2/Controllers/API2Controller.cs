using Microsoft.AspNetCore.Mvc;

namespace BackendAPI2.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class API2Controller : ControllerBase
    {         

       public List<User> UsersList = new List<User>()
        {
             new User { FirstName = "Iris", LastName = "Jackson", Location = "API2" },
             new User { FirstName = "Sebastian", LastName = "Tyler", Location = "API2" },
            new User { FirstName = "Helena", LastName = "Boyd", Location = "API2" },
            new User { FirstName = "Dean", LastName = "Shields", Location = "API2" },
        };
        
        private readonly ILogger<API2Controller> _logger;

        public API2Controller(ILogger<API2Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAsync")]
        public  async Task<IEnumerable<User>> GetAsync()
        {
            // Simulate a delay to mimic a longer running operation
            await Task.Delay(3000);
            return UsersList;
        }

        [HttpPost]        
        public async Task<IActionResult> Send(User user)
        {
            //var message = "Message from API2: I have got your post!";
            await Task.Delay(3000);
            return Ok("Message from API2: I have got your post!");
        }
    }
}
