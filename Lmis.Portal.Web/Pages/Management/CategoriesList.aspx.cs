using System;
using System.Linq;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Common;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Converters.ModelToEntity;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Pages.Management
{
	public partial class CategoriesList : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			FillCategories();
		}

		protected void categoriesControl_OnAddChildCategory(object sender, GenericEventArgs<Guid> e)
		{
			var entity = DataContext.LP_Categories.FirstOrDefault(n => n.ID == e.Value);
			if (entity == null)
				return;

			var model = new CategoryModel();
			model.ParentID = entity.ID;

			categoryControl.Model = model;
			mpeAddEditCategory.Show();
		}

		protected void categoriesControl_OnEditCategory(object sender, GenericEventArgs<Guid> e)
		{
			var entity = DataContext.LP_Categories.FirstOrDefault(n => n.ID == e.Value);
			if (entity == null)
				return;

			var converter = new CategoryEntityModelConverter(DataContext);
			var model = converter.Convert(entity);

			categoryControl.Model = model;
			mpeAddEditCategory.Show();
		}

		protected void categoriesControl_OnDeleteCategory(object sender, GenericEventArgs<Guid> e)
		{
			var entity = DataContext.LP_Categories.FirstOrDefault(n => n.ID == e.Value);
			if (entity == null)
				return;

			entity.DateDeleted = DateTime.Now;
			DataContext.SubmitChanges();

			FillCategories();
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

			FillCategories();
		}

		protected void btnAddCategory_OnClick(object sender, EventArgs e)
		{
			var model = new CategoryModel();

			categoryControl.Model = model;
			mpeAddEditCategory.Show();
		}

		protected void FillCategories()
		{
			var converter = new CategoryEntityModelConverter(DataContext);

			var entities = DataContext.LP_Categories.Where(n => n.DateDeleted == null).ToList();

			var models = entities.Select(n => converter.Convert(n)).ToList();

			var categoriesModel = new CategoriesModel { List = models };
			categoriesControl.Model = categoriesModel;
		}
	}
}