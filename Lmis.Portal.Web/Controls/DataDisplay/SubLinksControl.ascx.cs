using System;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.DataDisplay
{
	public partial class SubLinksControl : BaseExtendedControl<LinksModel>
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override void OnSetModel(object model, Type type)
		{
			var videosModel = model as LinksModel;
			if (videosModel == null)
				return;

			rptItems.DataSource = videosModel.List;
			rptItems.DataBind();
		}

        protected String GetImageUrl(object eval)
        {
            var url = String.Format("~/Handlers/GetImage.ashx?Type=Link&ID={0}", eval);
            return url;
        }
    }
}