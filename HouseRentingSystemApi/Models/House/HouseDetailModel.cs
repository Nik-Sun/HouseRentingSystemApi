using HouseRentingSystemApi.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

using static HouseRentingSystemApi.Data.DataConstants.DataConstants.House;

namespace HouseRentingSystemApi.Models.House
{
	public class HouseDetailModel
	{

		[MaxLength(TitleMaxLength)]
		[Required(ErrorMessage = "Test Error Msg")]
		public string Title { get; set; }

		[MaxLength(AddressMaxLength)]
		public string  Address { get; set; }
		public string ImageUrl { get; set; }
		public string Description { get; set; }
		public decimal PricePerMonth { get; set; }

		public CategoryViewEnum Category { get; set; }
	}
}
