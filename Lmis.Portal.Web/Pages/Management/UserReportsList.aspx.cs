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
    public partial class UserReportsList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserUtil.GotoLoginIfNoSuperadmin();

            FillDataGrid();
        }

        protected void btnAddNew_OnClick(object sender, EventArgs e)
        {
            mainUserReportControl.Visible = true;
            subUserReportControl.Visible = false;

            mainUserReportControl.Model = new UserReportModel();
            mpeAddEdit.Show();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var converter = new UserReportModelEntityConverter(DataContext);

            var model = (UserReportModel)null;

            if (mainUserReportControl.Visible)
                model = mainUserReportControl.Model;
            else
                model = subUserReportControl.Model;

            if (model.ID != null)
            {
                var entity = DataContext.LP_UserReports.FirstOrDefault(n => n.ID == model.ID);
                if (entity == null)
                    return;

                converter.FillObject(entity, model);
            }
            else
            {
                var entity = converter.Convert(model);

                var query = (from n in DataContext.LP_UserReports
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

                DataContext.LP_UserReports.InsertOnSubmit(entity);
            }

            DataContext.SubmitChanges();

            mpeAddEdit.Hide();

            FillDataGrid();
        }

        protected void userReportsControl_OnAddChild(object sender, GenericEventArgs<Guid> e)
        {
            mainUserReportControl.Visible = false;
            subUserReportControl.Visible = true;

            var model = new UserReportModel
            {
                ParentID = e.Value
            };

            subUserReportControl.Model = model;

            mpeAddEdit.Show();
        }

        protected void userReportsControl_OnEditItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_UserReports.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var converter = new UserReportEntityModelConverter(DataContext);
            var model = converter.Convert(entity);

            if (model.ParentID == null)
            {
                mainUserReportControl.Visible = true;
                subUserReportControl.Visible = false;
            }
            else
            {
                mainUserReportControl.Visible = false;
                subUserReportControl.Visible = true;
            }

            mainUserReportControl.Model = model;
            subUserReportControl.Model = model;

            mpeAddEdit.Show();
        }

        protected void userReportsControl_OnDeleteItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_UserReports.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            entity.DateDeleted = DateTime.Now;
            DataContext.SubmitChanges();

            FillDataGrid();
        }

        protected void userReportsControl_OnUpItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_UserReports.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var query = from n in DataContext.LP_UserReports
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

            var list = query.ToList();

            for (int i = 0; i < list.Count; i++)
                list[i].OrderIndex = i;

            var currentItem = list.FirstOrDefault(n => n.ID == e.Value);
            if (currentItem == null)
                return;

            var index = list.IndexOf(currentItem);
            if (index < 0 || index == 0)
                return;

            list[index] = list[index - 1];
            list[index - 1] = currentItem;

            for (int i = 0; i < list.Count; i++)
                list[i].OrderIndex = i;

            DataContext.SubmitChanges();

            FillDataGrid();
        }

        protected void userReportsControl_OnDownItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_UserReports.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var query = from n in DataContext.LP_UserReports
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

            var list = query.ToList();

            for (int i = 0; i < list.Count; i++)
                list[i].OrderIndex = i;

            var currentItem = list.FirstOrDefault(n => n.ID == e.Value);
            if (currentItem == null)
                return;

            var index = list.IndexOf(currentItem);
            if (index < 0 || index == (list.Count - 1))
                return;

            list[index] = list[index + 1];
            list[index + 1] = currentItem;

            for (int i = 0; i < list.Count; i++)
                list[i].OrderIndex = i;

            DataContext.SubmitChanges();

            FillDataGrid();
        }

        private void FillDataGrid()
        {
            var entities = (from n in DataContext.LP_UserReports
                            where n.DateDeleted == null
                            orderby n.OrderIndex, n.DateCreated
                            select n).ToList();

            var converter = new UserReportEntityModelConverter(DataContext);

            var models = (from n in entities
                          let m = converter.Convert(n)
                          select m).ToList();

            var model = new UserReportsModel();
            model.List = models;

            userReportsControl.Model = model;
        }
    }
}
