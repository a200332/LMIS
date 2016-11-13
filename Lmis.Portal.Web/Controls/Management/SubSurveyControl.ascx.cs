using System;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.Management
{
    public partial class SubSurveyControl : BaseExtendedControl<SurveyModel>
    {
        protected void Page_Init(object sender, EventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            FillComboBoxes();
        }

        protected override void OnSetModel(object model, Type type)
        {
            FillComboBoxes();
        }

        protected override void OnGetModel(object model, Type type)
        {
            var castedModel = model as SurveyModel;
            if (castedModel == null)
                return;

            castedModel.FileData = fuFileData.FileBytes;
            castedModel.FileName = fuFileData.FileName;
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