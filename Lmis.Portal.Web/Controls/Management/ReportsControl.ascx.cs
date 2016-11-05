using System;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.Management
{
	public partial class ReportsControl : BaseExtendedControl<ReportsModel>
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override void OnSetModel(object model, Type type)
		{
			var reportsModel = (ReportsModel)model;
			gvData.DataSource = reportsModel.List;
			gvData.DataBind();
		}
	}
}
