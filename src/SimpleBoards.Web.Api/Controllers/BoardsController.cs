using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleBoards.Web.Api.Models.Boards;
using SimpleBoards.Web.Api.Services;

namespace SimpleBoards.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardsController : ControllerBase
    {
        public BoardsControllerServices ControllerServices { get; }

        public BoardsController(BoardsControllerServices controllerServices)
        {
            ControllerServices = controllerServices ?? throw new ArgumentNullException(nameof(controllerServices));
        }

        [HttpGet]
        public IActionResult GetBoards()
        {
            var model = ControllerServices.GetBoardsList();
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetBoardDetail(int id)
        {
            var model = ControllerServices.GetBoardDetail(id);
            if (model is null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewBoard([FromBody]BoardModel model)
        {
            var newBoardId = await ControllerServices.CreateNewBoard(model);
            return CreatedAtAction(nameof(GetBoardDetail), new { id = newBoardId }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditBoard(int id, [FromBody]BoardModel model)
        {
            await ControllerServices.EditBoard(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(int id)
        {
            await ControllerServices.DeleteBoard(id);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> RestoreBoard(int id)
        {
            await ControllerServices.RestoreBoard(id);
            return Ok();
        }
    }
}