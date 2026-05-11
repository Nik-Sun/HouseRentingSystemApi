using HouseRentingSystemApi.Services.Contracts;
using HouseRentingSystemApi.Services.Models.House;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouseRentingSystemApi.Controllers
{
	[Route("api/[controller]")]
	public class HouseController : ControllerBase
	{
		private readonly IHouseService houseService;

		public HouseController(IHouseService houseService)
		{
			this.houseService = houseService;
		}

		[HttpGet]
		[Produces(typeof(RequestResult<IEnumerable<HouseDetailModel>>))]
		public async Task<IActionResult> GetAll()
		{
			var result = await houseService.GetAllAsync();
			return Ok(result);
		}

		[HttpGet("{id}")]
		[Produces(typeof(HouseDetailModel))]
		public async Task<IActionResult> GetById(int id)
		{
			var house = await houseService.GetByIdAsync(id);
			if (house == null)
			{
				return NotFound();
			}

			return Ok(house);
		}

		[Authorize]
		[HttpPost]
		[Produces(typeof(HouseDetailModel))]
		public async Task<IActionResult> Create([FromBody] HouseDetailModel model)
		{
			if (ModelState.IsValid == false)
			{
				return BadRequest();
			}

			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var createdHouse = await houseService.CreateAsync(model, userId);

			return CreatedAtAction(nameof(GetById), new { id = createdHouse.Id }, createdHouse);
		}

		[Authorize]
		[HttpPut("{id}")]
		public async Task<IActionResult> Edit(int id, HouseDetailModel model)
		{
			if (ModelState.IsValid == false)
			{
				var allErrors = ModelState.Values
					.SelectMany(v => v.Errors)
					.Select(e => e.ErrorMessage)
					.ToArray();
				return BadRequest(string.Join(", ", allErrors));
			}

			return await Task.FromResult(Ok());
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			return await Task.FromResult(Ok());
		}
	}
}
