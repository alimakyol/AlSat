using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
