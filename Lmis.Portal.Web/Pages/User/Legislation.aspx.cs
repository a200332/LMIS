using System;
using System.Linq;
using CITI.EVO.Tools.Helpers;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Pages.User
{
    public partial class Legislation : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckDataLanguage();
            FillLegislations();
        }

        private void FillLegislations()
        {
            var parentID = DataConverter.ToNullableGuid(Request["ID"]);

            var currentLanguage = LanguageUtil.GetLanguage();

            var query = from n in DataContext.LP_Legislations
                        where n.DateDeleted == null && (n.Language == currentLanguage || n.Language == null || n.Language == "")
                        select n;

            if (parentID == null)
            {
                query = from n in query
                        where n.ParentID == null
                        select n;
            }
            else
            {
                query = from n in query
                        where n.ParentID == parentID
                        select n;
            }

            query = from n in query
                    orderby n.OrderIndex, n.DateCreated
                    select n;

            var entities = query.ToList();

            var converter = new LegislationEntityModelConverter(DataContext);

            var models = (from n in entities
                          let m = converter.Convert(n)
                          select m).ToList();

            var model = new LegislationsModel();
            model.List = models;

            legislationsControl.Model = model;
        }

        private void CheckDataLanguage()
        {
            var itemID = DataConverter.ToNullableGuid(Request["ID"]);
            if (itemID == null)
                return;

            var entity = DataContext.LP_Legislations.FirstOrDefault(n => n.ID == itemID);
            if (entity == null)
                return;

            if (String.IsNullOrWhiteSpace(entity.Language))
                return;

            var currentLanguage = LanguageUtil.GetLanguage();
            if (!StringComparer.OrdinalIgnoreCase.Equals(entity.Language, currentLanguage))
            {
                if (String.IsNullOrWhiteSpace(entity.Number))
                {
                    var baseUrl = Request.Url.GetLeftPart(UriPartial.Path);
                    Response.Redirect(baseUrl);

                    return;
                }

                var langSpecEntity = (from n in DataContext.LP_Legislations
                                      where n.DateDeleted == null &&
                                            n.Number == entity.Number &&
                                            n.Language == currentLanguage
                                      select n).FirstOrDefault();

                if (langSpecEntity == null)
                {
                    var baseUrl = Request.Url.GetLeftPart(UriPartial.Path);
                    Response.Redirect(baseUrl);
                }
                else
                {
                    var urlHelper = new UrlHelper(Request.Url);
                    urlHelper["ID"] = langSpecEntity.ID;

                    Response.Redirect(urlHelper.ToString());
                }
            }
        }
    }
}