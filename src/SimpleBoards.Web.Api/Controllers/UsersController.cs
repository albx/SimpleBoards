using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleBoards.Web.Models.Users;
using SimpleBoards.Web.Api.Services;

namespace SimpleBoards.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        public UsersControllerServices ControllerServices { get; }

        public UsersController(UsersControllerServices controllerServices)
        {
            ControllerServices = controllerServices ?? throw new ArgumentNullException(nameof(controllerServices));
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var model = ControllerServices.GetUsersList();
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserDetail(string id)
        {
            var model = ControllerServices.GetUserDetail(id);
            if (model is null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterUserModel model)
        {
            var userId = await ControllerServices.RegisterUser(model);
            return CreatedAtAction(nameof(GetUserDetail), new { id = userId }, model);
        }
    }
}