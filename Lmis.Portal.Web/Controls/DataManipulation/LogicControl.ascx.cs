using System;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.DataManipulation
{
	public partial class LogicControl : BaseExtendedControl<LogicModel>
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			ApplyViewMode();
		}

		private void ApplyViewMode()
		{
			var model = Model;
			if (model.Type == "Logic")
			{
				pnlLogic.Visible = true;
				pnlQuery.Visible = false;
			}
			else if (model.Type == "Query")
			{
				pnlLogic.Visible = false;
				pnlQuery.Visible = true;
			}
		}

		protected override void OnSetModel(object model, Type type)
		{
			ApplyViewMode();
		}
	}
}