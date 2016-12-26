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
    public partial class CareersList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserUtil.GotoLoginIfNoSuperadmin();

            FillDataGrid();
        }

        protected void btnAddNew_OnClick(object sender, EventArgs e)
        {
            mainCareerControl.Visible = true;
            subCareerControl.Visible = false;

            mainCareerControl.Model = new CareerModel();
            mpeAddEdit.Show();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var converter = new CareerModelEntityConverter(DataContext);

            var model = (CareerModel)null;

            if (mainCareerControl.Visible)
                model = mainCareerControl.Model;
            else
                model = subCareerControl.Model;

            if (model.ID != null)
            {
                var entity = DataContext.LP_Careers.FirstOrDefault(n => n.ID == model.ID);
                if (entity == null)
                    return;

                converter.FillObject(entity, model);
            }
            else
            {
                var entity = converter.Convert(model);

                var query = (from n in DataContext.LP_Careers
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

                DataContext.LP_Careers.InsertOnSubmit(entity);
            }

            DataContext.SubmitChanges();

            mpeAddEdit.Hide();

            FillDataGrid();
        }

        protected void careersControl_OnAddChild(object sender, GenericEventArgs<Guid> e)
        {
            mainCareerControl.Visible = false;
            subCareerControl.Visible = true;

            var model = new CareerModel
            {
                ParentID = e.Value
            };

            subCareerControl.Model = model;

            mpeAddEdit.Show();
        }

        protected void careersControl_OnEditItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Careers.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var converter = new CareerEntityModelConverter(DataContext);
            var model = converter.Convert(entity);

            if (model.ParentID == null)
            {
                mainCareerControl.Visible = true;
                subCareerControl.Visible = false;
            }
            else
            {
                mainCareerControl.Visible = false;
                subCareerControl.Visible = true;
            }

            mainCareerControl.Model = model;
            subCareerControl.Model = model;

            mpeAddEdit.Show();
        }

        protected void careersControl_OnDeleteItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Careers.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            entity.DateDeleted = DateTime.Now;
            DataContext.SubmitChanges();

            FillDataGrid();
        }

        protected void careersControl_OnUpItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Careers.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var query = from n in DataContext.LP_Careers
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

            var items = query.ToList();

            for (int i = 0; i < items.Count; i++)
                items[i].OrderIndex = i;

            var currentItem = items.FirstOrDefault(n => n.ID == e.Value);
            if (currentItem == null)
                return;

            var index = items.IndexOf(currentItem);
            if (index < 0 || index == 0)
                return;

            items[index] = items[index - 1];
            items[index - 1] = currentItem;

            for (int i = 0; i < items.Count; i++)
                items[i].OrderIndex = i;

            DataContext.SubmitChanges();

            FillDataGrid();
        }

        protected void careersControl_OnDownItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Careers.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var query = from n in DataContext.LP_Careers
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

            var items = query.ToList();

            for (int i = 0; i < items.Count; i++)
                items[i].OrderIndex = i;

            var currentItem = items.FirstOrDefault(n => n.ID == e.Value);
            if (currentItem == null)
                return;

            var index = items.IndexOf(currentItem);
            if (index < 0 || index == (items.Count - 1))
                return;

            items[index] = items[index + 1];
            items[index + 1] = currentItem;

            for (int i = 0; i < items.Count; i++)
                items[i].OrderIndex = i;

            DataContext.SubmitChanges();

            FillDataGrid();
        }

        private void FillDataGrid()
        {
            var entities = (from n in DataContext.LP_Careers
                            where n.DateDeleted == null
                            orderby n.OrderIndex, n.DateCreated
                            select n).ToList();

            var converter = new CareerEntityModelConverter(DataContext);

            var models = (from n in entities
                          let m = converter.Convert(n)
                          select m).ToList();

            var model = new CareersModel();
            model.List = models;

            careersControl.Model = model;
        }
    }
}