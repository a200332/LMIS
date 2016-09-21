using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.EntityToModel
{
	public class ColumnEntityModelConverter : SingleModelConverterBase<LP_Column, ColumnModel>
	{
		public ColumnEntityModelConverter(PortalDataContext dbContext) : base(dbContext)
		{
		}

		public override ColumnModel Convert(LP_Column source)
		{
			var model = new ColumnModel();
			FillObject(model, source);

			return model;
		}

		public override void FillObject(ColumnModel target, LP_Column source)
		{
			target.ID = source.ID;
			target.Name = source.Name;
			target.Type = source.Type;
			target.TableID = source.TableID;
		}
	}
}