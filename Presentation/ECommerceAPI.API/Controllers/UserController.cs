using ECommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using ECommerceAPI.Application.Features.Commands.AppUser.FaceBookLogin;
using ECommerceAPI.Application.Features.Commands.AppUser.GoogleLogin;
using ECommerceAPI.Application.Features.Commands.AppUser.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IMediator _meditaor;

        public UserController(IMediator meditaor)
        {
            _meditaor = meditaor;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await _meditaor.Send(createUserCommandRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]

        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _meditaor.Send(loginUserCommandRequest);

            return Ok(response);
        }

        [HttpPost("google-login")]

        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest)
        {

            GoogleLoginCommandResponse response = await _meditaor.Send(googleLoginCommandRequest);
            return Ok(response);

        }

        [HttpPost("facebook-login")]

        public async Task<IActionResult> FaceBooklogin(FacebookLoginCommandRequest facebookLoginCommandRequest)
        {

            FacebookLoginCommandResponse response = await _meditaor.Send(facebookLoginCommandRequest);
            return Ok(response);

        }


    }
}
