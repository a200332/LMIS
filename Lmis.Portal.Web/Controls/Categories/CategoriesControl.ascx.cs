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
		public event EventHandler<EventArgs> AddNew;
		protected virtual void OnAddNew()
		{
			if (AddNew != null)
				AddNew(this, EventArgs.Empty);
		}

		public event EventHandler<GenericEventArgs<Guid>> AddChild;
		protected virtual void OnAddChild(Guid value)
		{
			if (AddChild != null)
				AddChild(this, new GenericEventArgs<Guid>(value));
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnAddChild_OnCommand(object sender, CommandEventArgs e)
		{
			var entityID = DataConverter.ToNullableGuid(e.CommandArgument);
			if (entityID == null)
				return;

			OnAddChild(entityID.Value);
		}

		protected void btnAddNew_OnClick(object sender, EventArgs e)
		{
			OnAddNew();
		}

		protected override void OnSetModel(object model, Type type)
		{
			var categoriesModel = (CategoriesModel)model;
			tlData.DataSource = categoriesModel.List;
			tlData.DataBind();
		}
	}
}