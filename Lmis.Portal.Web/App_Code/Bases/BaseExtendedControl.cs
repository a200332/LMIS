using System;
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
	}
}
