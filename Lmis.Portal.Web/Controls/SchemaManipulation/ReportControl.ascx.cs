using System;
using System.Linq;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.SchemaManipulation
{
	public partial class ReportControl : BaseExtendedControl<ReportModel>
	{
		protected void Page_Init(object sender, EventArgs e)
		{
			FillComboBoxes();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected void FillComboBoxes()
		{
			var categories = DataContext.LP_Categories.Where(n => n.DateDeleted == null);

			cbxCategory.DataSource = categories;
			cbxCategory.DataBind();
		}
	}
}
