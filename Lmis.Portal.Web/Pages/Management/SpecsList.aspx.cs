using System;
using System.Linq;
using CITI.EVO.Tools.Extensions;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Common;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Converters.ModelToEntity;
using Lmis.Portal.Web.Models;
using Lmis.Portal.Web.Utils;

namespace Lmis.Portal.Web.Pages.Management
{
    public partial class SpecsList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserUtil.GotoLoginIfNoSuperadmin();

            FillDataGrid();
        }

        protected void specTreeControl_OnAddChild(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Specs.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var model = new SpecModel();
            model.ParentID = entity.ID;

            specCategoryControl.Model = model;
            specDataControl.Model = model;

            rbType.TrySetSelectedValue("Category");

            mpeAddEditSpec.Show();
        }

        protected void specTreeControl_OnEditItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Specs.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var converter = new SpecEntityModelConverter(DataContext);
            var model = converter.Convert(entity);

            specCategoryControl.Model = model;
            specDataControl.Model = model;

            if (model.IsCategory.GetValueOrDefault())
            {
                specCategoryControl.Visible = true;
                specDataControl.Visible = false;

                rbType.TrySetSelectedValue("Category");
            }
            else
            {
                specCategoryControl.Visible = false;
                specDataControl.Visible = true;

                rbType.TrySetSelectedValue("Data");
            }

            mpeAddEditSpec.Show();
        }

        protected void specTreeControl_OnDeleteItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Specs.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            entity.DateDeleted = DateTime.Now;
            DataContext.SubmitChanges();

            FillDataGrid();
        }

        protected void specTreeControl_OnUpItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Specs.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var query = from n in DataContext.LP_Specs
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

        protected void specTreeControl_OnDownItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Specs.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var query = from n in DataContext.LP_Specs
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

        protected void rbType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var selValue = rbType.TryGetStringValue();
            if (selValue == "Category")
            {
                specCategoryControl.Visible = true;
                specDataControl.Visible = false;
            }
            else
            {
                specCategoryControl.Visible = false;
                specDataControl.Visible = true;
            }

            mpeAddEditSpec.Show();
        }

        protected void btnSaveSpec_OnClick(object sender, EventArgs e)
        {
            var converter = new SpecModelEntityConverter(DataContext);

            var model = GetActiveModel();
            if (model.ID != null)
            {
                var entity = DataContext.LP_Specs.FirstOrDefault(n => n.ID == model.ID);
                if (entity == null)
                    return;

                converter.FillObject(entity, model);
            }
            else
            {
                var entity = converter.Convert(model);
                DataContext.LP_Specs.InsertOnSubmit(entity);
            }

            DataContext.SubmitChanges();

            mpeAddEditSpec.Hide();

            FillDataGrid();
        }

        protected void btnAddSpec_OnClick(object sender, EventArgs e)
        {
            var model = new SpecModel();

            specCategoryControl.Model = model;
            specDataControl.Model = model;

            rbType.TrySetSelectedValue("Category");

            mpeAddEditSpec.Show();
        }

        protected SpecModel GetActiveModel()
        {
            var selValue = rbType.TryGetStringValue();
            if (selValue == "Category")
            {
                var model = specCategoryControl.Model;
                model.IsCategory = true;

                return model;
            }

            if (selValue == "Data")
            {
                var model = specDataControl.Model;
                model.IsCategory = false;

                return model;
            }

            return null;
        }

        protected void FillDataGrid()
        {
            var converter = new SpecEntityModelConverter(DataContext);

            var entities = (from n in DataContext.LP_Specs
                            where n.DateDeleted == null
                            orderby n.OrderIndex, n.DateCreated
                            select n).ToList();

            var models = entities.Select(n => converter.Convert(n)).ToList();

            var specsModel = new SpecsModel { List = models };
            specTreeControl.Model = specsModel;
        }
    }
}