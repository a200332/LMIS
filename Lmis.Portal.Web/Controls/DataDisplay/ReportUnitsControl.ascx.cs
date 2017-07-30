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
            get
            {
                var unit = Unit.Parse(Convert.ToString(ViewState["ChartWidth"]));
                return (unit.IsEmpty ? new Unit(800) : unit);
            }
            set { ViewState["ChartWidth"] = value; }
        }

        public Unit ChartHeight
        {
            get
            {
                var unit = Unit.Parse(Convert.ToString(ViewState["ChartHeight"]));
                return (unit.IsEmpty ? new Unit(500) : unit);
            }
            set { ViewState["ChartHeight"] = value; }
        }

        public String ChartCssClass
        {
            get { return Convert.ToString(ViewState["ChartCssClass"]); }
            set { ViewState["ChartCssClass"] = value; }
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