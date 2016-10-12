using System;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Utils;

namespace Lmis.Portal.Web.Pages.Management
{
	public partial class AddEditTable : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			UserUtil.GotoLoginIfNoSuperadmin();
		}
	}
}