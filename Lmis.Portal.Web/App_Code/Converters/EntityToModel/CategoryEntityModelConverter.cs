using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.EntityToModel
{
	public class CategoryEntityModelConverter : SingleModelConverterBase<LP_Category, CategoryModel>
	{
		public CategoryEntityModelConverter(PortalDataContext dbContext) : base(dbContext)
		{
		}

		public override CategoryModel Convert(LP_Category source)
		{
			var target = new CategoryModel();
			FillObject(target, source);

			return target;
		}

		public override void FillObject(CategoryModel target, LP_Category source)
		{
			target.ID = source.ID;
			target.Number = source.Number;
			target.Name = source.Name;
			target.ParentID = source.ParentID;
            target.OrderIndex = source.OrderIndex;
            target.Image = (source.Image != null ? source.Image.ToArray() : null);
		}
	}
}