using System;
using System.Linq;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.BLL;
using Lmis.Portal.Web.Common;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Converters.ModelToEntity;
using Lmis.Portal.Web.Models;
using Lmis.Portal.Web.Utils;

namespace Lmis.Portal.Web.Pages.Management
{
    public partial class TablesList : BasePage
    {
        public bool CopyMode
        {
            get { return DataConverter.ToNullableBool(ViewState["CopyMode"]).GetValueOrDefault(); }
            set { ViewState["CopyMode"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UserUtil.GotoLoginIfNoSuperadmin();

            FillTablesTree();
        }

        protected void tablesControl_OnAddNewTable(object sender, EventArgs e)
        {
            var model = new TableModel();
            tableControl.Model = model;
            CopyMode = false;

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
            CopyMode = false;

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
            var entity = DataContext.LP_Columns.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var converter = new ColumnEntityModelConverter(DataContext);
            columnControl.Model = converter.Convert(entity);

            mpeAddEditColumn.Show();
        }

        protected void tablesControl_OnDeleteColumn(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Columns.FirstOrDefault(n => n.ID == e.Value);
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

            entity.Status = "Synchronized";

            DataContext.SubmitChanges();

            FillTablesTree();
        }

        protected void tablesControl_OnCopyTable(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Tables.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var converter = new TableEntityModelConverter(DataContext);
            var model = converter.Convert(entity);

            tableControl.Model = model;
            CopyMode = true;

            mpeAddEditTable.Show();
        }

        protected void btnAddTable_OnClick(object sender, EventArgs e)
        {
            var model = new TableModel();
            tableControl.Model = model;
            CopyMode = false;

            mpeAddEditTable.Show();
        }

        protected void btnSaveTable_OnClick(object sender, EventArgs e)
        {
            var model = tableControl.Model;

            var modelConverter = new TableModelEntityConverter(DataContext);
            var entityConverter = new TableEntityModelConverter(DataContext);
            var columnConverter = new ColumnModelEntityConverter(DataContext);

            var entity = DataContext.LP_Tables.FirstOrDefault(n => n.ID == model.ID);

            if (CopyMode)
            {
                if (entity == null)
                    return;
                
                var oldModel = entityConverter.Convert(entity);
                oldModel.Status = String.Empty;
                oldModel.Name = model.Name;

                var newEntity = modelConverter.Convert(oldModel);

                foreach (var columnModel in oldModel.Columns)
                {
                    var columnEntity = columnConverter.Convert(columnModel);
                    newEntity.Columns.Add(columnEntity);
                }

                DataContext.LP_Tables.InsertOnSubmit(newEntity);
            }
            else
            {
                if (entity == null)
                {
                    entity = modelConverter.Convert(model);
                    DataContext.LP_Tables.InsertOnSubmit(entity);
                }
                else
                {
                    modelConverter.FillObject(entity, model);
                }
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
                            orderby n.DateCreated descending
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