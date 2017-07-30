using System;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.DataDisplay
{
    public partial class UserReportsControl : BaseExtendedControl<UserReportsModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnSetModel(object model, Type type)
        {
            var userReportsModel = model as UserReportsModel;
            if (userReportsModel == null)
                return;

            rptItems.DataSource = userReportsModel.List;
            rptItems.DataBind();
        }

        protected Object GetTargetUrl(Object obj)
        {
            var model = obj as UserReportModel;
            if (model == null)
                return "#";

            if (model.FileData == null && model.ParentID == null)
            {
                var url = String.Format("~/Pages/User/UserReports.aspx?ID={0}", model.ID);
                return url;
            }
            else
            {
                var url = String.Format("~/Handlers/GetFile.ashx?Type=UserReport&ID={0}", model.ID);
                return url;
            }
        }

        protected String GetImageUrl(object eval)
        {
            var url = String.Format("~/Handlers/GetImage.ashx?Type=UserReport&ID={0}", eval);
            return url;
        }

        protected Object GetTarget(Object obj)
        {
            var model = obj as ProjectModel;
            if (model != null && model.FileData != null)
            {
                return "_blank";
            }

            return String.Empty;
        }
    }
}
