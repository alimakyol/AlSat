using AlSat.Server.Enums;

namespace AlSat.Server.VModels
{
	public class BaseResponse
	{
		public ResponseStatus Status { get; set; }
		public string Message { get; set; }

		public dynamic Data { get; set; }
	}
}
