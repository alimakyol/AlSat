using System.ComponentModel.DataAnnotations;

namespace AlSat.Data.Models
{
	public class User
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string NormalizedUserName { get; set; }
		public string Email { get; set; }
		public string NormalizedEmail { get; set; }
		public string PhoneNumber { get; set; }
		public string PasswordHash { get; set; }
		public bool EmailConfirmed { get; set; }
		public bool PhoneConfirmed { get; set; }
		public string Token { get; set; }
		public bool IsActive { get; set; }

		[Timestamp]
		public byte[] RowVersion { get; set; }
	}
}
