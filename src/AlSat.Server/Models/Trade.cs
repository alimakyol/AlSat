using System;

using AlSat.Server.Enums;

namespace AlSat.Server.Models
{
	public class Trade
	{
		public int Id { get; set; }
		public int FromBusinessId { get; set; }
		public int ToBusinessId { get; set; }
		public int ProductId { get; set; }

		public DateTime TradeDt { get; set; }

		public decimal ProductQuantity { get; set; }
		public decimal? PaymentAmount { get; set; }
		public bool IsPaid { get; set; }
		public string Notes { get; set; }

		public virtual Business FromBusiness { get; set; }
		public virtual Business ToBusiness { get; set; }
		public virtual Product Product { get; set; }
	}
}
