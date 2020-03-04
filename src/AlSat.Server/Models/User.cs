using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlSat.Server.Models
{
	public class User
	{
		public int Id { get; set; }
		public int? ManagerId { get; set; }
		public string UserName { get; set; }
		public string NormalizedUserName { get; set; }
		public string Email { get; set; }
		public string NormalizedEmail { get; set; }
		public string PhoneNumber { get; set; }
		public string PasswordHash { get; set; }
		public bool EmailConfirmed { get; set; }
		public bool PhoneConfirmed { get; set; }
		public string Token { get; set; }
		public bool IsManager { get; set; }
		public bool IsActive { get; set; } = true;

		[Timestamp]
		public byte[] RowVersion { get; set; }

		public virtual User Manager { get; set; }
		public virtual IList<User> Employees { get; set; }
	}
}
