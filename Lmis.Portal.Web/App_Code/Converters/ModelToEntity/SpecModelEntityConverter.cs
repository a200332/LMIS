using System;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.ModelToEntity
{
    public class SpecModelEntityConverter : SingleModelConverterBase<SpecModel, LP_Spec>
    {
        public SpecModelEntityConverter(PortalDataContext dbContext) : base(dbContext)
        {
        }

        public override LP_Spec Convert(SpecModel source)
        {
            var entity = new LP_Spec();
            entity.ID = Guid.NewGuid();
            entity.DateCreated = DateTime.Now;

            FillObject(entity, source);

            return entity;
        }

        public override void FillObject(LP_Spec target, SpecModel source)
        {
            //target.ID = source.ID;
            target.ParentID = source.ParentID;
            target.OrderIndex = source.OrderIndex;
            target.Title = source.Title;
            target.FullText = source.FullText;
            target.Language = source.Language;
            target.IsCategory = source.IsCategory;
        }
    }
}