using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Extensions;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.DAL.DAL;
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

		protected void btnReportsOK_OnClick(object sender, EventArgs e)
		{
		}

		private void FillReports()
		{
			var categoryID = DataConverter.ToNullableGuid(Request["CategoryID"]);
			if (categoryID == null)
				return;

			var reports = (from n in DataContext.LP_Reports
						   where n.CategoryID == categoryID &&
								 n.DateDeleted == null &&
								 n.Public == true
						   select n).ToList();

			//if (reports.Count > 0)
			//	btnReports.Style["display"] = "";
			//else
			//	btnReports.Style["display"] = "none";

			//FillReportsList(reports);

			//var selReports = GetSelectedReports().ToHashSet();

			var converter = new ReportEntityUnitModelConverter(DataContext);

			//var reportModels = (from n in reports
			//					where selReports.Count == 0 || selReports.Contains(n.ID)
			//					select converter.Convert(n));

			var reportModels = (from n in reports
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

		//protected void FillReportsList(IEnumerable<LP_Report> reports)
		//{
		//	var selReports = GetSelectedReports().ToHashSet();

		//	lstReports.DataSource = reports;
		//	lstReports.DataBind();

		//	foreach (ListItem listItem in lstReports.Items)
		//		listItem.Selected = selReports.Contains(DataConverter.ToNullableGuid(listItem.Value));
		//}

		//protected IEnumerable<Guid?> GetSelectedReports()
		//{
		//	var selReports = from ListItem n in lstReports.Items
		//					 let m = DataConverter.ToNullableGuid(n.Value)
		//					 where m != null && n.Selected
		//					 select m;

		//	return selReports;
		//}
	}
}