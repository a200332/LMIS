using System;
using System.Collections.Generic;
using System.Linq;
using CITI.EVO.Tools.Extensions;
using CITI.EVO.Tools.Helpers;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Models;
using Lmis.Portal.Web.Utils;

namespace Lmis.Portal.Web.Pages.User
{
    public partial class Dashboard : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var urlHelper = new UrlHelper("~/Pages/User/DashboardEx.aspx");
            foreach (var pair in RequestUrl)
                urlHelper[pair.Key] = pair.Value;

            btnFullscreen.NavigateUrl = urlHelper.ToEncodedUrl();

            var url = new UrlHelper("~/Pages/User/ReportsConfig.aspx");
            url["TargetUrl"] = LmisCommonUtil.ConvertToBase64("~/Pages/User/Dashboard.aspx");

            btnConfiguration.NavigateUrl = url.ToEncodedUrl();

            FillCategories();

            FillReports();
        }

        protected void btnReportsOK_OnClick(object sender, EventArgs e)
        {
        }


        private void FillReports()
        {
            var currentLanguage = LanguageUtil.GetLanguage();

            var categoryID = DataConverter.ToNullableGuid(RequestUrl["CategoryID"]);
            if (categoryID == null)
            {
                var categoryCode = Convert.ToString(RequestUrl["CategoryCode"]);
                if (String.IsNullOrWhiteSpace(categoryCode))
                    return;

                var langSpecCategory = GetLanguageSpecCategory(categoryCode);
                if (langSpecCategory == null)
                    return;

                var url = String.Format("~/Pages/User/Dashboard.aspx?CategoryID={0}", langSpecCategory.ID);
                Response.Redirect(url);

                return;
            }

            var category = DataContext.LP_Categories.FirstOrDefault(n => n.ID == categoryID);
            if (category == null)
                return;


            if (!String.IsNullOrWhiteSpace(category.Language) && category.Language != currentLanguage)
            {
                var langSpecCategory = GetLanguageSpecCategory(category.Number);
                if (langSpecCategory == null)
                    return;

                var url = String.Format("~/Pages/User/Dashboard.aspx?CategoryID={0}", langSpecCategory.ID);
                Response.Redirect(url);

                return;
            }

            var reportsQuery = from n in category.Reports
                               where n.Public == true &&
                                     n.DateDeleted == null &&
                                     n.CategoryID == categoryID
                               select n;

            reportsQuery = from n in reportsQuery
                           where n.Language == currentLanguage || n.Language == null || n.Language == ""
                           select n;

            var reportsList = reportsQuery.ToList();

            var converter = new ReportEntityUnitModelConverter(DataContext);
            var reportModels = reportsList.Select(n => converter.Convert(n));

            var reportUnits = new ReportUnitsModel
            {
                List = reportModels.ToList()
            };

            reportUnitsControl.Model = reportUnits;
        }

        protected void FillCategories()
        {
            var currentLanguage = LanguageUtil.GetLanguage();

            var converter = new CategoryEntityModelConverter(DataContext);

            var allEntitiesLp = (from n in DataContext.LP_Categories
                                 where n.DateDeleted == null && (n.Language == currentLanguage || n.Language == null || n.Language == "")
                                 select n).ToLookup(n => n.ParentID);

            var entitiesList = CategoryUtil.GetAllCategories(null, allEntitiesLp).ToList();

            CategoryUtil.Sort(entitiesList);

            var models = entitiesList.Select(n => converter.Convert(n)).ToList();

            var categoriesModel = new CategoriesModel { List = models };
            categoriesControl.Model = categoriesModel;
        }

        protected LP_Category GetLanguageSpecCategory(String code)
        {
            return GetLanguageSpecCategory(code, LanguageUtil.GetLanguage());
        }
        protected LP_Category GetLanguageSpecCategory(String code, String lang)
        {
            if (String.IsNullOrWhiteSpace(code))
                return null;

            var allCategories = DataContext.LP_Categories.ToList();

            var categoriesDict = allCategories.ToDictionary(n => n.ID);

            var category = (from n in allCategories
                            where !IsDeleted(n, categoriesDict) &&
                                  n.Number == code &&
                                  (n.Language == lang || n.Language == null || n.Language == "")
                            select n).FirstOrDefault();

            return category;
        }

        protected bool IsDeleted(LP_Category category, IDictionary<Guid, LP_Category> categories)
        {
            while (category != null)
            {
                if (category.DateDeleted != null)
                    return true;

                category = categories.GetValueOrDefault(category.ParentID.GetValueOrDefault());
            }

            return false;
        }
    }
}