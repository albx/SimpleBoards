using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SimpleBoards.Web.Api.Controllers
{
    [ApiController]
    [Route("api/issues/{issueId}/[controller]")]
    public class CommentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetComments(int issueId)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetCommentDetail(int issueId, int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewComment(int issueId)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int issueId, int id)
        {
            return Ok();
        }
    }
}