using System;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.ModelToEntity
{
    public class SurveyModelEntityConverter : SingleModelConverterBase<SurveyModel, LP_Survey>
    {
        public SurveyModelEntityConverter(PortalDataContext dbContext) : base(dbContext)
        {
        }

        public override LP_Survey Convert(SurveyModel source)
        {
            var entity = new LP_Survey();
            entity.ID = Guid.NewGuid();
            entity.DateCreated = DateTime.Now;

            FillObject(entity, source);

            return entity;
        }

        public override void FillObject(LP_Survey target, SurveyModel source)
        {
            //target.ID = source.ID.Value;
            target.Title = source.Title;
            target.Description = source.Description;
            target.Language = source.Language;
            target.OrderIndex = source.OrderIndex;
            target.FileData = source.FileData;
            target.FileName = source.FileName;
        }
    }
}