using System;
using System.Linq;
using System.Web.UI.WebControls;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.SchemaManipulation
{
	public partial class ReportLogicsControl : BaseExtendedControl<ReportLogicsModel>
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override void OnSetModel(object model, Type type)
		{
			var reportLogicsModel = model as ReportLogicsModel;
			if (reportLogicsModel == null)
				return;

			if (reportLogicsModel.List == null)
				return;

			var query = (from n in reportLogicsModel.List
						 select new
						 {
							 ID = n.ID,
							 Type = n.Type,
							 Logic = n.Logic.Name
						 });

			gvData.DataSource = query.ToList();
			gvData.DataBind();
		}
	}
}