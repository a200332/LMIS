using System;
using System.Linq;
using CITI.EVO.Tools.Extensions;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;
using Lmis.Portal.Web.Utils;

namespace Lmis.Portal.Web.Controls.Management
{
    public partial class CategoryControl : BaseExtendedControl<CategoryModel>
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            FillComboBoxes();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnSetModel(object model, Type type)
        {
            var categoryModel = model as CategoryModel;
            if (categoryModel == null)
                return;

            FillComboBoxes();

            cbxCategory.TrySetSelectedValue(categoryModel.ParentID);
            cbxLanguage.TrySetSelectedValue(categoryModel.Language);
        }

        protected void FillComboBoxes()
        {
            if (cbxCategory.DataSource == null)
            {
                var categories = (from n in DataContext.LP_Categories
                                  where n.DateDeleted == null
                                  orderby n.Name
                                  select n).ToList();

                cbxCategory.DataSource = categories;
                cbxCategory.DataBind();
            }

            if (cbxLanguage.DataSource == null)
            {
                var languages = LanguageUtil.GetLanguages();

                cbxLanguage.DataSource = languages;
                cbxLanguage.DataBind();
            }
        }
    }
}