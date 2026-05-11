using HouseRentingSystemApi.Services.Models.House;

namespace HouseRentingSystemApi.Services.Contracts
{
	public interface IHouseService
	{
		Task<RequestResult<List<HouseDetailModel>>> GetAllAsync();

		Task<HouseDetailModel?> GetByIdAsync(int id);

		Task<HouseDetailModel> CreateAsync(HouseDetailModel model, string? userId);
	}
}
