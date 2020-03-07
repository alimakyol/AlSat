using System.ComponentModel.DataAnnotations;

namespace AlSat.Server.Models
{
	public class Product
	{
		public int Id { get; set; }

		public int BusinessId { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		[StringLength(100)]
		public string NormalizedName { get; set; }

		public bool IsActive { get; set; } = true;
		public decimal PurchasePrice { get; set; }
		public decimal SalesPrice { get; set; }

		[StringLength(10)]
		public string Unit { get; set; }

		public bool IsDefault { get; set; }

		[Timestamp]
		public byte[] RowVersion { get; set; }

		public virtual Business Business { get; set; }
	}
}
