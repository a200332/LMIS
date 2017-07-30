using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.WcfLinq.Helpers;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.DataDisplay
{
    public partial class MainCategoriesControl : BaseExtendedControl<CategoriesModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnSetModel(object model, Type type)
        {
            var categoriesModel = model as CategoriesModel;
            if (categoriesModel == null || categoriesModel.List == null)
                return;

            dtCategories.DataSource = categoriesModel.List;
            dtCategories.DataBind();
        }

        protected String GetImageLink(object eval)
        {
            return String.Format("~/Handlers/GetImage.ashx?Type=Category&ID={0}", eval);
        }

        protected String GetTargetUrl(object eval)
        {
            var url = String.Format("~/Pages/User/Dashboard.aspx?CategoryID={0}", eval);
            return url;
        }
    }
}