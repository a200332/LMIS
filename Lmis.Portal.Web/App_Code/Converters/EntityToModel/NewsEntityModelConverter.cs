using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.EntityToModel
{
    public class NewsEntityModelConverter : SingleModelConverterBase<LP_News, NewsModel>
    {
        public NewsEntityModelConverter(PortalDataContext dbContext) : base(dbContext)
        {
        }

        public override NewsModel Convert(LP_News source)
        {
            var target = new NewsModel();

            FillObject(target, source);

            return target;
        }

        public override void FillObject(NewsModel target, LP_News source)
        {
            target.ID = source.ID;
            target.Title = source.Title;
            target.Attachment = (source.Attachment != null ? source.Attachment.ToArray() : null);
            target.AttachmentName = source.AttachmentName;
            target.FullText = source.FullText;
            target.NewsDate = source.NewsDate;
            target.Description = source.Description;
            target.Language = source.Language;
            target.Image = (source.Image != null ? source.Image.ToArray() : null);
        }
    }
}