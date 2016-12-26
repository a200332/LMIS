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

        protected Object GetTargetUrl(Object obj)
        {
            var model = obj as SurveyModel;
            if (model == null)
                return "#";

            if (model.FileData == null && model.ParentID == null)
            {
                var url = String.Format("~/Pages/User/Surveys.aspx?ID={0}", model.ID);
                return url;
            }

            if (model.FileData != null)
            {
                var url = String.Format("~/Handlers/GetFile.ashx?Type=Survey&ID={0}", model.ID);
                return url;
            }

            return model.Url;
        }

        protected String GetImageUrl(object eval)
        {
            var url = String.Format("~/Handlers/GetImage.ashx?Type=Survey&ID={0}", eval);
            return url;
        }

        protected Object GetTarget(Object obj)
        {
            var model = obj as LegislationModel;
            if (model != null && model.FileData != null)
            {
                return "_blank";
            }

            return String.Empty;
        }
    }
}