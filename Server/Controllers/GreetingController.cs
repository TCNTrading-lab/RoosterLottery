using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GreetingController : ControllerBase
    {
        [HttpGet(Name = "GetGreeting")]
        public Greeting Get()
        {
            return new Greeting { Message = "Good luck!" };
        }
    }
}
