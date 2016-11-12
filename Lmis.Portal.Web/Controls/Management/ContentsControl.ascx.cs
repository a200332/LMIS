using System;
using System.Linq;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.Management
{
    public partial class ContentsControl : BaseExtendedControl<ContentsModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnSetModel(object model, Type type)
        {
            var contentsModel = (ContentsModel)model;

            var list = (from n in contentsModel.List
                        let url = GetUrl(n.ID)
                        select new
                        {
                            ID = n.ID,
                            AttachmentSize = n.Attachment.Length,
                            AttachmentName = n.AttachmentName,
                            DateCreated = n.DateCreated,
                            Url = ConvertToAbsoluteUrl(url)
                        });

            gvData.DataSource = list;
            gvData.DataBind();
        }

        public String ConvertToAbsoluteUrl(String relativeUrl)
        {
            var protocol = (Request.IsSecureConnection ? "https" : "http");
            return String.Format("{0}://{1}{2}", protocol, Request.Url.Host, Page.ResolveUrl(relativeUrl));
        }

        private String GetUrl(Guid? ID)
        {
            var url = String.Format("~/Handlers/GetFile.ashx?Type=Content&ID={0}", ID);
            return url;
        }
    }
}