using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.EntityToModel
{
	public class ReportEntityUnitModelConverter : SingleModelConverterBase<LP_Report, ReportUnitModel>
	{
		private readonly TableEntityModelConverter tableConverter;
		private readonly LogicEntityModelConverter logicConverter;

		public ReportEntityUnitModelConverter(PortalDataContext dbContext) : base(dbContext)
		{
			tableConverter = new TableEntityModelConverter(dbContext);
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
			target.Table = tableConverter.Convert(source.Table);
			target.Logic = logicConverter.Convert(source.Logic);
		}
	}
}