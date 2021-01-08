using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleBoards.Core.Models;

namespace SimpleBoards.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IssuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetIssues()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetIssueDetail(int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewIssue()
        {
            return Ok();
        }

        [HttpPatch("{id}/assign")]
        public async Task<IActionResult> AssignIssue(int id)
        {
            return Ok();
        }

        [HttpPatch("{id}/start")]
        public async Task<IActionResult> StartWorkingOnIssue(int id)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CloseIssue(int id)
        {
            return Ok();
        }

        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> CompleteIssue(int id)
        {
            return Ok();
        }

        [HttpPatch("{id}/reject")]
        public async Task<IActionResult> RejectIssue(int id)
        {
            return Ok();
        }

        [HttpPatch("{id}/testing")]
        public async Task<IActionResult> MoveIssueInTesting(int id)
        {
            return Ok();
        }
    }
}