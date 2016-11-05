using System;
using System.Linq;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Pages.User
{
    public partial class Projects : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FillProjects();
        }

        private void FillProjects()
        {
            var currentLanguage = LanguageUtil.GetLanguage();

            var entities = (from n in DataContext.LP_Projects
                            where n.DateDeleted == null && (n.Language == currentLanguage || n.Language == null || n.Language == "")
                            orderby n.OrderIndex, n.DateCreated
                            select n).ToList();

            var converter = new ProjectEntityModelConverter(DataContext);

            var models = (from n in entities
                          let m = converter.Convert(n)
                          select m).ToList();

            var model = new ProjectsModel();
            model.List = models;

            projectsControl.Model = model;
        }
    }
}