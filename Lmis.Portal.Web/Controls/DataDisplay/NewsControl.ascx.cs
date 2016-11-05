using System;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.DataDisplay
{
    public partial class NewsControl : BaseExtendedControl<NewsModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnSetModel(object model, Type type)
        {
            var newsModel = model as NewsModel;
            if (newsModel == null)
                return;

            dvFullText.InnerHtml = newsModel.FullText;

            pnlImage.Visible = (newsModel.Image != null);
            pnlAttachment.Visible = (newsModel.Attachment != null);

            if (pnlImage.Visible)
            {
                imgPhoto.ImageUrl = GetImageUrl(newsModel.ID);
            }

            if (pnlAttachment.Visible)
            {
                lnkAttachment.Text = newsModel.AttachmentName;
                lnkAttachment.NavigateUrl = GetFileUrl(newsModel.ID);
            }
        }

        private String GetFileUrl(Guid? itemID)
        {
            var url = String.Format("~/Handlers/GetFile.ashx?Type=News&ID={0}", itemID);
            return url;
        }

        private String GetImageUrl(Guid? itemID)
        {
            var url = String.Format("~/Handlers/GetImage.ashx?Type=News&ID={0}", itemID);
            return url;
        }
    }
}