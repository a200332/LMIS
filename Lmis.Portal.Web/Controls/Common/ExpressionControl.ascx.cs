using System;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models.Common;

namespace Lmis.Portal.Web.Controls.Common
{
	public partial class ExpressionControl : BaseExtendedControl<ExpressionModel>
	{
		public String Key
		{
			get { return Convert.ToString(ViewState["Key"]); }
			set { ViewState["Key"] = value; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}
	}
}