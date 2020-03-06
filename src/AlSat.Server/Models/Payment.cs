using System;

using AlSat.Server.Enums;

namespace AlSat.Server.Models
{
	public class Payment
	{
		public int Id { get; set; }

		public PurchaseSalesType Type { get; set; }

		public DateTime PurchaseSalesDt { get; set; }

		public int CompanyId { get; set; }
		public int ProductId { get; set; }
		public decimal ProductQuantity { get; set; }
		public decimal PaymentAmount { get; set; }
		public bool IsPaid { get; set; }
		public string Notes { get; set; }
	}
}
