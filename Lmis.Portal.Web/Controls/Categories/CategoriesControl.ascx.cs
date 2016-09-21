using System;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.Categories
{
	public partial class CategoriesControl : BaseExtendedControl<CategoriesModel>
	{
		public event EventHandler<EventArgs> AddNewCategory;
		protected virtual void OnAddNewCategory()
		{
			if (AddNewCategory != null)
				AddNewCategory(this, EventArgs.Empty);
		}

		public event EventHandler<GenericEventArgs<Guid>> EditCategory;
		protected virtual void OnEditCategory(Guid value)
		{
			if (EditCategory != null)
				EditCategory(this, new GenericEventArgs<Guid>(value));
		}

		public event EventHandler<GenericEventArgs<Guid>> DeleteCategory;
		protected virtual void OnDeleteCategory(Guid value)
		{
			if (DeleteCategory != null)
				DeleteCategory(this, new GenericEventArgs<Guid>(value));
		}

		public event EventHandler<GenericEventArgs<Guid>> AddChildCategory;
		protected virtual void OnAddChildCategory(Guid value)
		{
			if (AddChildCategory != null)
				AddChildCategory(this, new GenericEventArgs<Guid>(value));
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnEdit_OnCommand(object sender, CommandEventArgs e)
		{
			var entityID = DataConverter.ToNullableGuid(tvData.SelectedValue);
			if (entityID == null)
				return;

			OnEditCategory(entityID.Value);
		}

		protected void btnDelete_OnCommand(object sender, CommandEventArgs e)
		{
			var entityID = DataConverter.ToNullableGuid(tvData.SelectedValue);
			if (entityID == null)
				return;

			OnDeleteCategory(entityID.Value);
		}

		protected void btnAddChild_OnCommand(object sender, CommandEventArgs e)
		{
			var entityID = DataConverter.ToNullableGuid(tvData.SelectedValue);
			if (entityID == null)
				return;

			OnAddChildCategory(entityID.Value);
		}

		protected void btnAddNew_OnClick(object sender, EventArgs e)
		{
			OnAddNewCategory();
		}

		protected override void OnSetModel(object model, Type type)
		{
			var categoriesModel = (CategoriesModel)model;
			tvData.DataSource = categoriesModel.List;
			tvData.DataBind();
		}
	}
}