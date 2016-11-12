using System;
using System.Linq;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Common;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Converters.ModelToEntity;
using Lmis.Portal.Web.Models;
using Lmis.Portal.Web.Utils;

namespace Lmis.Portal.Web.Pages.Management
{
    public partial class FilesList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserUtil.GotoLoginIfNoSuperadmin();

            FillDataGrid();
        }

        protected void btnAddNew_OnClick(object sender, EventArgs e)
        {
            contentControl.Model = new ContentModel();
            mpeAddEdit.Show();
        }

        protected void contentsControl_OnEditItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Contents.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var converter = new ContentEntityModelConverter(DataContext);
            var model = converter.Convert(entity);

            contentControl.Model = model;
            mpeAddEdit.Show();
        }

        protected void contentsControl_OnDeleteItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Contents.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            entity.DateDeleted = DateTime.Now;
            DataContext.SubmitChanges();

            FillDataGrid();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var converter = new ContentModelEntityConverter(DataContext);

            var model = contentControl.Model;
            if (model.ID != null)
            {
                var entity = DataContext.LP_Contents.FirstOrDefault(n => n.ID == model.ID);
                if (entity == null)
                    return;

                converter.FillObject(entity, model);
            }
            else
            {
                var entity = converter.Convert(model);
                entity.Type = "Content";

                DataContext.LP_Contents.InsertOnSubmit(entity);
            }

            DataContext.SubmitChanges();

            mpeAddEdit.Hide();

            FillDataGrid();
        }

        private void FillDataGrid()
        {
            var entities = (from n in DataContext.LP_Contents
                            where n.DateDeleted == null && 
                                  n.Type == "Content"
                            orderby n.DateCreated
                            select n).ToList();

            var converter = new ContentEntityModelConverter(DataContext);

            var models = (from n in entities
                          let m = converter.Convert(n)
                          select m).ToList();

            var model = new ContentsModel();
            model.List = models;

            contentsControl.Model = model;
        }
    }
}