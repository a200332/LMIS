using System;
using System.Linq;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.BLL;
using Lmis.Portal.Web.Common;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Converters.ModelToEntity;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Pages.Management
{
	public partial class TablesList : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			FillTablesTree();
		}

		protected void tablesControl_OnAddNewTable(object sender, EventArgs e)
		{
			var model = new TableModel();
			tableControl.Model = model;

			mpeAddEditTable.Show();
		}

		protected void tablesControl_OnEditItem(object sender, GenericEventArgs<Guid> e)
		{
			var entity = DataContext.LP_Tables.FirstOrDefault(n => n.ID == e.Value);
			if (entity == null)
				return;

			var converter = new TableEntityModelConverter(DataContext);
			var model = converter.Convert(entity);

			tableControl.Model = model;

			mpeAddEditTable.Show();
		}

		protected void tablesControl_OnDeleteItem(object sender, GenericEventArgs<Guid> e)
		{
			var entity = DataContext.LP_Tables.FirstOrDefault(n => n.ID == e.Value);
			if (entity == null)
				return;

			entity.DateDeleted = DateTime.Now;

			DataContext.SubmitChanges();

			FillTablesTree();
		}

		protected void tablesControl_OnAddNewColumn(object sender, GenericEventArgs<Guid> e)
		{
			var entity = DataContext.LP_Tables.FirstOrDefault(n => n.ID == e.Value);
			if (entity == null)
				return;

			var model = new ColumnModel
			{
				TableID = e.Value
			};

			columnControl.Model = model;

			mpeAddEditColumn.Show();
		}

		protected void tablesControl_OnEditColumn(object sender, GenericEventArgs<Guid> e)
		{
			var model = tableControl.Model;

			var converter = new TableModelEntityConverter(DataContext);

			var entity = DataContext.LP_Tables.FirstOrDefault(n => n.ID == model.ID);
			if (entity == null)
			{
				entity = converter.Convert(model);
				DataContext.LP_Tables.InsertOnSubmit(entity);
			}
			else
			{
				converter.FillObject(entity, model);
			}

			DataContext.SubmitChanges();
		}

		protected void tablesControl_OnDeleteColumn(object sender, GenericEventArgs<Guid> e)
		{
			var entity = DataContext.LP_Tables.FirstOrDefault(n => n.ID == e.Value);
			if (entity == null)
				return;

			entity.DateDeleted = DateTime.Now;

			DataContext.SubmitChanges();

			FillTablesTree();
		}

		protected void tablesControl_OnSyncTable(object sender, GenericEventArgs<Guid> e)
		{
			var entity = DataContext.LP_Tables.FirstOrDefault(n => n.ID == e.Value);
			if (entity == null)
				return;

			var converter = new TableEntityModelConverter(DataContext);
			var model = converter.Convert(entity);


			var synchronizer = new SchemaSynchronizer(model);
			synchronizer.Update();
		}

		protected void btnAddTable_OnClick(object sender, EventArgs e)
		{
			var model = new TableModel();
			tableControl.Model = model;

			mpeAddEditTable.Show();
		}

		protected void btnSaveTable_OnClick(object sender, EventArgs e)
		{
			var model = tableControl.Model;

			var converter = new TableModelEntityConverter(DataContext);

			var entity = DataContext.LP_Tables.FirstOrDefault(n => n.ID == model.ID);
			if (entity == null)
			{
				entity = converter.Convert(model);
				DataContext.LP_Tables.InsertOnSubmit(entity);
			}
			else
			{
				converter.FillObject(entity, model);
			}

			DataContext.SubmitChanges();

			FillTablesTree();
		}

		protected void btnSaveColumn_OnClick(object sender, EventArgs e)
		{
			var model = columnControl.Model;

			var converter = new ColumnModelEntityConverter(DataContext);

			var entity = DataContext.LP_Columns.FirstOrDefault(n => n.ID == model.ID);
			if (entity == null)
			{
				entity = converter.Convert(model);
				DataContext.LP_Columns.InsertOnSubmit(entity);
			}
			else
			{
				converter.FillObject(entity, model);
			}

			DataContext.SubmitChanges();

			FillTablesTree();
		}

		protected void FillTablesTree()
		{
			var entities = (from n in DataContext.LP_Tables
							where n.DateDeleted == null
							select n).ToList();

			var converter = new TableEntityModelConverter(DataContext);

			var models = (from n in entities
						  let m = converter.Convert(n)
						  select m);

			var model = new TablesModel();
			model.List = models.ToList();

			tablesControl.Model = model;
		}
	}
}