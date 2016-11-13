using System;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.DataDisplay
{
    public partial class ProjectsControl : BaseExtendedControl<ProjectsModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnSetModel(object model, Type type)
        {
            var projectsModel = model as ProjectsModel;
            if (projectsModel == null)
                return;

            rptItems.DataSource = projectsModel.List;
            rptItems.DataBind();
        }

        protected Object GetTargetUrl(Object obj)
        {
            var model = obj as LegislationModel;
            if (model == null)
                return "#";

            if (model.FileData == null && model.ParentID == null)
            {
                var url = String.Format("~/Pages/User/Projects.aspx?ID={0}", model.ID);
                return url;
            }
            else
            {
                var url = String.Format("~/Handlers/GetFile.ashx?Type=Project&ID={0}", model.ID);
                return url;
            }
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