using System;
using System.Linq;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.SchemaManipulation
{
	public partial class ReportControl : BaseExtendedControl<ReportModel>
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			var categories = DataContext.LP_Categories.Where(n => n.DateDeleted == null);
			var tables = DataContext.LP_Tables.Where(n => n.DateDeleted == null);
			var logics = DataContext.LP_Logics.Where(n => n.DateDeleted == null);

			cbxCategory.DataSource = categories;
			cbxCategory.DataBind();

			cbxLogic.DataSource = logics;
			cbxLogic.DataBind();

			cbxTable.DataSource = tables;
			cbxTable.DataBind();
		}
	}
}
