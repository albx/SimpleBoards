using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SimpleBoards.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBoards()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetBoardDetail(int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewBoard()
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditBoard(int id)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(int id)
        {
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> RestoreBoard(int id)
        {
            return Ok();
        }
    }
}