using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using AlSat.Data.Interfaces;

namespace AlSat.Data.Models
{
	public class Localization : IAuditable
	{
		[Key]
		[Column(Order = 0)]
		[Required]
		[StringLength(20)]
		public string CultureCode { get; set; }

		[Key]
		[Column(Order = 1)]
		[Required]
		[StringLength(400)]
		public string KeyText { get; set; }

		[StringLength(1000)]
		public string Translation { get; set; }

		#region IAuditInfo Members

		//[Index]
		//public int? UserId_CreatedBy { get; set; }

		//[DateTimeKind(DateTimeKind.Utc)]
		//[Index]
		//public DateTime? CreatedDateTime { get; set; }

		//[Index]
		//public int? UserId_LastUpdatedBy { get; set; }

		//[DateTimeKind(DateTimeKind.Utc)]
		//[Index]
		//public DateTime? LastUpdatedDateTime { get; set; }

		//[ForeignKey("UserId_CreatedBy")]
		//public virtual User User_CreatedBy { get; set; }

		//[ForeignKey("UserId_LastUpdatedBy")]
		//public virtual User User_LastUpdatedBy { get; set; }

		#endregion IAuditInfo Members
	}
}
