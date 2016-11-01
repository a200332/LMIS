using System;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;
using CITI.EVO.Tools.Extensions;

namespace Lmis.Portal.Web.Converters.ModelToEntity
{
    public class NewsModelEntityConverter : SingleModelConverterBase<NewsModel, LP_News>
    {
        public NewsModelEntityConverter(PortalDataContext dbContext) : base(dbContext)
        {
        }

        public override LP_News Convert(NewsModel source)
        {
            var entity = new LP_News();
            entity.ID = Guid.NewGuid();
            entity.DateCreated = DateTime.Now;

            FillObject(entity, source);

            return entity;
        }

        public override void FillObject(LP_News target, NewsModel source)
        {
            //target.ID = source.ID;
            target.Title = source.Title;
            target.FullText = source.FullText;
            target.NewsDate = source.NewsDate;
            target.Description = source.Description;

            if (!source.Image.IsNullOrEmpty())
            {
                target.Image = source.Image;
            }

            if (!source.Attachment.IsNullOrEmpty())
            {
                target.Attachment = source.Attachment;
                target.AttachmentName = source.AttachmentName;
            }
        }
    }
}