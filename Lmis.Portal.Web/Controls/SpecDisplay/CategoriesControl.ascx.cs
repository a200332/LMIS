using System;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.SpecDisplay
{
    public partial class CategoriesControl : BaseExtendedControl<SpecsModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnSetModel(object model, Type type)
        {
            var specsModel = model as SpecsModel;
            if (specsModel == null)
                return;

            rptData.DataSource = specsModel.List;
            rptData.DataBind();
        }

        protected String GetSpecUrl(object eval)
        {
            var specID = DataConverter.ToNullableGuid(eval);
            if (specID == null)
                return "#";

            var url = String.Format("~/Pages/Spec/MainSpec.aspx?ID={0}", specID);
            return url;
        }
    }
}