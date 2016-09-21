using System;
using System.Collections.Generic;
using System.Linq;
using CITI.EVO.CommonData.DAL.Context;
using CITI.EVO.CommonData.Svc.Contracts;
using CITI.EVO.CommonData.Svc.Enums;
using CITI.EVO.CommonData.Web.Caches;
using CITI.EVO.CommonData.Web.Extensions;

namespace CITI.EVO.CommonData.Web.Services.Managers
{
	public static class CommonDataManager
	{
		public static List<AreaContract> GetAreas(AreaTypesEnum type, RecordTypesEnum recordType)
		{

			switch (type)
			{
				case AreaTypesEnum.Region:
					return GetAreasByCode((int)AreaTypesEnum.Region, recordType);

				case AreaTypesEnum.Municipality:
					return GetAreasByCode((int)AreaTypesEnum.Municipality, recordType);

				case AreaTypesEnum.MunicipalCenter:
					return GetAreasByCode((int)AreaTypesEnum.MunicipalCenter, recordType);

				case AreaTypesEnum.Town:
					return GetAreasByCode((int)AreaTypesEnum.Town, recordType);

				case AreaTypesEnum.Village:
					return GetAreasByCode((int)AreaTypesEnum.Village, recordType);

				case AreaTypesEnum.Country:
					return GetAreasByCode((int)AreaTypesEnum.Country, recordType);

			}

			return null;
		}

		public static List<AreaContract> GetAreasByCode(int code, RecordTypesEnum recordType)
		{
			using (var db = new CommonDataDataContext())
			{
				var areasQuery = from t in db.CD_Areas
								 let areaType = t.AreaType
								 where areaType.Code == code
								 select t;

				switch (recordType)
				{
					case RecordTypesEnum.Active:
						{
							areasQuery = from n in areasQuery
										 where n.DateDeleted == null
										 select n;
						}
						break;
					case RecordTypesEnum.Inactive:
						{
							areasQuery = from n in areasQuery
										 where n.DateDeleted != null
										 select n;
						}
						break;
				}

				var areas = areasQuery.ToList();

				return areas.ToContracts();
			}
		}

		public static AreaContract GetAreaByCode(String code)
		{
			using (var db = new CommonDataDataContext())
			{
				if (String.IsNullOrEmpty(code))
				{
					return null;
				}

				var area = (from n in db.CD_Areas
							where n.NewCode == code.Trim()
							select n).FirstOrDefault();

				if (area == null)
				{
					return null;
				}

				return area.ToContract();
			}
		}

		public static List<AreaContract> GetChildAreas(Guid ParentID)
		{
			using (var db = new CommonDataDataContext())
			{
				var childAreas = (from a in db.CD_Areas
								  where a.ParentID == ParentID
								  select a).ToList();

				return childAreas.ToContracts();

			}
		}

		public static AreaContract GetAreaByID(Guid Id)
		{
			using (var db = new CommonDataDataContext())
			{
				var area = (from a in db.CD_Areas
							where a.ID == Id
							select a).FirstOrDefault();

				return area.ToContract();

			}
		}

		public static List<MobileIndexesContract> GetAllMobileIndexes()
		{
			using (var db = new CommonDataDataContext())
			{
				var mobileIndexes = (from i in db.CD_MobileIndexes
									 where i.DateDeleted == null
									 select i).ToList();

				return mobileIndexes.ToContracts();

			}
		}

		public static MobileIndexesContract GetMobileIndexByID(Guid? ID)
		{
			if (ID == null || ID == Guid.Empty)
			{
				return null;
			}

			using (var db = new CommonDataDataContext())
			{
				var mobileIndex = (from i in db.CD_MobileIndexes
								   where i.ID == ID && i.DateDeleted == null
								   select i).FirstOrDefault();

				return mobileIndex.ToContract();
			}
		}

		public static List<LanguageContract> GetLanguages()
		{
			return LanguageCache.LangCache.ToContracts();
		}

		public static String GetTranslatedText(String moduleName, String languagePair, String trnKey, String defaultText)
		{
			if (String.IsNullOrWhiteSpace(moduleName))
			{
				throw new ArgumentNullException("moduleName");
			}

			if (String.IsNullOrWhiteSpace(trnKey))
			{
				throw new ArgumentNullException("trnKey");
			}

			if (String.IsNullOrWhiteSpace(languagePair))
			{
				throw new ArgumentNullException("languagePair");
			}

			var translatedText = TranslationCache.GetTranslatedText(moduleName, languagePair, trnKey, defaultText);
			return translatedText;

		}

		public static void SetTranslatedText(String moduleName, String languagePair, String trnKey, String translatedText)
		{
			if (String.IsNullOrWhiteSpace(moduleName))
			{
				throw new ArgumentNullException("moduleName");
			}

			if (String.IsNullOrWhiteSpace(trnKey))
			{
				throw new ArgumentNullException("trnKey");
			}

			if (String.IsNullOrWhiteSpace(languagePair))
			{
				throw new ArgumentNullException("languagePair");
			}

			TranslationCache.SetTranslatedText(moduleName, languagePair, trnKey, translatedText);
		}

		public static List<TranslationContract> GetTranslations(String moduleName, String languagePair, List<TranslationContract> list)
		{
			var @set = new HashSet<String>();
			var result = new List<TranslationContract>();

			foreach (var contract in list)
			{
				if (String.IsNullOrWhiteSpace(contract.TrnKey))
					continue;

				if (!@set.Add(contract.TrnKey))
					continue;

				contract.TranslatedText = TranslationCache.GetTranslatedText(moduleName, languagePair, contract.TrnKey, contract.DefaultText);
				result.Add(contract);
			}

			return result;
		}
	}
}