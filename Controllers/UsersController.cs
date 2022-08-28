using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_Examinationen.Models.User;
using WebApi_Examinationen.Services;
using static WebApi_Examinationen.Services.UserService;

namespace WebApi_Examinationen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserRequest request)
        {
            var item = await _userService.CreateUserAsync(request);
            if (item != null)
                return new OkObjectResult(item);

            return new BadRequestResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return new OkObjectResult(await _userService.GetAllUsersAsync());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _userService.GetUserAsync(id);
            if (item != null)
                return new OkObjectResult(item);

            return new NotFoundResult();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserRequest request)
        {
            var item = await _userService.UpdateUserAsync(id, request);
            if (item != null)
                return new OkObjectResult(item);

            return new BadRequestResult();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _userService.DeleteUserAsync(id))
                return new OkResult();

            return new BadRequestResult();
        }
    }
}
