using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Security;
using CITI.EVO.UserManagement.Web.Bases;

public partial class SiteMaster : MasterPageBase
{
    protected void Page_Init(object sender, EventArgs e)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
     //   foreach (var childLink in GetCurrentUrlLinks(this))
     //   {
     //       if (childLink.CssClass == "highlevel")
     //       {
     //           childLink.CssClass = "highlevel_active ";
     //       }
     //       else if (childLink.CssClass == "sub")
     //       {
     //           childLink.CssClass = "sub_active ";
     //       }
     //   }

	    //DisplayCurrentUserData();
    }

    protected void lnkLogout_OnClick(object sender, EventArgs e)
    {
        UmUtil.Instance.GoToLogout();
    }

    protected void lnkChangePassword_OnClick(object sender, EventArgs e)
    {
        UmUtil.Instance.GoToChangePassword();
    }

	protected void DisplayCurrentUserData()
	{
		var instance = UmUtil.Instance;
		if (instance != null && instance.IsLogged)
		{
			var currentUser = instance.CurrentUser;
			if (currentUser != null)
			{
				lblLoginName.Text = currentUser.LoginName;
				lblFirstName.Text = currentUser.FirstName;
				lblLastName.Text = currentUser.LastName;
			}
		}
	}
}
