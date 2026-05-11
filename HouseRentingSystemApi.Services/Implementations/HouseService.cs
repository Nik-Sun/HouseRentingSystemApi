using HouseRentingSystemApi.Data;
using HouseRentingSystemApi.Data.Entities;
using HouseRentingSystemApi.Services.Contracts;
using HouseRentingSystemApi.Services.Models.House;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HouseRentingSystemApi.Services.Implementations
{
	public class HouseService : IHouseService
	{
		private readonly AppDbContext context;
		private readonly IConfiguration config;

		public HouseService(AppDbContext context, IConfiguration config)
		{
			this.context = context;
			this.config = config;
		}

		public async Task<RequestResult<List<HouseDetailModel>>> GetAllAsync()
		{
			var houses = await context.Houses
				.AsNoTracking()
				.Select(h => new HouseDetailModel
				{
					Id = h.Id,
					Title = h.Title,
					Address = h.Address,
					ImageUrl = h.ImageUrl
				})
				.ToListAsync();

			var configData = config.GetSection("MyConfigData").Value;

			return new RequestResult<List<HouseDetailModel>>
			{
				Code = 200,
				Message = $"OK and data from config file = {configData}",
				Data = houses
			};
		}

		public async Task<HouseDetailModel?> GetByIdAsync(int id)
		{
			return await context.Houses
				.AsNoTracking()
				.Where(h => h.Id == id)
				.Select(h => new HouseDetailModel
				{
					Id = h.Id,
					Title = h.Title,
					Address = h.Address,
					ImageUrl = h.ImageUrl,
					Description = h.Description,
					PricePerMonth = h.PricePerMonth
				})
				.FirstOrDefaultAsync();
		}

		public async Task<HouseDetailModel> CreateAsync(HouseDetailModel model, string? userId)
		{
			var newHouse = new House
			{
				Description = model.Description,
				PricePerMonth = model.PricePerMonth,
				Address = model.Address,
				Title = model.Title,
				ImageUrl = model.ImageUrl,
				UserId = userId
			};

			var categoryName = model.Category.ToString();
			var category = await context.Categories
				.FirstOrDefaultAsync(c => c.Name == categoryName);

			if (category == null)
			{
				category = new Category
				{
					Name = categoryName
				};

				context.Categories.Add(category);
				await context.SaveChangesAsync();
			}

			newHouse.CategoryId = category.Id;

			context.Houses.Add(newHouse);
			await context.SaveChangesAsync();

			return new HouseDetailModel
			{
				Id = newHouse.Id,
				Address = newHouse.Address,
				ImageUrl = newHouse.ImageUrl,
				Title = newHouse.Title,
				Description = newHouse.Description,
				PricePerMonth = newHouse.PricePerMonth,
				Category = model.Category
			};
		}
	}
}
