using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using CITI.EVO.CommonData.DAL.Context;

namespace CITI.EVO.CommonData.Web.Caches
{
    public static class LanguageCache
    {
        private const String cacheKey = "@{LanguageCache_List}";

        private static IList<CD_Language> langCache;
        public static IList<CD_Language> LangCache
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                var cache = HttpContext.Current.Cache;
                if (cache != null)
                {
                    if (langCache == null)
                    {
                        langCache = cache[cacheKey] as IList<CD_Language>;
                        if (langCache == null)
                        {
                            langCache = LoadLanguages();
                            cache.Insert(cacheKey, langCache, null, DateTime.Now.AddMinutes(15), System.Web.Caching.Cache.NoSlidingExpiration);
                        }
                    }
                }
                else
                {
                    langCache = (langCache ?? LoadLanguages());
                }

                return langCache;
            }
        }

        private static IList<CD_Language> LoadLanguages()
        {
            using (var db = new CommonDataDataContext())
            {
                var languagesList = db.CD_Languages.Where(n => n.DateDeleted == null).ToList();
                return languagesList;
            }
        }
    }
}