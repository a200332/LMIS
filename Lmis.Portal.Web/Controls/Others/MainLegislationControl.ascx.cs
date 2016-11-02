using System;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.Others
{
    public partial class MainLegislationControl : BaseExtendedControl<LegislationModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FillComboBoxes();
        }

        protected override void OnSetModel(object model, Type type)
        {
            FillComboBoxes();
        }

        protected void FillComboBoxes()
        {
            if (cbxLanguage.DataSource == null)
            {
                var languages = LanguageUtil.GetLanguages();

                cbxLanguage.DataSource = languages;
                cbxLanguage.DataBind();
            }
        }
    }
}