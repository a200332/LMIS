using System;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.Management
{
    public partial class ContentControl : BaseExtendedControl<ContentModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnGetModel(object model, Type type)
        {
            var castedModel = model as ContentModel;
            if (castedModel == null)
                return;

            castedModel.Attachment = fuFileData.FileBytes;
            castedModel.AttachmentName = fuFileData.FileName;
        }
    }
}