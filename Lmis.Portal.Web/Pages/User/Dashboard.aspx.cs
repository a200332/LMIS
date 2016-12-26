using System;
using System.Linq;
using CITI.EVO.Tools.Utils;
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
            FillCategories();

            FillReports();
        }

        protected void btnReportsOK_OnClick(object sender, EventArgs e)
        {
        }

        private void FillReports()
        {
            var categoryID = DataConverter.ToNullableGuid(Request["CategoryID"]);
            if (categoryID == null)
                return;

            var currentLanguage = LanguageUtil.GetLanguage();

            var reports = (from n in DataContext.LP_Reports
                           where n.Public == true &&
                                 n.DateDeleted == null &&
                                 n.CategoryID == categoryID &&
                                 (n.Language == currentLanguage || n.Language == null || n.Language == "")
                           select n).ToList();

            var converter = new ReportEntityUnitModelConverter(DataContext);
            var reportModels = reports.Select(n => converter.Convert(n));

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
    }
}