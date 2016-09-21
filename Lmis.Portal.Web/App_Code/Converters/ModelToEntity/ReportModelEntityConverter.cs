using System;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.ModelToEntity
{
	public class ReportModelEntityConverter : SingleModelConverterBase<ReportModel, LP_Report>
	{
		public ReportModelEntityConverter(PortalDataContext dbContext) : base(dbContext)
		{
		}

		public override LP_Report Convert(ReportModel source)
		{
			var entity = new LP_Report
			{
				ID = Guid.NewGuid(),
				DateCreated = DateTime.Now,
			};

			FillObject(entity, source);

			return entity;
		}

		public override void FillObject(LP_Report target, ReportModel source)
		{
			target.Name = source.Name;
			target.CategoryID = source.CategoryID.Value;
			target.LogicID = source.LogicID.Value;
			target.TableID = source.TableID.Value;
			target.Type = source.Type;
		}
	}
}