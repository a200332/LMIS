using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.EntityToModel
{
	public class EBookEntityModelConverter : SingleModelConverterBase<LP_EBook, EBookModel>
	{
		public EBookEntityModelConverter(PortalDataContext dbContext) : base(dbContext)
		{
		}

		public override EBookModel Convert(LP_EBook source)
		{
			var target = new EBookModel();

			FillObject(target, source);

			return target;
		}

		public override void FillObject(EBookModel target, LP_EBook source)
		{
			target.ID = source.ID;
			target.Url = source.Url;
			target.Title = source.Title;
			target.Description = source.Description;
		}
	}
}