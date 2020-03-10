using System;
using System.ComponentModel.DataAnnotations;

namespace AlSat.Server.Models
{
	public class User
	{
		public int Id { get; set; }
		public int BusinessId { get; set; }

		public Guid PrivateGuid { get; set; } = Guid.NewGuid();

		[Required]
		[StringLength(20)]
		public string PhoneNumber { get; set; }

		public bool IsPhoneConfirmed { get; set; }
		public bool IsActive { get; set; } = true;

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[StringLength(100)]
		public string PasswordHash { get; set; }

		[Required]
		[StringLength(1000)]
		public string Token { get; set; }

		public bool IsManager { get; set; }

		[Timestamp]
		public byte[] RowVersion { get; set; }

		public virtual Business Business { get; set; }
	}
}
