using System;
using System.Linq;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Pages.User
{
    public partial class News : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var newsID = DataConverter.ToNullableGuid(Request["ID"]);
            if (newsID != null)
            {
                var entity = DataContext.LP_News.FirstOrDefault(n => n.ID == newsID);
                if (entity != null)
                {
                    var converter = new NewsEntityModelConverter(DataContext);
                    var model = converter.Convert(entity);

                    newsControl.Model = model;
                }
            }
            else
            {
                var entity = DataContext.LP_News.OrderByDescending(n => n.NewsDate).FirstOrDefault();
                if (entity != null)
                {
                    var url = String.Format("~/Pages/User/News.aspx?ID={0}", entity.ID);
                    Response.Redirect(url);
                }
            }

            FillNews();
        }

        private void FillNews()
        {
            var currentLanguage = LanguageUtil.GetLanguage();

            var entities = (from n in DataContext.LP_News
                            where n.DateDeleted == null //&& (n.Language == currentLanguage || n.Language == null || n.Language == "")
                            orderby n.NewsDate descending
                            select n).ToList();

            var converter = new NewsEntityModelConverter(DataContext);

            var models = (from n in entities
                          let m = converter.Convert(n)
                          select m).ToList();

            var model = new NewsListModel
            {
                List = models
            };

            newsListControl.Model = model;
        }
    }
}