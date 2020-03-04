using System;
using System.ComponentModel.DataAnnotations;

namespace AlSat.Server.Models
{
	public class LogAudit
	{
		public int Id { get; set; }

		public DateTime TimeStamp { get; set; }

		[StringLength(10)]
		public string Level { get; set; }

		[StringLength(100)]
		public string Controller { get; set; }

		[StringLength(100)]
		public string Action { get; set; }

		[StringLength(100)]
		public string Logger { get; set; }

		[StringLength(50)]
		public string ClientIpAddress { get; set; }

		[StringLength(2000)]
		public string Url { get; set; }

		[StringLength(10)]
		public string HttpMethod { get; set; }

		public int? CompanyId { get; set; }

		public int? UserId { get; set; }

		[StringLength(100)]
		public string Email { get; set; }

		[StringLength(100)]
		public string UserFullName { get; set; }

		public string Message { get; set; }

		[StringLength(100)]
		public string EntityName { get; set; }

		[StringLength(10)]
		public string DbAction { get; set; }

		[StringLength(50)]
		public string KeyFields { get; set; }

		public string ChangedFields { get; set; }

		public int? TimeElapsed { get; set; }
	}
}
