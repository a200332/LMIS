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
    public partial class ProjectsList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserUtil.GotoLoginIfNoSuperadmin();

            FillDataGrid();
        }

        protected void btnAddNew_OnClick(object sender, EventArgs e)
        {
            mainProjectControl.Visible = true;
            subProjectControl.Visible = false;

            mainProjectControl.Model = new ProjectModel();
            mpeAddEdit.Show();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var converter = new ProjectModelEntityConverter(DataContext);

            var model = (ProjectModel)null;

            if (mainProjectControl.Visible)
                model = mainProjectControl.Model;
            else
                model = subProjectControl.Model;

            if (model.ID != null)
            {
                var entity = DataContext.LP_Projects.FirstOrDefault(n => n.ID == model.ID);
                if (entity == null)
                    return;

                converter.FillObject(entity, model);
            }
            else
            {
                var entity = converter.Convert(model);

                var query = (from n in DataContext.LP_Projects
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

                DataContext.LP_Projects.InsertOnSubmit(entity);
            }

            DataContext.SubmitChanges();

            mpeAddEdit.Hide();

            FillDataGrid();
        }

        protected void projectsControl_OnAddChild(object sender, GenericEventArgs<Guid> e)
        {
            mainProjectControl.Visible = false;
            subProjectControl.Visible = true;

            var model = new ProjectModel
            {
                ParentID = e.Value
            };

            subProjectControl.Model = model;

            mpeAddEdit.Show();
        }

        protected void projectsControl_OnEditItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Projects.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var converter = new ProjectEntityModelConverter(DataContext);
            var model = converter.Convert(entity);

            if (model.ParentID == null)
            {
                mainProjectControl.Visible = true;
                subProjectControl.Visible = false;
            }
            else
            {
                mainProjectControl.Visible = false;
                subProjectControl.Visible = true;
            }

            mainProjectControl.Model = model;
            subProjectControl.Model = model;

            mpeAddEdit.Show();
        }

        protected void projectsControl_OnDeleteItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Projects.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            entity.DateDeleted = DateTime.Now;
            DataContext.SubmitChanges();

            FillDataGrid();
        }

        protected void projectsControl_OnUpItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Projects.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var query = from n in DataContext.LP_Projects
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

            var projects = query.ToList();

            for (int i = 0; i < projects.Count; i++)
                projects[i].OrderIndex = i;

            var currentItem = projects.FirstOrDefault(n => n.ID == e.Value);
            if (currentItem == null)
                return;

            var index = projects.IndexOf(currentItem);
            if (index < 0 || index == 0)
                return;

            projects[index] = projects[index - 1];
            projects[index - 1] = currentItem;

            for (int i = 0; i < projects.Count; i++)
                projects[i].OrderIndex = i;

            DataContext.SubmitChanges();

            FillDataGrid();
        }

        protected void projectsControl_OnDownItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Projects.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var query = from n in DataContext.LP_Projects
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

            var projects = query.ToList();

            for (int i = 0; i < projects.Count; i++)
                projects[i].OrderIndex = i;

            var currentItem = projects.FirstOrDefault(n => n.ID == e.Value);
            if (currentItem == null)
                return;

            var index = projects.IndexOf(currentItem);
            if (index < 0 || index == (projects.Count - 1))
                return;

            projects[index] = projects[index + 1];
            projects[index + 1] = currentItem;

            for (int i = 0; i < projects.Count; i++)
                projects[i].OrderIndex = i;

            DataContext.SubmitChanges();

            FillDataGrid();
        }

        private void FillDataGrid()
        {
            var entities = (from n in DataContext.LP_Projects
                            where n.DateDeleted == null
                            orderby n.OrderIndex, n.DateCreated
                            select n).ToList();

            var converter = new ProjectEntityModelConverter(DataContext);

            var models = (from n in entities
                          let m = converter.Convert(n)
                          select m).ToList();

            var model = new ProjectsModel();
            model.List = models;

            projectsControl.Model = model;
        }
    }
}
