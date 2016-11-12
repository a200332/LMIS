using System;
using System.Linq;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Pages.Spec
{
    public partial class MainSpec : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var currentLanguage = LanguageUtil.GetLanguage();
            var specID = DataConverter.ToNullableGuid(Request["ID"]);

            var spec = DataContext.LP_Specs.FirstOrDefault(n => n.ID == specID);
            if (spec == null)
            {
                var mainSpecs = (from n in DataContext.LP_Specs
                                 where n.DateDeleted == null && 
                                       n.ParentID == null &&
                                       (n.Language == currentLanguage || n.Language == null || n.Language == "")
                                 select n).ToList();

                var converter = new SpecEntityModelConverter(DataContext);

                var models = (from n in mainSpecs
                              let c = converter.Convert(n)
                              select c).ToList();

                var specsModel = new SpecsModel
                {
                    List = models
                };

                categoriesControl.Model = specsModel;

                pnlSpecData.Visible = false;
                pnlSpecCategories.Visible = true;
            }
            else if (spec.IsCategory.GetValueOrDefault())
            {
                var mainSpecs = (from n in spec.Children
                                 where n.DateDeleted == null && (n.Language == currentLanguage || n.Language == null || n.Language == "")
                                 select n).ToList();

                var converter = new SpecEntityModelConverter(DataContext);

                var models = (from n in mainSpecs
                              let c = converter.Convert(n)
                              select c).ToList();

                var specsModel = new SpecsModel
                {
                    List = models
                };

                categoriesControl.Model = specsModel;

                pnlSpecData.Visible = false;
                pnlSpecCategories.Visible = true;
            }
            else
            {
                var converter = new SpecEntityModelConverter(DataContext);
                var model = converter.Convert(spec);

                specControl.Model = model;

                pnlSpecData.Visible = true;
                pnlSpecCategories.Visible = false;
            }
        }
    }
}