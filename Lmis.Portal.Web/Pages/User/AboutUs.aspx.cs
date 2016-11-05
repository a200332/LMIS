using System;
using System.Linq;
using Lmis.Portal.Web.Bases;

namespace Lmis.Portal.Web.Pages.User
{
    public partial class AboutUs : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var entity = DataContext.LP_Contents.FirstOrDefault(n => n.DateDeleted == null && n.Type == "AboutUs");
            if (entity != null)
            {
                dvFullText.InnerHtml = entity.FullText;
            }
        }
    }
}