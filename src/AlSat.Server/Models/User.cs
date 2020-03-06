using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlSat.Server.Models
{
	public class User
	{
		public int Id { get; set; }
		public int? ManagerId { get; set; }

		[Required]
		[StringLength(100)]
		public string UserName { get; set; }

		[Required]
		[StringLength(100)]
		public string NormalizedUserName { get; set; }

		public bool IsActive { get; set; } = true;

		[Required]
		[StringLength(100)]
		public string Email { get; set; }

		[Required]
		[StringLength(100)]
		public string NormalizedEmail { get; set; }

		[Required]
		[StringLength(20)]
		public string PhoneNumber { get; set; }

		[Required]
		[StringLength(100)]
		public string PasswordHash { get; set; }

		public bool EmailConfirmed { get; set; }
		public bool PhoneConfirmed { get; set; }

		[Required]
		[StringLength(1000)]
		public string Token { get; set; }

		public bool IsManager { get; set; }

		[Timestamp]
		public byte[] RowVersion { get; set; }

		public virtual User Manager { get; set; }
		public virtual IList<User> Employees { get; set; }
	}
}
