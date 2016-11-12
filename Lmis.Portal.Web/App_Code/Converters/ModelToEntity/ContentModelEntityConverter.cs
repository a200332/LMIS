using System;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.ModelToEntity
{
    public class ContentModelEntityConverter : SingleModelConverterBase<ContentModel, LP_Content>
    {
        public ContentModelEntityConverter(PortalDataContext dbContext) : base(dbContext)
        {
        }

        public override LP_Content Convert(ContentModel source)
        {
            var entity = new LP_Content
            {
                ID = Guid.NewGuid(),
                DateCreated = DateTime.Now
            };

            FillObject(entity, source);

            return entity;
        }

        public override void FillObject(LP_Content target, ContentModel source)
        {
            //target.ID = source.ID;
            target.Type = source.Type;
            target.Title = source.Title;
            target.Image = source.Image;
            target.FullText = source.FullText;
            target.Language = source.Language;
            target.Description = source.Description;
            target.Attachment = source.Attachment;
            target.AttachmentName = source.AttachmentName;
            //target.DateCreated = source.DateCreated;
            //target.DateChanged = source.DateChanged;
            //target.DateDeleted = source.DateDeleted;
        }
    }
}
