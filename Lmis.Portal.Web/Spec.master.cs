using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Security;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;

namespace Lmis.Portal.Web
{
    public partial class Spec : BaseMasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Page.PostBackControl == btEngLang)
                LanguageUtil.SetLanguage("en-US");

            if (Page.PostBackControl == btGeoLang)
                LanguageUtil.SetLanguage("ka-GE");

            //if (Page.PostBackControl == btTranslationMode)
            //    TranslationUtil.TranslationMode = !TranslationUtil.TranslationMode;

            if (!UmUtil.Instance.IsLogged || !UmUtil.Instance.CurrentUser.IsSuperAdmin)
            {
                if (TranslationUtil.TranslationMode)
                    TranslationUtil.TranslationMode = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
