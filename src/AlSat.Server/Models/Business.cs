using System;
using System.ComponentModel.DataAnnotations;
using System.Spatial;

using AlSat.Server.Enums;

namespace AlSat.Server.Models
{
	public class Business
	{
		public int Id { get; set; }

		/// <summary>
		/// Business ID which this business is the supplier and/or customer of.
		/// </summary>
		public int? OwnerId { get; set; }

		public Guid PrivateGuid { get; set; } = Guid.NewGuid();

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		public BusinessStatus Status { get; set; } = BusinessStatus.Active;

		public bool IsPrivate { get; set; }

		[StringLength(20)]
		public string Phone { get; set; }

		[StringLength(200)]
		public string Address { get; set; }

		[StringLength(50)]
		public string City { get; set; }

		[StringLength(50)]
		public string Country { get; set; }

		public Geography Geography { get; set; }

		[Timestamp]
		public byte[] RowVersion { get; set; }
	}
}
