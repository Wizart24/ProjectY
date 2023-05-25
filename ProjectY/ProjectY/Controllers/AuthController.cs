﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectY.Data;
using ProjectY.Dtos.User;
using ProjectY.Models;

namespace ProjectY.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthRepository _authRepository;

		public AuthController(IAuthRepository authRepository)
        {
			_authRepository = authRepository;
		}

		[HttpPost("Register")]
		public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
		{
			var response = await _authRepository.Register(
				new User { Username = request.Username }, request.Password
			);

			if (!response.Success)
			{
				return BadRequest(response);
			}
			return Ok(response);
		}

		[HttpPost("Login")]
		public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginDto request)
		{
			var response = await _authRepository.Login(request.Username, request.Password);

			if (!response.Success)
			{
				return BadRequest(response);
			}
			return Ok(response);
		}
	}
}
