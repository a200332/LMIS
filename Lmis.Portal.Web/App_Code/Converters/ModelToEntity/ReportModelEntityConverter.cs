using System;
using System.Linq;
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
            target.Type = source.Type;
            target.Public = source.Public;
            target.Language = source.Language;
            target.XLabelAngle = source.XLabelAngle;
            target.CategoryID = source.CategoryID.Value;
            target.Description = source.Description;
            target.Interpretation = source.Interpretation;
            target.InformationSource = source.InformationSource;

            foreach (var entity in target.ReportLogics)
                entity.DateDeleted = DateTime.Now;

            var converter = new ReportLogicModelEntityConverter(DbContext);

            if (source.ReportLogics != null && source.ReportLogics.List != null)
            {
                var query = source.ReportLogics.List.Select(n => converter.Convert(n));
                target.ReportLogics.AddRange(query);
            }
        }
    }
}