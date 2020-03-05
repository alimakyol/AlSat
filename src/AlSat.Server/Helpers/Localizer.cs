using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using AlSat.Server.DAL;
using AlSat.Server.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace AlSat.Server.Helpers
{
	public class Localizer : IStringLocalizer
	{
		private static readonly ConcurrentDictionary<string, Localization> _localizations = new ConcurrentDictionary<string, Localization>();
		private static bool _doRefresh = true;
		private IServiceProvider _services;
		private static object _lockObject = new object();

		public Localizer(IServiceProvider services)
		{
			_services = services;

			FillLocalizations();
		}

		#region IStringLocalizer implementation
		public LocalizedString this[string name]
		{
			get
			{
				var localized = GetText(name);

				return new LocalizedString(name, localized.LocalizedText, localized.IsResourceNotFound);
			}
		}
		public LocalizedString this[string name, params object[] arguments] => this[name];

		public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
		{
			throw new NotImplementedException();
		}

		public IStringLocalizer WithCulture(CultureInfo culture)
		{
			throw new NotImplementedException();
		}
		#endregion IStringLocalizer implementation

		public void RefreshCache()
		{
			_doRefresh = true;

			FillLocalizations();
		}

		private void FillLocalizations()
		{
			lock (_lockObject)
			{
				if (_doRefresh)
				{
					MainDbContext dbContext = _services.GetService<MainDbContext>();

					List<Localization> list = dbContext.Localization
							.AsNoTracking()
							.OrderBy(m => m.CultureCode)
							.ThenBy(m => m.KeyText)
							.ToList()
							;

					_localizations.Clear();

					foreach (Localization item in list)
					{
						string key = GetKey(item.CultureCode, item.KeyText);

						_localizations.AddOrUpdate(key, item, (key, oldValue) => item);
					}

					_doRefresh = false;
				}
			}
		}

		private string GetKey(string culture, string keyText)
		{
			return culture.ToLower() + "." + keyText.ToLower();
		}

		private (string LocalizedText, bool IsResourceNotFound) GetText(string name)
		{
			if (string.IsNullOrEmpty(name))
				return (name, true);

			name = name.Trim();

			string text = name;
			bool isFound = true;

			Localization localization = Find(name);

			if (localization == null)
			{
				localization = AddText(name);
				isFound = false;
			}

			if (!string.IsNullOrEmpty(localization.Translation))
				text = localization.Translation;
			else
				isFound = false;

			return (text, isFound);
		}

		private Localization Find(string name)
		{
			string culture = CultureInfo.CurrentCulture.ToString();
			string key = GetKey(culture, name);

			if (_localizations.ContainsKey(key))
				return _localizations[key];

			return null;
		}

		private Localization AddText(string text)
		{
			lock (_lockObject)
			{
				text = text.Trim();

				Localization localization = Find(text);

				if (localization == null)
				{
					try
					{
						localization = new Localization()
						{
							CultureCode = CultureInfo.CurrentCulture.ToString(),
							KeyText = text,
							Translation = null
						};

						MainDbContext dbContext = _services.GetService<MainDbContext>();
						dbContext.Localization.Add(localization);

						dbContext.SaveChanges();

						_localizations.AddOrUpdate(GetKey(localization.CultureCode, localization.KeyText), localization, (key, oldValue) => localization);
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
