using System;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;
using CITI.EVO.Tools.Extensions;

namespace Lmis.Portal.Web.Converters.ModelToEntity
{
    public class LegislationModelEntityConverter : SingleModelConverterBase<LegislationModel, LP_Legislation>
    {
        public LegislationModelEntityConverter(PortalDataContext dbContext) : base(dbContext)
        {
        }

        public override LP_Legislation Convert(LegislationModel source)
        {
            var entity = new LP_Legislation();
            entity.ID = Guid.NewGuid();
            entity.DateCreated = DateTime.Now;

            FillObject(entity, source);

            return entity;
        }

        public override void FillObject(LP_Legislation target, LegislationModel source)
        {
            //target.ID = source.ID.Value;
            target.Title = source.Title;
            target.Description = source.Description;
            target.Language = source.Language;
            target.OrderIndex = source.OrderIndex;

            if (!source.FileData.IsNullOrEmpty())
            {
                target.FileData = source.FileData;
                target.FileName = source.FileName;
            }
        }
    }
}