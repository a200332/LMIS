using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CITI.EVO.UserManagement.DAL.Context;

namespace CITI.EVO.UserManagement.Web.Bases
{

    public class BaseUserControl : UserControl
    {
        public BaseUserControl()
        {

        }

        private BasePage CurrentBasePage
        {
            get
            {
                return base.Page as BasePage;
            }
        }

        public UserManagementDataContext DataContext
        {
            get
            {
                if (Page == null)
                    throw new InvalidCastException("Can not convert to base page type.");

                return CurrentBasePage.DataContext;
            }
        }

        protected Control PagePostBackControl
        {
            get
            {
                return CurrentBasePage.PostBackControl;
            }
        }
    }
}