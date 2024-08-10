using EmergencyNotificationSystem.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmergencyNotificationSystem.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var res = await _userService.GetById(id);

            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.Delete(id);

            return Ok();
        }
    }
}
