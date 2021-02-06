using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleBoards.Web.Models.Issues;
using SimpleBoards.Web.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace SimpleBoards.Web.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class IssuesController : ControllerBase
    {
        public IssuesControllerServices ControllerServices { get; }

        public IssuesController(IssuesControllerServices controllerServices)
        {
            ControllerServices = controllerServices ?? throw new System.ArgumentNullException(nameof(controllerServices));
        }

        [HttpGet]
        public IActionResult GetIssues(int boardId)
        {
            var model = ControllerServices.GetIssuesListForBoard(boardId);
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetIssueDetail(int id)
        {
            var model = ControllerServices.GetIssueDetail(id);
            if (model is null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> OpenNewIssue([FromBody]NewIssueModel model)
        {
            var newIssueId = await ControllerServices.OpenNewIssue(model);
            return CreatedAtAction(nameof(GetIssueDetail), new { id = newIssueId }, model);
        }

        [HttpPatch("{id}/assign")]
        public async Task<IActionResult> AssignIssue(int id, [FromBody]AssignIssueModel model)
        {
            await ControllerServices.AssigneIssue(id, model);
            return Ok();
        }

        [HttpPatch("{id}/start")]
        public async Task<IActionResult> StartWorkingOnIssue(int id)
        {
            await ControllerServices.StartWorkingOnIssue(id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CloseIssue(int id)
        {
            await ControllerServices.CloseIssue(id);
            return Ok();
        }

        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> CompleteIssue(int id)
        {
            await ControllerServices.CompleteIssue(id);
            return Ok();
        }

        [HttpPatch("{id}/reject")]
        public async Task<IActionResult> RejectIssue(int id)
        {
            await ControllerServices.RejectIssue(id);
            return Ok();
        }

        [HttpPatch("{id}/testing")]
        public async Task<IActionResult> MoveIssueToTesting(int id, [FromBody]MoveIssueToTestingModel model)
        {
            await ControllerServices.MoveIssueToTesting(id, model);
            return Ok();
        }
    }
}