using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Server.Controllers
{
    [ApiController()]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {

        [HttpPost("findPlayer")]

        public Player findPlayerByPhoneNumber([FromBody]  SearchPlayer s)
        {

            return null;
        }
    }
    public class SearchPlayer
    {
        [MaxLength (15)]
        
        public string? phoneNumber { get; set; }
    }
}
