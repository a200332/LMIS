using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Security;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web
{
    public partial class Site : BaseMasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Page.PostBackControl == btEngLang)
                LanguageUtil.SetLanguage("en-US");

            if (Page.PostBackControl == btGeoLang)
                LanguageUtil.SetLanguage("ka-GE");

            if (Page.PostBackControl == btTranslationMode)
                TranslationUtil.TranslationMode = !TranslationUtil.TranslationMode;

            if (!UmUtil.Instance.IsLogged || !UmUtil.Instance.CurrentUser.IsSuperAdmin)
            {
                if (TranslationUtil.TranslationMode)
                    TranslationUtil.TranslationMode = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            btLogin.Visible = !UmUtil.Instance.IsLogged;
            btLogout.Visible = UmUtil.Instance.IsLogged;

            liAdmin.Visible = (UmUtil.Instance.IsLogged && UmUtil.Instance.CurrentUser.IsSuperAdmin);

            btTranslationMode.Visible = (UmUtil.Instance.IsLogged && UmUtil.Instance.CurrentUser.IsSuperAdmin);

            foreach (var childLink in GetCurrentUrlLinks(this))
            {
                if (childLink.CssClass == "mcolor")
                    childLink.CssClass = "mcolor_active ";
            }

            FillMainLinks();

            imgLogo.ImageUrl = String.Format("~/App_Themes/Default/images/logo_{0}.png", LanguageUtil.GetLanguage());
            imgFLogo.ImageUrl = String.Format("~/App_Themes/Default/images/f-logo_{0}.png", LanguageUtil.GetLanguage());

            var keyword = Request["Keyword"];
            if (!String.IsNullOrWhiteSpace(keyword))
                tbxSearch.Text = keyword;
        }

        protected void btEngLang_Click(object sender, EventArgs e)
        {
        }

        protected void btGeoLang_Click(object sender, EventArgs e)
        {
        }

        protected void btLogin_Click(object sender, EventArgs e)
        {
            UmUtil.Instance.GoToLogin();
        }

        protected void btLogout_Click(object sender, EventArgs e)
        {
            UmUtil.Instance.GoToLogout();
        }

        protected void btTranslationMode_Click(object sender, EventArgs e)
        {
        }

        protected void tbxSearch_OnTextChanged(object sender, EventArgs e)
        {
            var url = String.Format("~/Pages/User/SearchResult.aspx?keyword={0}", HttpUtility.UrlEncode(tbxSearch.Text));
            Response.Redirect(url);
        }

        private void FillMainLinks()
        {
            var page = Page as BasePage;
            if (page == null)
                return;

            var dbContext = page.DataContext;

            var entities = (from n in dbContext.LP_Links
                            where n.DateDeleted == null && n.ParentID == null
                            orderby n.OrderIndex, n.DateCreated
                            select n).ToList();

            var converter = new LinkEntityModelConverter(dbContext);

            var models = (from n in entities
                          let m = converter.Convert(n)
                          select m).ToList();

            var model = new LinksModel();
            model.List = models;

            mainLinksControl.Model = model;
        }

        protected IEnumerable<HyperLink> GetCurrentUrlLinks(Control control)
        {
            var links = (from n in UserInterfaceUtil.TraverseChildren(this)
                         let l = n as HyperLink
                         where l != null
                         select l);

            foreach (var link in links)
            {
                var linkUrl = GetLinkAbsUrl(link);
                if (IsRequestLink(linkUrl))
                {
                    yield return link;
                }
            }
        }

        protected bool IsRequestLink(String linkUrl)
        {
            if (linkUrl != null && linkUrl.Split('?').FirstOrDefault() == Request.Path)
            {
                return true;
            }

            return false;
        }

        protected String GetLinkAbsUrl(HyperLink hyperLink)
        {
            return ResolveUrl(hyperLink.NavigateUrl);
        }
    }
}
