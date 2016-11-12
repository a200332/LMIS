using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.EntityToModel
{
    public class ContentEntityModelConverter : SingleModelConverterBase<LP_Content, ContentModel>
    {
        public ContentEntityModelConverter(PortalDataContext dbContext) : base(dbContext)
        {
        }

        public override ContentModel Convert(LP_Content source)
        {
            var target = new ContentModel();
            FillObject(target, source);

            return target;
        }

        public override void FillObject(ContentModel target, LP_Content source)
        {
            target.ID = source.ID;
            target.Type = source.Type;
            target.Title = source.Title;
            target.Image = (source.Image != null ? source.Image.ToArray() : null);
            target.FullText = source.FullText;
            target.Language = source.Language;
            target.Description = source.Description;
            target.Attachment = (source.Attachment != null ? source.Attachment.ToArray() : null);
            target.AttachmentName = source.AttachmentName;
            target.DateCreated = source.DateCreated;
            target.DateChanged = source.DateChanged;
            target.DateDeleted = source.DateDeleted;
        }
    }
}
