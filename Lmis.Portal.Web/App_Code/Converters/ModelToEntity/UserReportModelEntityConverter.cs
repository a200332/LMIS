using System;
using CITI.EVO.Tools.Extensions;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.ModelToEntity
{
    public class UserReportModelEntityConverter : SingleModelConverterBase<UserReportModel, LP_UserReport>
    {
        public UserReportModelEntityConverter(PortalDataContext dbContext) : base(dbContext)
        {
        }

        public override LP_UserReport Convert(UserReportModel source)
        {
            var entity = new LP_UserReport();
            entity.ID = Guid.NewGuid();
            entity.DateCreated = DateTime.Now;

            FillObject(entity, source);

            return entity;
        }

        public override void FillObject(LP_UserReport target, UserReportModel source)
        {
            //target.ID = source.ID.Value;
            target.ParentID = source.ParentID;
            target.Title = source.Title;
            target.Description = source.Description;
            target.Language = source.Language;
            target.OrderIndex = source.OrderIndex;
            target.Number = source.Number;

            if (!source.FileData.IsNullOrEmpty())
            {
                target.FileData = source.FileData;
                target.FileName = source.FileName;
            }

            if (!source.Image.IsNullOrEmpty())
                target.Image = source.Image;
        }
    }
}