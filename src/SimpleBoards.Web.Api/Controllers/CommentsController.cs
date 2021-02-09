using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleBoards.Web.Models.Comments;
using SimpleBoards.Web.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace SimpleBoards.Web.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/issues/{issueId}/[controller]")]
    public class CommentsController : ControllerBase
    {
        public CommentsControllerServices ControllerServices { get; }

        public CommentsController(CommentsControllerServices controllerServices)
        {
            ControllerServices = controllerServices ?? throw new System.ArgumentNullException(nameof(controllerServices));
        }

        [HttpGet]
        public IActionResult GetComments(int issueId)
        {
            var model = ControllerServices.GetComments(issueId);
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetCommentDetail(int issueId, int id)
        {
            var model = ControllerServices.GetCommentDetail(issueId, id);
            if (model is null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewComment(int issueId, [FromBody]NewCommentModel model)
        {
            await ControllerServices.AddNewComment(issueId, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int issueId, int id)
        {
            await ControllerServices.DeleteComment(issueId, id);
            return Ok();
        }
    }
}