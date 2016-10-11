using System;
using System.Linq;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Pages.User
{
	public partial class Links : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			FillSubLinks();
		}

		private void FillSubLinks()
		{
			var parentID = DataConverter.ToNullableGuid(Request["ID"]);
			if (parentID == null)
				return;

			var entities = (from n in DataContext.LP_Links
							where n.DateDeleted == null && n.ParentID == parentID
							orderby n.DateCreated
							select n).ToList();

			var converter = new LinkEntityModelConverter(DataContext);

			var models = (from n in entities
						  let m = converter.Convert(n)
						  select m).ToList();

			var model = new LinksModel();
			model.List = models;

			subLinksControl.Model = model;
		}
	}
}