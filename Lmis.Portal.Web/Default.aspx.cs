using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Models;

public partial class _Default : BasePage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Request.RawUrl.Contains("Default.aspx"))
		{
			Response.Redirect("~/Default.aspx");
			return;
		}

		FillVideos();
	}

	private void FillVideos()
	{
		var entities = (from n in DataContext.LP_Videos
						where n.DateDeleted == null
						orderby n.DateCreated descending
						select n).ToList();

		var converter = new VideoEntityModelConverter(DataContext);

		var models = (from n in entities
					  let m = converter.Convert(n)
					  select m).ToList();

		var model = new VideosModel();
		model.List = models;

		videosControl.Model = model;
	}
}