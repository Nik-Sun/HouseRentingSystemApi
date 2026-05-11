namespace HouseRentingSystemApi.Models.House
{
	public class RequestResult<T>
	{
		public int Code { get; set; }
		public string Message { get; set; }

		public T Data { get; set; }
	}
}
