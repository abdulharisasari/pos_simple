using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pos_simple.DTO;
using pos_simple.Model;
using pos_simple.Service;

namespace pos_simple.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiResponse<User>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            var newUser = await _authService.Register(request.Username, request.Password, request.Role);
            if (newUser == null)
                return BadRequest(new ApiResponse<object>
                {
                    Code = 400,
                    Message = "Username already exists",
                    Data = null
                });

            return Ok(new ApiResponse<User>
            {
                Code = 200,
                Message = "Register successful",
                Data = newUser
            });
        }


        //[HttpPost("login")]
        //[ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
        //public async Task<IActionResult> Login([FromBody] LoginRequest request)
        //{
        //    var loginResult = await _authService.Login(request.Username, request.Password);

        //    if (loginResult == null)
        //        return NotFound(new ApiResponse<object>
        //        {
        //            Code = 404,
        //            Message = "Username not found",
        //            Data = null
        //        });

        //    if (loginResult.Token == null)
        //        return Unauthorized(new ApiResponse<LoginResponse>
        //        {
        //            Code = 401,
        //            Message = "Incorrect password",
        //            Data = null
        //        });

        //    return Ok(new ApiResponse<LoginResponse>
        //    {
        //        Code = 200,
        //        Message = "Login successful",
        //        Data = loginResult
        //    });
        //}
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var loginResult = await _authService.Login(request.Username, request.Password);

            if (loginResult == null)
            {
                return NotFound(new LoginResponse
                {
                    Code = 404,
                    Message = "Username not found",
                    Token = string.Empty,
                    Data = null
                });
            }

            if (loginResult.Token == null)
            {
                return Unauthorized(new LoginResponse
                {
                    Code = 401,
                    Message = "Incorrect password",
                    Token = string.Empty,
                    Data = null
                });
            }

            return Ok(new LoginResponse
            {
                Code = 200,
                Message = "Login successful",
                Token = loginResult.Token,
                Data = loginResult.Data
            });
        }


    }
}
