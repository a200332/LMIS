using System;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.DataDisplay
{
    public partial class SurveysControl : BaseExtendedControl<SurveysModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnSetModel(object model, Type type)
        {
            var surveysModel = model as SurveysModel;
            if (surveysModel == null)
                return;

            rptItems.DataSource = surveysModel.List;
            rptItems.DataBind();
        }

        protected Object GetFileUrl(Object eval)
        {
            var id = DataConverter.ToNullableGuid(eval);
            if (id == null)
                return "#";

            var url = String.Format("~/Handlers/GetFile.ashx?Type=Survey&ID={0}", id);
            return url;
        }
    }
}