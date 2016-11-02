using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.EntityToModel
{
    public class LegislationEntityModelConverter : SingleModelConverterBase<LP_Legislation, LegislationModel>
    {
        public LegislationEntityModelConverter(PortalDataContext dbContext) : base(dbContext)
        {
        }

        public override LegislationModel Convert(LP_Legislation source)
        {
            var target = new LegislationModel();
            FillObject(target, source);

            return target;
        }

        public override void FillObject(LegislationModel target, LP_Legislation source)
        {
            target.ID = source.ID;
            target.Title = source.Title;
            target.Description = source.Description;
            target.Language = source.Language;
            target.OrderIndex = source.OrderIndex;
            target.ParentID = source.ParentID;
            target.FileName = source.FileName;
            target.FileData = (source.FileData != null ? source.FileData.ToArray() : null);
        }
    }
}