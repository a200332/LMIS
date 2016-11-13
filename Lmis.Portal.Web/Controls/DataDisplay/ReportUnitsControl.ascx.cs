using System;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.DataDisplay
{
	public partial class ReportUnitsControl : BaseExtendedControl<ReportUnitsModel>
	{
	    public Unit ChartWidth
	    {
            get { return DataConverter.ToNullableInt(ViewState["ChartWidth"]).GetValueOrDefault(800); }
            set { ViewState["ChartWidth"] = value; }
        }

        public Unit ChartHeight
        {
            get { return DataConverter.ToNullableInt(ViewState["ChartHeight"]).GetValueOrDefault(500); }
            set { ViewState["ChartHeight"] = value; }
        }

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