using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.EntityToModel
{
    public class CareerEntityModelConverter : SingleModelConverterBase<LP_Career, CareerModel>
    {
        public CareerEntityModelConverter(PortalDataContext dbContext) : base(dbContext)
        {
        }

        public override CareerModel Convert(LP_Career source)
        {
            var target = new CareerModel();
            FillObject(target, source);

            return target;
        }

        public override void FillObject(CareerModel target, LP_Career source)
        {
            target.ID = source.ID;
            target.Title = source.Title;
            target.Description = source.Description;
            target.Language = source.Language;
            target.OrderIndex = source.OrderIndex;
            target.ParentID = source.ParentID;
            target.Url = source.Url;
        }
    }
}