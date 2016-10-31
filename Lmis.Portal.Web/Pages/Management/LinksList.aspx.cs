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
	public partial class LinksList : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			UserUtil.GotoLoginIfNoSuperadmin();

			FillDataGrid();
		}

		protected void btnAddNew_OnClick(object sender, EventArgs e)
		{
			mainLinkControl.Model = new LinkModel();

			subLinkControl.Visible = false;
			mainLinkControl.Visible = true;

			mpeAddEdit.Show();
		}

		protected void linksControl_OnAddChild(object sender, GenericEventArgs<Guid> e)
		{
			var entity = DataContext.LP_Links.FirstOrDefault(n => n.ID == e.Value);
			if (entity == null)
				return;

			var model = new LinkModel();
			model.ParentID = entity.ID;

			subLinkControl.Model = model;

			subLinkControl.Visible = true;
			mainLinkControl.Visible = false;

			mpeAddEdit.Show();
		}

		protected void linksControl_OnEditItem(object sender, GenericEventArgs<Guid> e)
		{
			var entity = DataContext.LP_Links.FirstOrDefault(n => n.ID == e.Value);
			if (entity == null)
				return;

			var converter = new LinkEntityModelConverter(DataContext);
			var model = converter.Convert(entity);

			subLinkControl.Visible = (model.ParentID != null);
			mainLinkControl.Visible = (model.ParentID == null);

			mainLinkControl.Model = model;
		    subLinkControl.Model = model;

            mpeAddEdit.Show();
		}

		protected void linksControl_OnDeleteItem(object sender, GenericEventArgs<Guid> e)
		{
			var entity = DataContext.LP_Links.FirstOrDefault(n => n.ID == e.Value);
			if (entity == null)
				return;

			entity.DateDeleted = DateTime.Now;
			DataContext.SubmitChanges();

			FillDataGrid();
		}

        protected void linksControl_OnUpItem(object sender, GenericEventArgs<Guid> e)
        {
            var links = (from n in DataContext.LP_Links
                            where n.DateDeleted == null
                            orderby n.OrderIndex, n.DateCreated
                            select n).ToList();

            for (int i = 0; i < links.Count; i++)
                links[i].OrderIndex = i;

            var currentItem = links.FirstOrDefault(n => n.ID == e.Value);
            if (currentItem == null)
                return;

            var index = links.IndexOf(currentItem);
            if (index < 0 || index == 0)
                return;

            links[index] = links[index - 1];
            links[index - 1] = currentItem;

            for (int i = 0; i < links.Count; i++)
                links[i].OrderIndex = i;

            DataContext.SubmitChanges();

            FillDataGrid();
        }

        protected void linksControl_OnDownItem(object sender, GenericEventArgs<Guid> e)
        {
            var links = (from n in DataContext.LP_Links
                         where n.DateDeleted == null
                            orderby n.OrderIndex, n.DateCreated
                            select n).ToList();

            for (int i = 0; i < links.Count; i++)
                links[i].OrderIndex = i;

            var currentItem = links.FirstOrDefault(n => n.ID == e.Value);
            if (currentItem == null)
                return;

            var index = links.IndexOf(currentItem);
            if (index < 0 || index == (links.Count - 1))
                return;

            links[index] = links[index + 1];
            links[index + 1] = currentItem;

            for (int i = 0; i < links.Count; i++)
                links[i].OrderIndex = i;

            DataContext.SubmitChanges();

            FillDataGrid();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
		{
			var converter = new LinkModelEntityConverter(DataContext);

			var model = mainLinkControl.Model;
			if (subLinkControl.Visible)
			{
				model = subLinkControl.Model;
			}

			if (model.ID != null)
			{
				var entity = DataContext.LP_Links.FirstOrDefault(n => n.ID == model.ID);
				if (entity == null)
					return;

				converter.FillObject(entity, model);
			}
			else
			{
				var entity = converter.Convert(model);
				DataContext.LP_Links.InsertOnSubmit(entity);
			}

			DataContext.SubmitChanges();

			mpeAddEdit.Hide();

			FillDataGrid();
		}

		private void FillDataGrid()
		{
			var entities = (from n in DataContext.LP_Links
							where n.DateDeleted == null
							orderby n.OrderIndex, n.DateCreated
                            select n).ToList();

			var converter = new LinkEntityModelConverter(DataContext);

			var models = (from n in entities
						  let m = converter.Convert(n)
						  select m).ToList();

			var model = new LinksModel();
			model.List = models;

			linksControl.Model = model;
		}
	}
}