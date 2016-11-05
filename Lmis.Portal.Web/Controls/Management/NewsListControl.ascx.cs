using System;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.Management
{
	public partial class NewsListControl : BaseExtendedControl<NewsListModel>
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override void OnSetModel(object model, Type type)
		{
			var linksModel = model as NewsListModel;
			if (linksModel == null)
				return;

			gvData.DataSource = linksModel.List;
			gvData.DataBind();
		}
	}
}