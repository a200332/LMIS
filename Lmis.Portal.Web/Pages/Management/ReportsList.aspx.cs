using System;
using System.Linq;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Common;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Converters.ModelToEntity;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Pages.Management
{
	public partial class ReportsList : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			FillCategories();

			FillReportsGrid();
		}

		protected void btnAddReport_OnClick(object sender, EventArgs e)
		{
			var categoryID = DataConverter.ToNullableGuid(Request["CategoryID"]);
			if (categoryID == null)
				return;

			reportControl.Model = new ReportModel()
			{
				CategoryID = categoryID
			};

			mpeAddEditReport.Show();
		}

		protected void btnSaveReport_OnClick(object sender, EventArgs e)
		{
			var model = reportControl.Model;

			var converter = new ReportModelEntityConverter(DataContext);

			var entity = DataContext.LP_Reports.FirstOrDefault(n => n.ID == model.ID);
			if (entity == null)
			{
				entity = converter.Convert(model);
				DataContext.LP_Reports.InsertOnSubmit(entity);
			}
			else
			{
				converter.FillObject(entity, model);
			}

			DataContext.SubmitChanges();

			FillReportsGrid();
		}

		protected void reportsControl_OnViewReport(object sender, GenericEventArgs<Guid> e)
		{
			var entity = DataContext.LP_Reports.FirstOrDefault(n => n.ID == e.Value);
			if (entity == null)
				return;

			var converter = new ReportEntityModelConverter(DataContext);
			var model = converter.Convert(entity);

			reportControl.Model = model;

			mpeAddEditReport.Show();
		}

		protected void reportsControl_OnEditReport(object sender, GenericEventArgs<Guid> e)
		{
			var entity = DataContext.LP_Reports.FirstOrDefault(n => n.ID == e.Value);
			if (entity == null)
				return;

			var converter = new ReportEntityModelConverter(DataContext);
			var model = converter.Convert(entity);

			reportControl.Model = model;

			mpeAddEditReport.Show();
		}

		protected void reportsControl_OnDeleteReport(object sender, GenericEventArgs<Guid> e)
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
			var categoryID = DataConverter.ToNullableGuid(Request["CategoryID"]);
			if (categoryID == null)
				return;

			var entities = (from n in DataContext.LP_Reports
							where n.DateDeleted == null && n.CategoryID == categoryID
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