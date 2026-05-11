using HouseRentingSystemApi.Data;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystemApi.Middlewares
{
	public class CustomMiddleware
	{
		private readonly RequestDelegate next;

		public CustomMiddleware(RequestDelegate next)
		{
			this.next = next;
		}


		public async Task InvokeAsync(HttpContext context, AppDbContext ctx) 
		{
			var housesCount = await ctx.Houses.CountAsync();
			Console.WriteLine($"Total houses in DB = {housesCount}");
			
			await this.next(context);

			var housesCountNew = await ctx.Houses.CountAsync();
			if (housesCount != housesCountNew)
			{
				Console.WriteLine($"There is {housesCountNew- housesCount} new houses");
			}
			else
			{
				Console.WriteLine("No new houses added.");
			}


		}

	}
}
