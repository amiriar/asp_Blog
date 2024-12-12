using Blog.Models.Dtos;
using Blog.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenRepository tokenRepository;

        public UserManager<IdentityUser> UserManager { get; set; }

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository _tokenRepository)
        {
            UserManager = userManager;
            tokenRepository = _tokenRepository;
        }


        [HttpPost]
        [Route("Register")]
        //[ValidateModel]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = new IdentityUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Username,
            };
            var identityResult = await UserManager.CreateAsync(user, registerDto.Password);

            if (identityResult.Succeeded)
            {
                // add roles
                if (registerDto.Roles != null && registerDto.Roles.Any())
                {
                    identityResult = await UserManager.AddToRolesAsync(user, registerDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("Registered Successfully, Log in now");
                    }
                }
            }
            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("Login")]
        //[ValidateModel]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await UserManager.FindByEmailAsync(loginDto.Username);
            if (user != null)
            {
                var checkPasswordResult = await UserManager.CheckPasswordAsync(user, loginDto.Password);

                if (checkPasswordResult)
                {
                    var roles = await UserManager.GetRolesAsync(user);
                    if (roles != null)
                    {

                        // create token
                        var token = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var respone = new LoginResponseDto
                        {
                            Token = token,
                        };

                        return Ok(respone);
                    }
                }
            }
            return BadRequest("Something went wrong");
        }
    }
}
