namespace WebApplication48.Models
{
	public enum StatusCode
	{
		Ok = 200,
		NotFound = 404,
		Error = 500
	}

	public class BaseResponse<T>
	{
		public T Data { get; set; }
		public string Message { get; set; }
		public StatusCode Status { get; set; }

		public BaseResponse()
		{
			Status = StatusCode.Ok;
		}

	}
}
