using System;
using System.Collections.Generic;
using System.Linq;
using CITI.EVO.Tools.Comparers;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Common;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Converters.ModelToEntity;
using Lmis.Portal.Web.Models;
using Lmis.Portal.Web.Utils;

namespace Lmis.Portal.Web.Pages.Management
{
    public partial class CategoriesList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserUtil.GotoLoginIfNoSuperadmin();

            FillDataGrid();
        }

        protected void categoriesControl_OnAddChild(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Categories.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var model = new CategoryModel();
            model.ParentID = entity.ID;

            categoryControl.Model = model;
            mpeAddEditCategory.Show();
        }

        protected void categoriesControl_OnEditItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Categories.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var converter = new CategoryEntityModelConverter(DataContext);
            var model = converter.Convert(entity);

            categoryControl.Model = model;
            mpeAddEditCategory.Show();
        }

        protected void categoriesControl_OnDeleteItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Categories.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            entity.DateDeleted = DateTime.Now;
            DataContext.SubmitChanges();

            FillDataGrid();
        }

        protected void categoriesControl_OnUpItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Categories.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var query = from n in DataContext.LP_Categories
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

        protected void categoriesControl_OnDownItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Categories.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var query = from n in DataContext.LP_Categories
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

        protected void btnSaveCatevory_OnClick(object sender, EventArgs e)
        {
            var converter = new CategoryModelEntityConverter(DataContext);

            var model = categoryControl.Model;
            if (model.ID != null)
            {
                var entity = DataContext.LP_Categories.FirstOrDefault(n => n.ID == model.ID);
                if (entity == null)
                    return;

                converter.FillObject(entity, model);
            }
            else
            {
                var entity = converter.Convert(model);
                DataContext.LP_Categories.InsertOnSubmit(entity);
            }

            DataContext.SubmitChanges();

            mpeAddEditCategory.Hide();

            FillDataGrid();
        }

        protected void btnAddCategory_OnClick(object sender, EventArgs e)
        {
            var model = new CategoryModel();

            categoryControl.Model = model;
            mpeAddEditCategory.Show();
        }

        protected void FillDataGrid()
        {
            var converter = new CategoryEntityModelConverter(DataContext);
            var entities = DataContext.LP_Categories.Where(n => n.DateDeleted == null).ToList();

            CategoryUtil.Sort(entities);

            var models = entities.Select(n => converter.Convert(n)).ToList();

            var categoriesModel = new CategoriesModel { List = models };
            categoriesControl.Model = categoriesModel;
        }
    }
}