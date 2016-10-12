using System;
using CITI.EVO.Tools.Security;

namespace Lmis.Portal.Web.Utils
{
	/// <summary>
	/// Summary description for UserUtil
	/// </summary>
	public static class UserUtil
	{
		public static void GotoLoginIfNoSuperadmin()
		{
			if (!UmUtil.Instance.IsLogged)
				UmUtil.Instance.GoToLogin();

			if (!UmUtil.Instance.CurrentUser.IsSuperAdmin)
				UmUtil.Instance.GoToLogin();
		}

		public static void GotoLoginIfNoAccess(String permisionKey)
		{
			if (!UmUtil.Instance.HasAccess(permisionKey))
				UmUtil.Instance.GoToLogin();
		}
	}
}