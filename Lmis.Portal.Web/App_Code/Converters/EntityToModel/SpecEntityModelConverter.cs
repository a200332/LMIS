using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.EntityToModel
{
    public class SpecEntityModelConverter : SingleModelConverterBase<LP_Spec, SpecModel>
    {
        public SpecEntityModelConverter(PortalDataContext dbContext) : base(dbContext)
        {
        }

        public override SpecModel Convert(LP_Spec source)
        {
            var target = new SpecModel();
            FillObject(target, source);

            return target;
        }

        public override void FillObject(SpecModel target, LP_Spec source)
        {
            target.ID = source.ID;
            target.ParentID = source.ParentID;
            target.OrderIndex = source.OrderIndex;
            target.Title = source.Title;
            target.FullText = source.FullText;
            target.Language = source.Language;
            target.IsCategory = source.IsCategory;
        }
    }
}