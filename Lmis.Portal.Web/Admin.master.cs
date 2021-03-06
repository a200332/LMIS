﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Security;
using CITI.EVO.Tools.Utils;
using CITI.EVO.Tools.Web.Bases;

namespace Lmis.Portal.Web
{
    public partial class Admin : MasterPageBase
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
            //liAdmin.Visible = (UmUtil.Instance.IsLogged && UmUtil.Instance.CurrentUser.IsSuperAdmin);
            btTranslationMode.Visible = (UmUtil.Instance.IsLogged && UmUtil.Instance.CurrentUser.IsSuperAdmin);

            foreach (var childLink in GetCurrentUrlLinks(this))
            {
                if (childLink.CssClass == "mcolor")
                    childLink.CssClass = "mcolor_active ";
            }

            imgLogo.ImageUrl = String.Format("~/App_Themes/Default/images/logo_{0}.png", LanguageUtil.GetLanguage());
            imgFLogo.ImageUrl = String.Format("~/App_Themes/Default/images/f-logo_{0}.png", LanguageUtil.GetLanguage());

            btEngLang.Enabled = LanguageUtil.GetLanguage() != "en-US";
            btGeoLang.Enabled = LanguageUtil.GetLanguage() != "ka-GE";
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
