using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.Main.DTO;
using server.Main.DTO.User;
using server.Main.Models;
using server.Main.Service;

namespace server.Main.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _iAuthService;
        public AuthController(IAuthService iAuthService)
        {
            _iAuthService = iAuthService;
        }

        // REGISTER //
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(UserRegisterDto request)
        {
            // Validate and register the new user
            var response = await _iAuthService.Register(new User { Username = request.Username, Email = request.Email }, request.Password);

            // Return the response message
            if (!response.Success) return BadRequest(response.Message);
            return Ok(response.Message);
        }

        // LOGIN //
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto request)
        {
            // Log in the user
            var response = await _iAuthService.Login(request.Username, request.Password);

            // Return the response message
            if (!response.Success) return BadRequest(response.Message);
            return Ok(response);
        }

        // CHECK ALLOWED //
        [Authorize]
        [HttpGet("loggedIn")]
        public ActionResult<string> Allowed()
        {
            var response = "Success";
            return Ok(response);
        }

        // CHANGE PASSWORD //
        [Authorize]
        [HttpPost("changePassword")]
        public async Task<ActionResult<string>> ChangePassword(UserChangePasswordDto request)
        {
            // Validate and change password
            var response = await _iAuthService.ChangePassword(request.OldPassword, request.NewPassword);

            // Return the response message
            if (!response.Success) return BadRequest(response.Message);
            return Ok(response.Message);
        }
    }
}