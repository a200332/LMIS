using System;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.SchemaManipulation
{
	public partial class ReportsControl : BaseExtendedControl<ReportsModel>
	{
		public event EventHandler<GenericEventArgs<Guid>> ViewReport;
		protected virtual void OnViewReport(Guid value)
		{
			if (ViewReport != null)
				ViewReport(this, new GenericEventArgs<Guid>(value));
		}

		public event EventHandler<GenericEventArgs<Guid>> EditReport;
		protected virtual void OnEditReport(Guid value)
		{
			if (EditReport != null)
				EditReport(this, new GenericEventArgs<Guid>(value));
		}

		public event EventHandler<GenericEventArgs<Guid>> DeleteReport;
		protected virtual void OnDeleteReport(Guid value)
		{
			if (DeleteReport != null)
				DeleteReport(this, new GenericEventArgs<Guid>(value));
		}


		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override void OnSetModel(object model, Type type)
		{
			var reportsModel = (ReportsModel)model;
			gvData.DataSource = reportsModel.List;
			gvData.DataBind();
		}

		protected void btnView_OnCommand(object sender, CommandEventArgs e)
		{
			var entityId = DataConverter.ToNullableGuid(e.CommandArgument);
			if (entityId == null)
				return;

			OnViewReport(entityId.Value);
		}

		protected void btnEdit_OnCommand(object sender, CommandEventArgs e)
		{
			var entityId = DataConverter.ToNullableGuid(e.CommandArgument);
			if (entityId == null)
				return;

			OnViewReport(entityId.Value);
		}

		protected void btnDelete_OnCommand(object sender, CommandEventArgs e)
		{
			var entityId = DataConverter.ToNullableGuid(e.CommandArgument);
			if (entityId == null)
				return;

			OnDeleteReport(entityId.Value);
		}
	}
}
