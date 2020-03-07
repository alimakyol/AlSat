using System;

using AlSat.Server.Enums;

namespace AlSat.Server.Models
{
	public class Payment
	{
		public int Id { get; set; }

		public int B2BId { get; set; }

		public DateTime PaymentDt { get; set; }

		public decimal Amount { get; set; }
		public string Notes { get; set; }
	}
}
