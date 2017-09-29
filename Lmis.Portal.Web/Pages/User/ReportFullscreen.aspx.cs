using System;
using System.Linq;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Converters.EntityToModel;

namespace Lmis.Portal.Web.Pages.User
{
    public partial class ReportFullscreen : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var reportID = DataConverter.ToNullableGuid(RequestUrl["ReportID"]);
            if (reportID == null)
                return;

            var report = DataContext.LP_Reports.FirstOrDefault(n => n.ID == reportID);
            if (report == null)
                return;

            var converter = new ReportEntityUnitModelConverter(DataContext);
            var model = converter.Convert(report);

            reportUnitControl.EnableFullscreen = false;

            reportUnitControl.Model = model;
            reportUnitControl.DataBind();
        }
    }
}