using System;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.Management
{
    public partial class NewsControl : BaseExtendedControl<NewsModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnGetModel(object model, Type type)
        {
            var newsModel = model as NewsModel;
            if (newsModel == null)
                return;

            newsModel.AttachmentName = fuAttachment.FileName;
        }
    }
}