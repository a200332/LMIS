using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.EntityToModel
{
	public class ReportEntityModelConverter : SingleModelConverterBase<LP_Report, ReportModel>
	{
		public ReportEntityModelConverter(PortalDataContext dbContext) : base(dbContext)
		{
		}

		public override ReportModel Convert(LP_Report source)
		{
			var target = new ReportModel();
			FillObject(target, source);

			return target;
		}

		public override void FillObject(ReportModel target, LP_Report source)
		{
			target.ID = source.ID;
			target.Name = source.Name;
			target.Type = source.Type;
			target.CategoryID = source.CategoryID;
			target.TableID = source.TableID;
			target.LogicID = source.LogicID;
		}
	}
}