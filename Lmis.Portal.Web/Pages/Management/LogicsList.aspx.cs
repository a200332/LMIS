using System;
using System.Linq;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Common;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Models;
using Lmis.Portal.Web.Utils;

namespace Lmis.Portal.Web.Pages.Management
{
	public partial class LogicsList : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			UserUtil.GotoLoginIfNoSuperadmin();

			FillLogicsGrid();
		}

		protected void btnAddLogic_OnClick(object sender, EventArgs e)
		{
			Response.Redirect("~/Pages/Management/AddEditLogic.aspx");
		}

		private void FillLogicsGrid()
		{
			var entities = (from n in DataContext.LP_Logics
							where n.DateDeleted == null
							orderby n.DateCreated descending
							select n).ToList();

			var converter = new LogicEntityModelConverter(DataContext);

			var models = (from n in entities
						  let m = converter.Convert(n)
						  select m).ToList();

			var model = new LogicsModel();
			model.List = models;

			logicsControl.Model = model;
		}

		protected void logicsControl_OnEditItem(object sender, GenericEventArgs<Guid> e)
		{
			var url = String.Format("~/Pages/Management/AddEditLogic.aspx?LogicID={0}", e.Value);
			Response.Redirect(url);
		}

		protected void logicsControl_OnDeleteItem(object sender, GenericEventArgs<Guid> e)
		{
			var entity = DataContext.LP_Logics.FirstOrDefault(n => n.ID == e.Value);
			if (entity != null)
				entity.DateDeleted = DateTime.Now;

			DataContext.SubmitChanges();

			FillLogicsGrid();
		}

		protected void logicsControl_OnViewItem(object sender, GenericEventArgs<Guid> e)
		{
			var url = String.Format("~/Pages/Management/AddEditLogic.aspx?LogicID={0}", e.Value);
			Response.Redirect(url);
		}
	}
}