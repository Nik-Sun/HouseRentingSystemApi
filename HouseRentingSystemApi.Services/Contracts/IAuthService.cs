using HouseRentingSystemApi.Services.Models.Auth;

namespace HouseRentingSystemApi.Services.Contracts
{
	public interface IAuthService
	{
		Task<AuthResult> LoginAsync(AuthModel model);

		Task<AuthResult> RegisterAsync(AuthModel model);
	}
}
