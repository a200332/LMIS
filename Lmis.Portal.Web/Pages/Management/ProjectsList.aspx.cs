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
            projectControl.Model = new ProjectModel();
            mpeAddEdit.Show();
        }

        protected void projectsControl_OnEditItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Projects.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var converter = new ProjectEntityModelConverter(DataContext);
            var model = converter.Convert(entity);

            projectControl.Model = model;
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

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var converter = new ProjectModelEntityConverter(DataContext);

            var model = projectControl.Model;
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

                var maxOrder = DataContext.LP_Projects.Max(n => n.OrderIndex);
                entity.OrderIndex = maxOrder.GetValueOrDefault() + 1;

                DataContext.LP_Projects.InsertOnSubmit(entity);
            }

            DataContext.SubmitChanges();

            mpeAddEdit.Hide();

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

        protected void projectsControl_OnUpItem(object sender, GenericEventArgs<Guid> e)
        {
            var projects = (from n in DataContext.LP_Projects
                where n.DateDeleted == null
                orderby n.OrderIndex, n.DateCreated
                select n).ToList();

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
            var projects = (from n in DataContext.LP_Projects
                where n.DateDeleted == null
                orderby n.OrderIndex, n.DateCreated
                select n).ToList();

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
    }
}
