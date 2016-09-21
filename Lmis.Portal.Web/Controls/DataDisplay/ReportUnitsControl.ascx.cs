using System;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.DataDisplay
{
	public partial class ReportUnitsControl : BaseExtendedControl<ReportUnitsModel>
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected override void OnSetModel(object model, Type type)
		{
			var unitsModel = model as ReportUnitsModel;
			if (unitsModel == null)
				return;

			rpReports.DataSource = unitsModel.List;
			rpReports.DataBind();
		}

		protected ReportUnitModel GetReportUnitModel(Object dataItem)
		{
			return dataItem as ReportUnitModel;
		}
	}
}