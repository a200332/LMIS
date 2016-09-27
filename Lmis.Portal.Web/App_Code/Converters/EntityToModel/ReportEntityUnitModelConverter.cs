using System.Linq;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.EntityToModel
{
	public class ReportEntityUnitModelConverter : SingleModelConverterBase<LP_Report, ReportUnitModel>
	{
		private readonly LogicEntityModelConverter logicConverter;

		public ReportEntityUnitModelConverter(PortalDataContext dbContext) : base(dbContext)
		{
			logicConverter = new LogicEntityModelConverter(dbContext);
		}

		public override ReportUnitModel Convert(LP_Report source)
		{
			var target = new ReportUnitModel();
			FillObject(target, source);

			return target;
		}

		public override void FillObject(ReportUnitModel target, LP_Report source)
		{
			target.ID = source.ID;
			target.Name = source.Name;
			target.Type = source.Type;
			target.CategoryID = source.CategoryID;

			var logics = (from n in source.ReportLogics
						  where n.DateDeleted == null &&
								n.Logic != null
						  select n.Logic);

			var models = logics.Select(n => logicConverter.Convert(n));

			target.Logics = new LogicsModel { List = models.ToList() };
		}
	}
}