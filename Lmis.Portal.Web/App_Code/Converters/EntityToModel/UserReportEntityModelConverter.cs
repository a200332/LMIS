using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.EntityToModel
{
    public class UserReportEntityModelConverter : SingleModelConverterBase<LP_UserReport, UserReportModel>
    {
        public UserReportEntityModelConverter(PortalDataContext dbContext) : base(dbContext)
        {
        }

        public override UserReportModel Convert(LP_UserReport source)
        {
            var target = new UserReportModel();
            FillObject(target, source);

            return target;
        }

        public override void FillObject(UserReportModel target, LP_UserReport source)
        {
            target.ID = source.ID;
            target.ParentID = source.ParentID;
            target.Title = source.Title;
            target.Description = source.Description;
            target.Language = source.Language;
            target.OrderIndex = source.OrderIndex;
            target.FileName = source.FileName;
            target.FileData = (source.FileData != null ? source.FileData.ToArray() : null);
            target.Image = (source.Image != null ? source.Image.ToArray() : null);
            target.Number = source.Number;
        }
    }
}