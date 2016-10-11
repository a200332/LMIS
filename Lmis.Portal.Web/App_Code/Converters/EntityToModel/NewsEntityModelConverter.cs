using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.EntityToModel
{
	public class NewsEntityModelConverter : SingleModelConverterBase<LP_News, NewsModel>
	{
		public NewsEntityModelConverter(PortalDataContext dbContext) : base(dbContext)
		{
		}

		public override NewsModel Convert(LP_News source)
		{
			var target = new NewsModel();

			FillObject(target, source);

			return target;
		}

		public override void FillObject(NewsModel target, LP_News source)
		{
			//target.ID = source.ID;
			//target.Url = source.Url;
			//target.Title = source.Title;
			//target.Description = source.Description;
		}
	}
}