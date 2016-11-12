using System;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.SpecDisplay
{
    public partial class SpecControl : BaseExtendedControl<SpecModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnSetModel(object model, Type type)
        {
            var specModel = model as SpecModel;
            if (specModel == null)
                return;

            dvFullText.InnerHtml = specModel.FullText;
        }
    }
}