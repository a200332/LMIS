using System;
using System.Linq;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;

namespace Lmis.Portal.Web.Pages.User
{
	public partial class ContactUs : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            var currentLanguage = LanguageUtil.GetLanguage();

            var entity = (from n in DataContext.LP_Contents
                          where n.DateDeleted == null && n.Type == "ContactUs" && (n.Language == currentLanguage || n.Language == null || n.Language == "")
                          select n).FirstOrDefault();

            if (entity != null)
            {
                dvFullText.InnerHtml = entity.FullText;
            }
        }
    }
}