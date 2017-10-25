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
    public partial class BooksList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserUtil.GotoLoginIfNoSuperadmin();

            FillDataGrid();
        }

        protected void btnAddNew_OnClick(object sender, EventArgs e)
        {
            eBookControl.Model = new EBookModel();
            mpeAddEdit.Show();
        }

        protected void eBooksControl_OnEditItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_EBooks.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var converter = new EBookEntityModelConverter(DataContext);
            var model = converter.Convert(entity);

            eBookControl.Model = model;
            mpeAddEdit.Show();
        }

        protected void eBooksControl_OnDeleteItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_EBooks.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            entity.DateDeleted = DateTime.Now;
            DataContext.SubmitChanges();

            FillDataGrid();
        }

        protected void eBooksControl_OnUpItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_EBooks.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var query = from n in DataContext.LP_EBooks
                        where n.DateDeleted == null 
                        orderby n.OrderIndex, n.DateCreated
                        select n;

            var eBooks = query.ToList();

            for (int i = 0; i < eBooks.Count; i++)
                eBooks[i].OrderIndex = i;

            var currentItem = eBooks.FirstOrDefault(n => n.ID == e.Value);
            if (currentItem == null)
                return;

            var index = eBooks.IndexOf(currentItem);
            if (index < 0 || index == 0)
                return;

            eBooks[index] = eBooks[index - 1];
            eBooks[index - 1] = currentItem;

            for (int i = 0; i < eBooks.Count; i++)
                eBooks[i].OrderIndex = i;

            DataContext.SubmitChanges();

            FillDataGrid();
        }

        protected void eBooksControl_OnDownItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_EBooks.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var query = from n in DataContext.LP_EBooks
                        where n.DateDeleted == null
                        orderby n.OrderIndex, n.DateCreated
                        select n;

            var eBooks = query.ToList();

            for (int i = 0; i < eBooks.Count; i++)
                eBooks[i].OrderIndex = i;

            var currentItem = eBooks.FirstOrDefault(n => n.ID == e.Value);
            if (currentItem == null)
                return;

            var index = eBooks.IndexOf(currentItem);
            if (index < 0 || index == (eBooks.Count - 1))
                return;

            eBooks[index] = eBooks[index + 1];
            eBooks[index + 1] = currentItem;

            for (int i = 0; i < eBooks.Count; i++)
                eBooks[i].OrderIndex = i;

            DataContext.SubmitChanges();

            FillDataGrid();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var converter = new EBookModelEntityConverter(DataContext);

            var model = eBookControl.Model;
            if (model.ID != null)
            {
                var entity = DataContext.LP_EBooks.FirstOrDefault(n => n.ID == model.ID);
                if (entity == null)
                    return;

                converter.FillObject(entity, model);
            }
            else
            {
                var entity = converter.Convert(model);
                DataContext.LP_EBooks.InsertOnSubmit(entity);
            }

            DataContext.SubmitChanges();

            mpeAddEdit.Hide();

            FillDataGrid();
        }

        private void FillDataGrid()
        {
            var entities = (from n in DataContext.LP_EBooks
                            where n.DateDeleted == null
                            orderby n.DateCreated descending
                            select n).ToList();

            var converter = new EBookEntityModelConverter(DataContext);

            var models = (from n in entities
                          let m = converter.Convert(n)
                          select m).ToList();

            var model = new EBooksModel();
            model.List = models;

            eBooksControl.Model = model;
        }
    }
}