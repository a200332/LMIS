using System;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.Others
{
	public partial class CategoriesControl : BaseExtendedControl<CategoriesModel>
	{
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

		protected override void OnSetModel(object model, Type type)
		{
			var categoriesModel = (CategoriesModel)model;
			tlData.DataSource = categoriesModel.List;
			tlData.DataBind();
		}
	}
}