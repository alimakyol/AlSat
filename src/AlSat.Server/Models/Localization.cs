using System.ComponentModel.DataAnnotations;

using AlSat.Server.Interfaces;

namespace AlSat.Server.Models
{
	public class Localization : IAuditable
	{
		public int Id { get; set; }

		[Required]
		[StringLength(10)]
		public string CultureCode { get; set; }

		[Required]
		public string KeyText { get; set; }

		public string Translation { get; set; }
	}
}
