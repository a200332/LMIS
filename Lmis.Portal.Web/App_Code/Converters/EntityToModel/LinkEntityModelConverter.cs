using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.EntityToModel
{
	public class LinkEntityModelConverter : SingleModelConverterBase<LP_Link, LinkModel>
	{
		public LinkEntityModelConverter(PortalDataContext dbContext) : base(dbContext)
		{
		}

		public override LinkModel Convert(LP_Link source)
		{
			var target = new LinkModel();

			FillObject(target, source);

			return target;
		}

		public override void FillObject(LinkModel target, LP_Link source)
		{
			target.ID = source.ID;
			target.Url = source.Url;
			target.Title = source.Title;
			target.Image = source.Image.ToArray();
			target.ParentID = source.ParentID;
			target.Description = source.Description;
		    target.OrderIndex = source.OrderIndex;
		}
	}
}