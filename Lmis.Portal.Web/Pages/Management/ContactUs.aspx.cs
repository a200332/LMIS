using System;
using System.Linq;
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
            var entity = DataContext.LP_Contents.FirstOrDefault(n => n.DateDeleted == null && n.Type == "ContactUs");
            if (entity == null)
            {
                entity = new LP_Content
                {
                    ID = Guid.NewGuid(),
                    DateCreated = DateTime.Now,
                    Type = "AboutUs"
                };

                DataContext.LP_Contents.InsertOnSubmit(entity);
            }

            entity.FullText = htmlEditor.Html;

            DataContext.SubmitChanges();
        }

        protected void FillHtmlEditor()
        {
            var entity = DataContext.LP_Contents.FirstOrDefault(n => n.DateDeleted == null && n.Type == "ContactUs");
            if (entity != null)
            {
                htmlEditor.Html = entity.FullText;
            }
        }
    }
}