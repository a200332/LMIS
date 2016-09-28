using System;
using System.Linq;
using CITI.EVO.Tools.Extensions;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.SchemaManipulation
{
	public partial class ReportLogicControl : BaseExtendedControl<ReportLogicModel>
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			FillComboBoxes();
		}

		protected void cbxTable_OnSelectedIndexChanged(object sender, EventArgs e)
		{

		}

		protected void FillComboBoxes()
		{
			var tables = DataContext.LP_Tables.Where(n => n.DateDeleted == null);

			cbxTable.DataSource = tables;
			cbxTable.DataBind();

			var selTableID = cbxTable.TryGetGuidValue();
			if (selTableID != null)
			{
				var logics = DataContext.LP_Logics.Where(n => n.DateDeleted == null && n.TableID == selTableID);

				cbxLogic.DataSource = logics;
				cbxLogic.DataBind();
			}
		}
	}
}