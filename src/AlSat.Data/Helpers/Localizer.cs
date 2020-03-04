using System.Collections.Generic;
using System.Linq;

using AlSat.Data.DAL;
using AlSat.Data.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AlSat.Data.Helpers
{
	public static class Localizer
	{
		private const string C_PREFIX_SUFFIX_SEPARATOR = "_";

		private static SortedDictionary<string, Localization> mLocalizations;
		private static object mLockObject = new object();

		public static bool HasText(string prefix, string text)
		{
			return HasText(prefix + C_PREFIX_SUFFIX_SEPARATOR + text);
		}

		public static bool HasText(string prefix, string text, string suffix)
		{
			return HasText(prefix + C_PREFIX_SUFFIX_SEPARATOR + text + C_PREFIX_SUFFIX_SEPARATOR + suffix);
		}

		public static bool HasText(string text)
		{
			text = text.Trim();

			Localization localization = Find(text);

			if (localization == null)
				localization = AddText(text);

			return !string.IsNullOrEmpty(localization.Translation);
		}

		public static string Text(string prefix, string text)
		{
			prefix = prefix.Trim();
			text = text.Trim();

			string key = prefix + C_PREFIX_SUFFIX_SEPARATOR + text;

			if (HasText(key))
				return Text(key);

			return Text(text);
		}

		public static string Text(string prefix, string text, string suffix)
		{
			prefix = prefix.Trim();
			text = text.Trim();
			suffix = suffix.Trim();

			string key = prefix + C_PREFIX_SUFFIX_SEPARATOR + text + C_PREFIX_SUFFIX_SEPARATOR + suffix;

			if (HasText(key))
				return Text(key);

			return Text(text);
		}

		public static string Text(string text)
		{
			if (string.IsNullOrEmpty(text))
				return text;

			text = text.Trim();

			string retVal = text;

			Localization localization = Find(text);

			if (localization == null)
				localization = AddText(text);

			if (!string.IsNullOrEmpty(localization.Translation))
				retVal = localization.Translation;

			return retVal;
		}

		public static void RefreshCache()
		{
			mLocalizations = null;

			GetLocalizations();
		}

		public static string Command_Add()
		{
			return Text("Add");
		}

		public static string Command_AddNew()
		{
			return Text("Add New");
		}

		public static string Command_Create()
		{
			return Text("Create");
		}

		public static string Command_Edit()
		{
			return Text("Edit");
		}

		public static string Command_Save()
		{
			return Text("Save");
		}

		public static string Command_Search()
		{
			return Text("Search");
		}

		//public static List<SelectListItem> EnumSelectItemList<TEnum>(this TEnum value, bool selected = true)
		//{
		//	List<SelectListItem> enumValues = (from Enum e in Enum.GetValues(typeof(TEnum))
		//																		 select new SelectListItem
		//																		 {
		//																			 Selected = (selected && e.Equals(value)),
		//																			 Text = e.GetLocalizedText(),
		//																			 Value = e.ToString()
		//																		 })
		//																		.OrderBy(m => m.Text)
		//																		.ToList()
		//																		;

		//	return enumValues;
		//}

		//public static List<SelectListItem> EnumSelectItemList<TEnum>()
		//{
		//	return EnumSelectItemList<TEnum>(default(TEnum));
		//}

		//public static SelectList EnumSelectList<TEnum>(this TEnum value, bool selected = true)
		//{
		//	SelectList retVal = new SelectList(EnumSelectItemList<TEnum>(value, selected));

		//	return retVal;
		//}

		//public static SelectList EnumSelectList<TEnum>()
		//{
		//	return EnumSelectList<TEnum>(default(TEnum));
		//}

		public static string Field_IsPassive()
		{
			return Text("IsPassive");
		}

		public static string Error_ModelIsInvalid()
		{
			return Text("Mandatory fields must be filled.");
		}

		public static string Error_Title()
		{
			return Text("Error");
		}

		public static string Message_CanNotBeEmpty()
		{
			return Text("This field can not be blank");
		}

		public static string Message_NothingFound()
		{
			return Text("Nothing found.");
		}

		public static string Message_SameAnswer()
		{
			return Text("You can not add the same answer twice.");
		}

		public static string Message_CompanyAlreadyExist()
		{
			return Text("Company already exist.");
		}

		public static string Message_SelectCriteria()
		{
			return Text("Please enter or select some criteria values to begin search.");
		}

		public static string Message_SavedSuccessfully()
		{
			return Text("Saved successfully.");
		}

		public static string Watermark_AcEnterText()
		{
			return Text("Enter at least 3 characters");
		}

		public static string Watermark_EnterText()
		{
			return Text("Enter Text");
		}

		public static string Watermark_Select()
		{
			return Text("-- Select --");
		}

		public static string Watermark_SelectCompanyFirst()
		{
			return Text("-- Select Company first");
		}

		public static string Watermark_SelectCountryFirst()
		{
			return Text("-- Select Country first");
		}

		public static string Watermark_SelectCityFirst()
		{
			return Text("-- Select City first");
		}

		public static string Watermark_SelectCountyFirst()
		{
			return Text("-- Select County first");
		}

		public static string Watermark_ProjectTypeText()
		{
			return Text("Project Type");
		}

		private static SortedDictionary<string, Localization> GetLocalizations()
		{
			lock (mLockObject)
			{
				if (mLocalizations == null)
				{
					LocalizationDbContext db = HttpContext.RequestServices.GetService<LocalizationDbContext>();
					var list = db.Localization
						.AsNoTracking()
						.OrderBy(m => m.CultureCode)
						.ThenBy(m => m.KeyText)
						.ToList()
						;

					mLocalizations = new SortedDictionary<string, Localization>();

					foreach (var item in list)
					{
						string key = item.CultureCode.ToLower() + "_" + item.KeyText.ToLower();

						if (!mLocalizations.ContainsKey(key))
							mLocalizations.Add(key, item);
					}
				}

				return mLocalizations;
			}
		}

		private static Localization Find(string text)
		{
			string culture = ""; // CultureHelper.GetCurrentCultureName();

			SortedDictionary<string, Localization> list = GetLocalizations();

			string key = culture.ToLower() + "_" + text.ToLower();

			if (list.ContainsKey(key))
				return list[key];

			return null;
		}

		private static Localization AddText(string text)
		{
			lock (mLockObject)
			{
				text = text.Trim();

				Localization localization = Find(text);

				if (localization == null)
				{
					try
					{
						LocalizationDbContext db = HttpContext.RequestServices.GetService<LocalizationDbContext>();
						localization = new Localization()
						{
							//CultureCode = CultureHelper.GetCurrentCultureName(),
							KeyText = text,
							Translation = null
						};

						db.Localization.Add(localization);

						db.SaveChanges();

						mLocalizations.Add(localization.CultureCode + '_' + localization.KeyText.ToLower(), localization);
					}
					catch
					{
						RefreshCache();

						localization = Find(text);
					}
				}

				return localization;
			}
		}
	}
}
