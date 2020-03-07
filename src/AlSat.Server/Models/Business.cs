using System;
using System.Collections.Generic;
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

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		[StringLength(100)]
		public string NormalizedName { get; set; }

		public Guid PrivateGuid { get; set; } = Guid.NewGuid();

		public BusinessStatus Status { get; set; } = BusinessStatus.Active;

		[StringLength(200)]
		public string Address { get; set; }

		[StringLength(20)]
		public string City { get; set; }

		[StringLength(20)]
		public string Phone { get; set; }

		public Geography Geography { get; set; }

		[Timestamp]
		public byte[] RowVersion { get; set; }
	}
}
