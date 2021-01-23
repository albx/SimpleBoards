using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SimpleBoards.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser()
        {
            return Ok();
        }
    }
}