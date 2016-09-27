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
			target.Type = source.Type;

			foreach (var entity in target.ReportLogics)
				entity.DateDeleted = DateTime.Now;

			if (source.Logics != null && source.Logics.List != null)
			{
				foreach (var logicModel in source.Logics.List)
				{
					var entity = new LP_ReportLogic
					{
						ID = Guid.NewGuid(),
						DateCreated = DateTime.Now,
						ReportID = target.ID,
						LogicID = logicModel.ID,
					};

					target.ReportLogics.Add(entity);
				}
			}
		}
	}
}