using System;
using System.Linq;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Extensions;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Pages.User
{
	public partial class Dashboard : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			FillCategories();

			FillReports();
		}

		private void FillReports()
		{
			var categoryID = DataConverter.ToNullableGuid(Request["CategoryID"]);
			if (categoryID == null)
				return;

			var reports = DataContext.LP_Reports.Where(n => n.CategoryID == categoryID && n.DateDeleted == null).ToList();

			var selReports = (from ListItem n in lstReports.Items
							  let m = DataConverter.ToNullableGuid(n.Value)
							  where m != null && n.Selected
							  select m).ToHashSet();

			lstReports.DataSource = reports;
			lstReports.DataBind();

			foreach (ListItem listItem in lstReports.Items)
				listItem.Selected = selReports.Contains(DataConverter.ToNullableGuid(listItem.Value));

			var converter = new ReportEntityUnitModelConverter(DataContext);

			var reportModels = (from n in reports
								where selReports.Count == 0 || selReports.Contains(n.ID)
								select converter.Convert(n));

			var reportUnits = new ReportUnitsModel
			{
				List = reportModels.ToList()
			};

			reportUnitsControl.Model = reportUnits;
		}

		protected void FillCategories()
		{
			var converter = new CategoryEntityModelConverter(DataContext);

			var entities = DataContext.LP_Categories.Where(n => n.DateDeleted == null).ToList();

			var models = entities.Select(n => converter.Convert(n)).ToList();

			var categoriesModel = new CategoriesModel { List = models };
			categoriesControl.Model = categoriesModel;
		}
	}
}