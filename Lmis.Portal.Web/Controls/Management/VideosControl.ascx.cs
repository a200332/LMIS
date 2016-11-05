using System;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.Management
{
	public partial class VideosControl : BaseExtendedControl<VideosModel>
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override void OnSetModel(object model, Type type)
		{
			var videosModel = model as VideosModel;
			if (videosModel == null)
				return;

			gvData.DataSource = videosModel.List;
			gvData.DataBind();
		}
	}
}