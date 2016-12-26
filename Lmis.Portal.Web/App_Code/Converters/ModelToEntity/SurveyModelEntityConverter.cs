using System;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;
using CITI.EVO.Tools.Extensions;

namespace Lmis.Portal.Web.Converters.ModelToEntity
{
    public class SurveyModelEntityConverter : SingleModelConverterBase<SurveyModel, LP_Survey>
    {
        public SurveyModelEntityConverter(PortalDataContext dbContext) : base(dbContext)
        {
        }

        public override LP_Survey Convert(SurveyModel source)
        {
            var entity = new LP_Survey
            {
                ID = Guid.NewGuid(),
                DateCreated = DateTime.Now
            };

            FillObject(entity, source);

            return entity;
        }

        public override void FillObject(LP_Survey target, SurveyModel source)
        {
            //target.ID = source.ID.Value;
            target.ParentID = source.ParentID;
            target.Url = source.Url;
            target.Title = source.Title;
            target.Description = source.Description;
            target.Language = source.Language;
            target.OrderIndex = source.OrderIndex;

            if (!source.FileData.IsNullOrEmpty())
            {
                target.FileData = source.FileData;
                target.FileName = source.FileName;
            }

            if (!source.Image.IsNullOrEmpty())
                target.Image = source.Image;
        }
    }
}