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
            mainLegislationControl.Visible = true;
            subLegislationControl.Visible = false;

            mainLegislationControl.Model = new LegislationModel();
            mpeAddEdit.Show();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var converter = new LegislationModelEntityConverter(DataContext);

            var model = (LegislationModel)null;

            if (mainLegislationControl.Visible)
                model = mainLegislationControl.Model;
            else
                model = subLegislationControl.Model;

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

                var query = (from n in DataContext.LP_Legislations
                             where n.DateDeleted == null
                             select n);

                if (model.ParentID == null)
                {
                    query = (from n in query
                             where n.ParentID == null
                             select n);
                }
                else
                {
                    query = (from n in query
                             where n.ParentID == model.ParentID
                             select n);
                }

                var maxOrder = query.Max(n => n.OrderIndex);
                entity.OrderIndex = maxOrder.GetValueOrDefault() + 1;

                DataContext.LP_Legislations.InsertOnSubmit(entity);
            }

            DataContext.SubmitChanges();

            mpeAddEdit.Hide();

            FillDataGrid();
        }

        protected void legislationsControl_OnAddChild(object sender, GenericEventArgs<Guid> e)
        {
            mainLegislationControl.Visible = false;
            subLegislationControl.Visible = true;

            var model = new LegislationModel
            {
                ParentID = e.Value
            };

            subLegislationControl.Model = model;

            mpeAddEdit.Show();
        }

        protected void legislationsControl_OnEditItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Legislations.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var converter = new LegislationEntityModelConverter(DataContext);
            var model = converter.Convert(entity);

            if (model.ParentID == null)
            {
                mainLegislationControl.Visible = true;
                subLegislationControl.Visible = false;
            }
            else
            {
                mainLegislationControl.Visible = false;
                subLegislationControl.Visible = true;
            }

            mainLegislationControl.Model = model;
            subLegislationControl.Model = model;

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

        protected void legislationsControl_OnUpItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Legislations.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var query = from n in DataContext.LP_Legislations
                        where n.DateDeleted == null
                        select n;

            if (entity.ParentID == null)
            {
                query = from n in query
                        where n.ParentID == null
                        select n;
            }
            else
            {
                query = from n in query
                        where n.ParentID == entity.ParentID
                        select n;
            }

            query = from n in query
                    orderby n.OrderIndex, n.DateCreated
                    select n;

            var legislations = query.ToList();

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
            var entity = DataContext.LP_Legislations.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var query = from n in DataContext.LP_Legislations
                        where n.DateDeleted == null
                        select n;

            if (entity.ParentID == null)
            {
                query = from n in query
                        where n.ParentID == null
                        select n;
            }
            else
            {
                query = from n in query
                        where n.ParentID == entity.ParentID
                        select n;
            }

            query = from n in query
                    orderby n.OrderIndex, n.DateCreated
                    select n;

            var legislations = query.ToList();

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
    }
}