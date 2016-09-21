using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Web.Bases;
using CITI.EVO.UserManagement.DAL.Context;

namespace CITI.EVO.UserManagement.Web.Bases
{
    public class BasePage : PageBase
    {
        public BasePage()
        {
        }

        private UserManagementDataContext dataContext;
        public UserManagementDataContext DataContext
        {
            get
            {
                dataContext = (dataContext ?? new UserManagementDataContext());
                return dataContext;
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            DataContext.Dispose();
            base.OnUnload(e);
        }
    }
}