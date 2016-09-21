using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using CITI.EVO.CommonData.Svc.Contracts;
using CITI.EVO.Proxies;
using CITI.EVO.Tools.Cache;
using CITI.EVO.Tools.Security;
using CITI.EVO.Tools.Web.UI.Common;

namespace CITI.EVO.Tools.Utils
{
	public class TranslationUtil
	{

		private const String trnKeyKey = "TrnKey";
		private const String defaultTextKey = "DefaultText";
		private const String moduleNameKey = "ModuleName";
		private const String trnEditPageKey = "TrnEditPage";

		private const String trnCacheKey = "$[TranslationUtil_Translations]";

		private const String requestTranslationModeKey = "translationMode";
		private const String sessionTranslationModeKey = "TranslationUtil_translationMode";

		private const String contextEnableTranslationKey = "TranslationUtil_enableTranslation";

		private const char delimiter = (char)1;
		private const String editLinkFormat = "<a target=\"_blank\" href=\"{0}?moduleName={1}&trnKey={2}&languagePair={3}\">[T]</a>";

		public static bool TranslationMode
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			get
			{
				var context = HttpContext.Current;
				if (context != null)
				{
					return (context.Session[sessionTranslationModeKey] != null || context.Request[requestTranslationModeKey] != null);
				}

				return false;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				var context = HttpContext.Current;
				if (context != null)
				{
					if (value)
					{
						context.Session[sessionTranslationModeKey] = "ON";
					}
					else
					{
						context.Session[sessionTranslationModeKey] = null;
					}
				}
			}
		}

		public static bool EnableTranslation
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			get
			{
				var context = HttpContext.Current;

				var value = DataConverter.ToNullableBool(context.Items[contextEnableTranslationKey]);
				if (value == null)
				{
					value = DataConverter.ToNullableBool(ConfigurationManager.AppSettings["EnableTranslations"]);
					value = value.GetValueOrDefault(true);

					context.Items[contextEnableTranslationKey] = value;
				}

				return value.GetValueOrDefault();
			}
		}


		public static void ResetCache()
		{
			var context = HttpContext.Current;
			if (context != null)
			{
				var cache = context.Cache;
				cache.Remove(trnCacheKey);
			}
		}

		public static String GetTranslatedText(String defaultText)
		{
			var trnKey = CryptographyUtil.ComputeMD5(defaultText);
			return GetTranslatedText(trnKey, defaultText);
		}

		public static String GetTranslatedText(String trnKey, String defaultText)
		{
			var languagePair = Thread.CurrentThread.CurrentCulture.Name;
			return GetTranslatedText(trnKey, defaultText, languagePair);
		}

		public static String GetTranslatedText(String trnKey, String defaultText, String languagePair)
		{
			if (!EnableTranslation)
			{
				return defaultText;
			}

			if (!String.IsNullOrWhiteSpace(trnKey))
			{
				var moduleName = PermissionUtil.ModuleName;
				var trnEditPage = ConfigurationManager.AppSettings[trnEditPageKey];

				String trnText;
				if (!TranslationMode)
				{
					var cacheKey = String.Format("{0}|{1}|{2}", moduleName, trnKey, languagePair);

					trnText = GetCacheTranslation(cacheKey);

					if (String.IsNullOrWhiteSpace(trnText))
					{
						trnText = CommonProxy.GetTranslatedText(moduleName, languagePair, trnKey, defaultText);
						if (!String.IsNullOrWhiteSpace(trnText))
						{
							SetCacheTranslation(cacheKey, trnText);
						}
					}
				}
				else
				{
					trnText = CommonProxy.GetTranslatedText(moduleName, languagePair, trnKey, defaultText);

					var editLink = String.Format(editLinkFormat, trnEditPage, moduleName, trnKey, languagePair);
					trnText = String.Concat(trnText, delimiter, editLink);
				}

				defaultText = trnText;
			}

			return defaultText;
		}

		private static void SetCacheTranslation(String cacheKey, String translateText)
		{
			var cache = CommonObjectCache.InitObjectCache(trnCacheKey, () => new Dictionary<String, String>());
			SetCacheTranslation(cache, cacheKey, translateText);
		}
		private static void SetCacheTranslation(IDictionary<String, String> cache, String cacheKey, String translateText)
		{
			if (cache != null)
			{
				lock (cache)
				{
					cache[cacheKey] = translateText;
				}
			}
		}

		private static String GetCacheTranslation(String cacheKey)
		{
			var cache = CommonObjectCache.InitObjectCache(trnCacheKey, () => new Dictionary<String, String>());
			return GetCacheTranslation(cache, cacheKey);
		}
		private static String GetCacheTranslation(IDictionary<String, String> cache, String cacheKey)
		{
			lock (cache)
			{
				String tranlatedText;
				if (!cache.TryGetValue(cacheKey, out tranlatedText))
				{
					return null;
				}

				return tranlatedText;
			}
		}

		private static bool ContainsCacheTranslation(String cacheKey)
		{
			var cache = CommonObjectCache.InitObjectCache(trnCacheKey, () => new Dictionary<String, String>());
			return ContainsCacheTranslation(cache, cacheKey);
		}
		private static bool ContainsCacheTranslation(IDictionary<String, String> cache, String cacheKey)
		{
			lock (cache)
			{
				return cache.ContainsKey(cacheKey);
			}
		}

		public static void PreloadTranslations(Control control)
		{
			PreloadTranslations(control, false);
		}
		public static void PreloadTranslations(Control control, bool applyTranslations)
		{
			var moduleName = PermissionUtil.ModuleName;
			var languagePair = Thread.CurrentThread.CurrentCulture.Name;


			var cache = CommonObjectCache.InitObjectCache(trnCacheKey, () => new Dictionary<String, String>());
			var controls = UserInterfaceUtil.TraverseControls(control);

			var query = from ctl in controls
						let trn = ctl as ITranslatable
						where trn != null &&
							  !String.IsNullOrWhiteSpace(trn.Text)
						let defText = GetClearText(trn.Text)
						let trnKey = (String.IsNullOrWhiteSpace(trn.TrnKey) ? CryptographyUtil.ComputeMD5(defText) : trn.TrnKey)
						let cacheKey = String.Format("{0}|{1}|{2}", moduleName, trnKey, languagePair)
						select new
						{
							TrnKey = trnKey,
							CacheKey = cacheKey,
							DefaultText = defText,
							Translatable = trn,
						};

			var list = query.ToList();
			var lookup = list.ToLookup(n => n.CacheKey);

			var notInCacheControls = (from n in lookup
									  where !ContainsCacheTranslation(cache, n.Key)
									  select n).ToList();

			var notInCacheContracts = (from n in notInCacheControls
									   let p = n.FirstOrDefault()
									   let m = new TranslationContract { TrnKey = p.TrnKey, DefaultText = p.DefaultText }
									   select m).ToList();

			var contracts = CommonProxy.GetTranslations(moduleName, languagePair, notInCacheContracts);

			foreach (var contract in contracts)
			{
				var cacheKey = String.Format("{0}|{1}|{2}", moduleName, contract.TrnKey, languagePair);
				SetCacheTranslation(cache, cacheKey, contract.TranslatedText);

				if (applyTranslations)
				{
					foreach (var item in lookup[cacheKey])
					{
						var translatable = item.Translatable;

						translatable.TrnKey = item.TrnKey;
						translatable.Text = contract.TranslatedText;
					}
				}
			}
		}

		public static void ApplyTranslation(ITranslatable translatable)
		{
			var sw = HttpContext.Current.Items["ApplyTranslation_SW"] as Stopwatch;
			if (sw == null)
			{
				sw = new Stopwatch();
			}

			sw.Start();

			if (String.IsNullOrEmpty(translatable.Text))
			{
				return;
			}

			var defaultText = GetClearText(translatable.Text);
			if (String.IsNullOrEmpty(translatable.TrnKey))
			{
				translatable.TrnKey = CryptographyUtil.ComputeMD5(defaultText);
			}

			translatable.Text = GetTranslatedText(translatable.TrnKey, defaultText);
			sw.Stop();
		}

		private static String GetClearText(String text)
		{
			//var patternFormat = String.Concat(delimiter, "<a target=\"_blank\" href=\"{0}?moduleName={1}&trnKey=");

			//var moduleName = ConfigurationManager.AppSettings[moduleNameKey];
			//var trnEditPage = ConfigurationManager.AppSettings[trnEditPageKey];

			//var pattern = String.Format(patternFormat, trnEditPage, moduleName);

			var patternIndex = text.IndexOf(delimiter);
			if (patternIndex > -1)
			{
				var clearText = text.Substring(0, patternIndex);
				return clearText;
			}

			return text;
		}

	}

}
