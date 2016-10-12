using System;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Common;

namespace Lmis.Portal.Web.Bases
{
	public class BaseExtendedControl<TModel> : BaseUserControl<TModel> where TModel : class, new()
	{
		public BaseExtendedControl()
		{
		}

		public event EventHandler DataChanged;
		protected virtual void OnDataChanged()
		{
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		public event EventHandler<DataLoadingEventArgs> DataLoading;
		protected virtual void OnDataLoading(DataLoadingEventArgs args)
		{
			if (DataLoading != null)
				DataLoading(this, args);
		}

		public event EventHandler<GenericEventArgs<Guid>> ViewItem;
		protected virtual void OnViewItem(Guid value)
		{
			if (ViewItem != null)
				ViewItem(this, new GenericEventArgs<Guid>(value));
		}

		public event EventHandler<GenericEventArgs<Guid>> EditItem;
		protected virtual void OnEditItem(Guid value)
		{
			if (EditItem != null)
				EditItem(this, new GenericEventArgs<Guid>(value));
		}

		public event EventHandler<GenericEventArgs<Guid>> DeleteItem;
		protected virtual void OnDeleteItem(Guid value)
		{
			if (DeleteItem != null)
				DeleteItem(this, new GenericEventArgs<Guid>(value));
		}

		protected virtual void btnView_OnCommand(object sender, CommandEventArgs e)
		{
			var entityId = DataConverter.ToNullableGuid(e.CommandArgument);
			if (entityId == null)
				return;

			OnViewItem(entityId.Value);
		}

		protected virtual void btnEdit_OnCommand(object sender, CommandEventArgs e)
		{
			var entityId = DataConverter.ToNullableGuid(e.CommandArgument);
			if (entityId == null)
				return;

			OnEditItem(entityId.Value);
		}

		protected virtual void btnDelete_OnCommand(object sender, CommandEventArgs e)
		{
			var entityId = DataConverter.ToNullableGuid(e.CommandArgument);
			if (entityId == null)
				return;

			OnDeleteItem(entityId.Value);
		}
	}
}
