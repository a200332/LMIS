using System;
using System.Linq;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Bases;

namespace Lmis.Portal.Web.Pages.Management
{
    public partial class ContactUs : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                FillHtmlEditor();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var currentLanguage = LanguageUtil.GetLanguage();

            var entity = (from n in DataContext.LP_Contents
                          where n.DateDeleted == null && n.Type == "ContactUs" && (n.Language == currentLanguage || n.Language == null || n.Language == "")
                          select n).FirstOrDefault();

            if (entity == null)
            {
                entity = new LP_Content
                {
                    ID = Guid.NewGuid(),
                    DateCreated = DateTime.Now,
                    Type = "ContactUs",
                    Language = currentLanguage,
                };

                DataContext.LP_Contents.InsertOnSubmit(entity);
            }

            entity.FullText = htmlEditor.Html;

            DataContext.SubmitChanges();
        }

        protected void FillHtmlEditor()
        {
            var currentLanguage = LanguageUtil.GetLanguage();

            var entity = (from n in DataContext.LP_Contents
                          where n.DateDeleted == null && n.Type == "ContactUs" && (n.Language == currentLanguage || n.Language == null || n.Language == "")
                          select n).FirstOrDefault();

            if (entity != null)
            {
                htmlEditor.Html = entity.FullText;
            }
        }
    }
}