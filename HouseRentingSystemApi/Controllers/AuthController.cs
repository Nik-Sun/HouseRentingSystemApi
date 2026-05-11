using HouseRentingSystemApi.Services.Contracts;
using HouseRentingSystemApi.Services.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystemApi.Controllers
{
	[Route("api/[controller]")]
	public class AuthController : Controller
	{
		private readonly IAuthService authService;

		public AuthController(IAuthService authService)
		{
			this.authService = authService;
		}

		[HttpPost("login")]
		[Produces(typeof(AuthResult))]
		public async Task<IActionResult> Login([FromBody] AuthModel model)
		{
			if (ModelState.IsValid == false)
			{
				var allErrors = ModelState.Values
					.SelectMany(v => v.Errors)
					.Select(e => e.ErrorMessage)
					.ToArray();

				return BadRequest(PopulateResult(400, null, allErrors));
			}

			var result = await authService.LoginAsync(model);
			if (result.Code != 200)
			{
				return Unauthorized(result);
			}

			return Ok(result);
		}

		[HttpPost("register")]
		[Produces(typeof(AuthResult))]
		public async Task<IActionResult> Resgister([FromBody] AuthModel model)
		{
			if (ModelState.IsValid == false)
			{
				var allErrors = ModelState.Values
					.SelectMany(v => v.Errors)
					.Select(e => e.ErrorMessage)
					.ToArray();

				return Unauthorized(PopulateResult(400, null, allErrors));
			}

			var result = await authService.RegisterAsync(model);
			if (result.Code != 200)
			{
				return BadRequest(result);
			}

			return Ok(result);
		}

		private static AuthResult PopulateResult(int code, string? token = null, params string[] messages)
		{
			return new AuthResult
			{
				Code = code,
				Message = string.Join(Environment.NewLine, messages),
				Token = token
			};
		}
	}
}
