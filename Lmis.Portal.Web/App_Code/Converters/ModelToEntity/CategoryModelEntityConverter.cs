using System;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.ModelToEntity
{
    public class CategoryModelEntityConverter : SingleModelConverterBase<CategoryModel, LP_Category>
    {
        public CategoryModelEntityConverter(PortalDataContext dbContext) : base(dbContext)
        {
        }

        public override LP_Category Convert(CategoryModel source)
        {
            var entity = new LP_Category();
            entity.ID = Guid.NewGuid();
            entity.DateCreated = DateTime.Now;

            FillObject(entity, source);

            return entity;
        }

        public override void FillObject(LP_Category target, CategoryModel source)
        {
            //target.ID = source.ID.Value;
            target.Number = source.Number;
            target.Name = source.Name;
            target.ParentID = source.ParentID;
            target.Image = source.Image;
        }
    }
}