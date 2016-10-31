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
    public partial class LegislationsList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserUtil.GotoLoginIfNoSuperadmin();

            FillDataGrid();
        }

        protected void btnAddNew_OnClick(object sender, EventArgs e)
        {
            legislationControl.Model = new LegislationModel();
            mpeAddEdit.Show();
        }

        protected void legislationsControl_OnEditItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Legislations.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var converter = new LegislationEntityModelConverter(DataContext);
            var model = converter.Convert(entity);

            legislationControl.Model = model;
            mpeAddEdit.Show();
        }

        protected void legislationsControl_OnDeleteItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Legislations.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            entity.DateDeleted = DateTime.Now;
            DataContext.SubmitChanges();

            FillDataGrid();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var converter = new LegislationModelEntityConverter(DataContext);

            var model = legislationControl.Model;
            if (model.ID != null)
            {
                var entity = DataContext.LP_Legislations.FirstOrDefault(n => n.ID == model.ID);
                if (entity == null)
                    return;

                converter.FillObject(entity, model);
            }
            else
            {
                var entity = converter.Convert(model);

                var maxOrder = DataContext.LP_Legislations.Max(n => n.OrderIndex);
                entity.OrderIndex = maxOrder.GetValueOrDefault() + 1;

                DataContext.LP_Legislations.InsertOnSubmit(entity);
            }

            DataContext.SubmitChanges();

            mpeAddEdit.Hide();

            FillDataGrid();
        }

        private void FillDataGrid()
        {
            var entities = (from n in DataContext.LP_Legislations
                            where n.DateDeleted == null
                            orderby n.OrderIndex, n.DateCreated
                            select n).ToList();

            var converter = new LegislationEntityModelConverter(DataContext);

            var models = (from n in entities
                          let m = converter.Convert(n)
                          select m).ToList();

            var model = new LegislationsModel();
            model.List = models;

            legislationsControl.Model = model;
        }

        protected void legislationsControl_OnUpItem(object sender, GenericEventArgs<Guid> e)
        {
            var legislations = (from n in DataContext.LP_Legislations
                                where n.DateDeleted == null
                                orderby n.OrderIndex, n.DateCreated
                                select n).ToList();

            for (int i = 0; i < legislations.Count; i++)
                legislations[i].OrderIndex = i;

            var currentItem = legislations.FirstOrDefault(n => n.ID == e.Value);
            if (currentItem == null)
                return;

            var index = legislations.IndexOf(currentItem);
            if (index < 0 || index == 0)
                return;

            legislations[index] = legislations[index - 1];
            legislations[index - 1] = currentItem;

            for (int i = 0; i < legislations.Count; i++)
                legislations[i].OrderIndex = i;

            DataContext.SubmitChanges();

            FillDataGrid();
        }

        protected void legislationsControl_OnDownItem(object sender, GenericEventArgs<Guid> e)
        {
            var legislations = (from n in DataContext.LP_Legislations
                                where n.DateDeleted == null
                                orderby n.OrderIndex, n.DateCreated
                                select n).ToList();

            for (int i = 0; i < legislations.Count; i++)
                legislations[i].OrderIndex = i;

            var currentItem = legislations.FirstOrDefault(n => n.ID == e.Value);
            if (currentItem == null)
                return;

            var index = legislations.IndexOf(currentItem);
            if (index < 0 || index == (legislations.Count - 1))
                return;

            legislations[index] = legislations[index + 1];
            legislations[index + 1] = currentItem;

            for (int i = 0; i < legislations.Count; i++)
                legislations[i].OrderIndex = i;

            DataContext.SubmitChanges();

            FillDataGrid();
        }
    }
}