using System;
using CITI.EVO.Tools.Extensions;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.ModelToEntity
{
    public class CareerModelEntityConverter : SingleModelConverterBase<CareerModel, LP_Career>
    {
        public CareerModelEntityConverter(PortalDataContext dbContext) : base(dbContext)
        {
        }

        public override LP_Career Convert(CareerModel source)
        {
            var entity = new LP_Career
            {
                ID = Guid.NewGuid(),
                DateCreated = DateTime.Now
            };

            FillObject(entity, source);

            return entity;
        }

        public override void FillObject(LP_Career target, CareerModel source)
        {
            //target.ID = source.ID.Value;
            target.Title = source.Title;
            target.Description = source.Description;
            target.Language = source.Language;
            target.ParentID = source.ParentID;
            target.OrderIndex = source.OrderIndex;
            target.Url = source.Url;
            target.Number = source.Number;

            if (!source.Image.IsNullOrEmpty())
                target.Image = source.Image;
        }
    }
}