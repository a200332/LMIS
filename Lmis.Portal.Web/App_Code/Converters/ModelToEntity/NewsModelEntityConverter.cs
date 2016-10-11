using System;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.ModelToEntity
{
	public class NewsModelEntityConverter : SingleModelConverterBase<NewsModel, LP_News>
	{
		public NewsModelEntityConverter(PortalDataContext dbContext) : base(dbContext)
		{
		}

		public override LP_News Convert(NewsModel source)
		{
			var entity = new LP_News();
			entity.ID = Guid.NewGuid();
			entity.DateCreated = DateTime.Now;

			FillObject(entity, source);

			return entity;
		}

		public override void FillObject(LP_News target, NewsModel source)
		{
			//target.ID = source.ID.Value;
			//target.Url = source.Url;
			//target.Title = source.Title;
			//target.Description = source.Description;
		}
	}
}