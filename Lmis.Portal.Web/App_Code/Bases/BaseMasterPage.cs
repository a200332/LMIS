using System.Web.UI;
using CITI.EVO.Tools.Web.Bases;
using CITI.EVO.Web.UI;
using Lmis.Portal.DAL.DAL;

namespace Lmis.Portal.Web.Bases
{
    public class BaseMasterPage : MasterPageBase
    {
        public BaseMasterPage()
        {
        }

        public PortalDataContext DataContext
        {
            get
            {
                var page = Page as BasePage;
                if (page == null)
                    return null;

                return page.DataContext;
            }
        }
    }
}