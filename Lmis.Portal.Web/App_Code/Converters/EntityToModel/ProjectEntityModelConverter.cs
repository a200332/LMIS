using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.EntityToModel
{
    public class ProjectEntityModelConverter : SingleModelConverterBase<LP_Project, ProjectModel>
    {
        public ProjectEntityModelConverter(PortalDataContext dbContext) : base(dbContext)
        {
        }

        public override ProjectModel Convert(LP_Project source)
        {
            var target = new ProjectModel();
            FillObject(target, source);

            return target;
        }

        public override void FillObject(ProjectModel target, LP_Project source)
        {
            target.ID = source.ID;
            target.Title = source.Title;
            target.Description = source.Description;
            target.Language = source.Language;
            target.OrderIndex = source.OrderIndex;
            target.FileName = source.FileName;
            target.FileData = (source.FileData != null ? source.FileData.ToArray() : null);
        }
    }
}