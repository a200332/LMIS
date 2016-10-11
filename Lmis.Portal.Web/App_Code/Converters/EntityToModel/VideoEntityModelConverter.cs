using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.EntityToModel
{
	public class VideoEntityModelConverter : SingleModelConverterBase<LP_Video, VideoModel>
	{
		public VideoEntityModelConverter(PortalDataContext dbContext) : base(dbContext)
		{
		}

		public override VideoModel Convert(LP_Video source)
		{
			var target = new VideoModel();

			FillObject(target, source);

			return target;
		}

		public override void FillObject(VideoModel target, LP_Video source)
		{
			target.ID = source.ID;
			target.Url = source.Url;
			target.Title = source.Title;
			target.Description = source.Description;
		}
	}
}