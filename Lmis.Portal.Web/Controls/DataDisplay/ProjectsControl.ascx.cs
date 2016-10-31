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

        protected Object GetFileUrl(Object eval)
        {
            var id = DataConverter.ToNullableGuid(eval);
            if (id == null)
                return "#";

            var url = String.Format("~/Handlers/GetFile.ashx?Type=Project&ID={0}", id);
            return url;
        }
    }
}