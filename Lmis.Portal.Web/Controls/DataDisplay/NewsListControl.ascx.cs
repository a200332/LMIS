using System;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.DataDisplay
{
    public partial class NewsListControl : BaseExtendedControl<NewsListModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnSetModel(object model, Type type)
        {
            var newsListModel = model as NewsListModel;
            if (newsListModel == null)
                return;

            rptItems.DataSource = newsListModel.List;
            rptItems.DataBind();
        }

        protected String GetNewsUrl(object eval)
        {
            var itemID = DataConverter.ToNullableGuid(eval);
            if (itemID == null)
                return "#";

            var url = String.Format("~/Pages/User/News.aspx?ID={0}", itemID);
            return url;
        }
    }
}