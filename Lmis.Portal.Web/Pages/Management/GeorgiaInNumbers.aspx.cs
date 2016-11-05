using System;
using System.Linq;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Bases;

namespace Lmis.Portal.Web.Pages.Management
{
    public partial class GeorgiaInNumbers : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var entity = DataContext.LP_Contents.FirstOrDefault(n => n.DateDeleted == null && n.Type == "GeorgiaInNumbers");
            if (entity == null)
            {
                entity = new LP_Content
                {
                    ID = Guid.NewGuid(),
                    DateCreated = DateTime.Now,
                    Type = "GeorgiaInNumbers"
                };

                DataContext.LP_Contents.InsertOnSubmit(entity);
            }

            entity.AttachmentName = fuAttachment.FileName;
            entity.Attachment = fuAttachment.FileBytes;

            DataContext.SubmitChanges();
        }
    }
}