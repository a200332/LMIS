using System;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.DataDisplay
{
    public partial class CareersControl : BaseExtendedControl<CareersModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnSetModel(object model, Type type)
        {
            var careersModel = model as CareersModel;
            if (careersModel == null)
                return;

            rptItems.DataSource = careersModel.List;
            rptItems.DataBind();
        }

        protected Object GetTargetUrl(Object obj)
        {
            var model = obj as CareerModel;
            if (model == null)
                return "#";

            if (String.IsNullOrWhiteSpace(model.Url) && model.ParentID == null)
            {
                var url = String.Format("~/Pages/User/Careers.aspx?ID={0}", model.ID);
                return url;
            }

            return model.Url;
        }

        protected String GetImageUrl(object eval)
        {
            var url = String.Format("~/Handlers/GetImage.ashx?Type=Career&ID={0}", eval);
            return url;
        }

        protected Object GetTarget(Object obj)
        {
            var model = obj as CareerModel;
            if (model == null)
                return null;

            if (!String.IsNullOrWhiteSpace(model.Url))
            {
                return "_blank";
            }

            return String.Empty;
        }
    }
}