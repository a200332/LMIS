using System;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.ModelToEntity
{
	public class VideoModelEntityConverter : SingleModelConverterBase<VideoModel, LP_Video>
	{
		public VideoModelEntityConverter(PortalDataContext dbContext) : base(dbContext)
		{
		}

		public override LP_Video Convert(VideoModel source)
		{
			var entity = new LP_Video();
			entity.ID = Guid.NewGuid();
			entity.DateCreated = DateTime.Now;

			FillObject(entity, source);

			return entity;
		}

		public override void FillObject(LP_Video target, VideoModel source)
		{
			//target.ID = source.ID.Value;
			target.Url = source.Url;
			target.Title = source.Title;
			target.Description = source.Description;
		}
	}
}