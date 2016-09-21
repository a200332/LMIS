using System;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.DataManipulation
{
	public partial class LogicsControl : BaseExtendedControl<LogicsModel>
	{
		public event EventHandler<GenericEventArgs<Guid>> EditLogic;
		protected virtual void OnEditLogic(Guid value)
		{
			if (EditLogic != null)
				EditLogic(this, new GenericEventArgs<Guid>(value));
		}

		public event EventHandler<GenericEventArgs<Guid>> DeleteLogic;
		protected virtual void OnDeleteLogic(Guid value)
		{
			if (DeleteLogic != null)
				DeleteLogic(this, new GenericEventArgs<Guid>(value));
		}

		public event EventHandler<GenericEventArgs<Guid>> ViewLogic;
		protected virtual void OnViewLogic(Guid value)
		{
			if (ViewLogic != null)
				ViewLogic(this, new GenericEventArgs<Guid>(value));
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnEdit_OnCommand(object sender, CommandEventArgs e)
		{
			var entityID = DataConverter.ToNullableGuid(e.CommandArgument);
			if (entityID == null)
				return;

			OnEditLogic(entityID.Value);
		}

		protected void btnDelete_OnCommand(object sender, CommandEventArgs e)
		{
			var entityID = DataConverter.ToNullableGuid(e.CommandArgument);
			if (entityID == null)
				return;

			OnDeleteLogic(entityID.Value);
		}

		protected void btnView_OnCommand(object sender, CommandEventArgs e)
		{
			var entityID = DataConverter.ToNullableGuid(e.CommandArgument);
			if (entityID == null)
				return;

			OnViewLogic(entityID.Value);
		}

		protected override void OnSetModel(object model, Type type)
		{
			var logicsModel = (LogicsModel)model;
			gvData.DataSource = logicsModel.List;
			gvData.DataBind();
		}
	}
}