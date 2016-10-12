using System;
using System.Linq;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Common;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Converters.ModelToEntity;
using Lmis.Portal.Web.Models;
using Lmis.Portal.Web.Utils;

namespace Lmis.Portal.Web.Pages.Management
{
	public partial class ReportsList : BasePage
	{
		public Guid? CategoryID
		{
			get { return DataConverter.ToNullableGuid(Request["CategoryID"]); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			UserUtil.GotoLoginIfNoSuperadmin();

			FillCategories();

			FillReportsGrid();
		}

		protected void btnAddReport_OnClick(object sender, EventArgs e)
		{
			if (CategoryID == null)
				return;

			var url = String.Format("~/Pages/Management/AddEditReport.aspx?Mode=Add&CategoryID={0}", CategoryID);
			Response.Redirect(url);
		}

		protected void reportsControl_OnViewItem(object sender, GenericEventArgs<Guid> e)
		{
			var url = String.Format("~/Pages/Management/AddEditReport.aspx?Mode=View&ReportID={0}&CategoryID={1}", e.Value, CategoryID);
			Response.Redirect(url);
		}

		protected void reportsControl_OnEditItem(object sender, GenericEventArgs<Guid> e)
		{
			var url = String.Format("~/Pages/Management/AddEditReport.aspx?Mode=Edit&ReportID={0}&CategoryID={1}", e.Value, CategoryID);
			Response.Redirect(url);
		}

		protected void reportsControl_OnDeleteItem(object sender, GenericEventArgs<Guid> e)
		{
			var entity = DataContext.LP_Reports.FirstOrDefault(n => n.ID == e.Value);
			if (entity == null)
				return;

			entity.DateDeleted = DateTime.Now;

			DataContext.SubmitChanges();
		}

		protected void FillCategories()
		{
			var converter = new CategoryEntityModelConverter(DataContext);

			var entities = DataContext.LP_Categories.Where(n => n.DateDeleted == null).ToList();

			var models = entities.Select(n => converter.Convert(n)).ToList();

			var categoriesModel = new CategoriesModel { List = models };
			categoriesControl.Model = categoriesModel;
		}

		protected void FillReportsGrid()
		{
			var entities = (from n in DataContext.LP_Reports
							where n.DateDeleted == null && n.CategoryID == CategoryID
							orderby n.DateCreated descending
							select n).ToList();

			var converter = new ReportEntityModelConverter(DataContext);

			var models = (from n in entities
						  let m = converter.Convert(n)
						  select m).ToList();

			var model = new ReportsModel { List = models };

			reportsControl.Model = model;
		}
	}
}