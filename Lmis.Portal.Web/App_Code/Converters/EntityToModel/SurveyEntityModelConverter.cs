using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.EntityToModel
{
    public class SurveyEntityModelConverter : SingleModelConverterBase<LP_Survey, SurveyModel>
    {
        public SurveyEntityModelConverter(PortalDataContext dbContext) : base(dbContext)
        {
        }

        public override SurveyModel Convert(LP_Survey source)
        {
            var target = new SurveyModel();
            FillObject(target, source);

            return target;
        }

        public override void FillObject(SurveyModel target, LP_Survey source)
        {
            target.ID = source.ID;
            target.Title = source.Title;
            target.Description = source.Description;
            target.Language = source.Language;
            target.OrderIndex = source.OrderIndex;
            target.FileName = source.FileName;
            target.FileData = (source.FileData != null ? source.FileData.ToArray() : null);
        }
    }
}