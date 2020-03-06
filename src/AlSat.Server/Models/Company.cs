using System.ComponentModel.DataAnnotations;

namespace AlSat.Server.Models
{
	public class Company
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		public bool IsActive { get; set; } = true;

		[StringLength(200)]
		public string Address { get; set; }

		[StringLength(20)]
		public string City { get; set; }

		[StringLength(20)]
		public string Phone { get; set; }

		public bool IsSupplier { get; set; }
		public bool IsCustomer { get; set; } = true;

		[Timestamp]
		public byte[] RowVersion { get; set; }
	}
}
