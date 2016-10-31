using System;
using CITI.EVO.Tools.Extensions;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.ModelToEntity
{
    public class LinkModelEntityConverter : SingleModelConverterBase<LinkModel, LP_Link>
    {
        public LinkModelEntityConverter(PortalDataContext dbContext) : base(dbContext)
        {
        }

        public override LP_Link Convert(LinkModel source)
        {
            var entity = new LP_Link
            {
                ID = Guid.NewGuid(),
                DateCreated = DateTime.Now
            };

            FillObject(entity, source);

            return entity;
        }

        public override void FillObject(LP_Link target, LinkModel source)
        {
            //target.ID = source.ID.Value;
            target.Url = source.Url;
            target.Title = source.Title;
            target.ParentID = source.ParentID;
            target.Description = source.Description;
            target.OrderIndex = source.OrderIndex;

            if (!source.Image.IsNullOrEmpty())
                target.Image = source.Image;
        }
    }
}