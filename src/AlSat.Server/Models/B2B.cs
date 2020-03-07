using System;
using System.ComponentModel.DataAnnotations;
using System.Spatial;
using AlSat.Server.Enums;

namespace AlSat.Server.Models
{
	public class B2B
	{
		public int Id { get; set; }

		public int Business1Id { get; set; }
		public int Business2Id { get; set; }

		public B2BStatus Status { get; set; } = B2BStatus.Active;

		public virtual Business Business1 { get; set; }
		public virtual Business Business2 { get; set; }

	}
}
