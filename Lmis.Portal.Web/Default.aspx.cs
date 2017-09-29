using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Utils;
using DevExpress.XtraPrinting.Native;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Models;
using Lmis.Portal.Web.Utils;
using CITI.EVO.Tools.Extensions;

public partial class _Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Request.RawUrl.Contains("Default.aspx"))
        {
            Response.Redirect("~/Default.aspx");
            return;
        }

        lblRealHost.Text = Request.Url.ToString();
        lblRealHost.Visible = !String.IsNullOrWhiteSpace(Request["displayHost"]);

        clDefault.TodaysDate = DateTime.Now;

        FillCategories();
    }
    private void FillCategories()
    {
        var allCategories = DataContext.LP_Categories.Where(n => n.DateDeleted == null).ToList();
        var categoriesDict = allCategories.ToDictionary(n => n.ID);

        lblEconomic.Text = GetGrandParentName("600", categoriesDict);
        lblEducation.Text = GetGrandParentName("800", categoriesDict);
        lblPopulation.Text = GetGrandParentName("500", categoriesDict);
        lblInvestitions.Text = GetGrandParentName("700", categoriesDict);
        //lblReportsClone.Text = GetGrandParentName("", categoriesDict);
        lblLabourMarket.Text = GetGrandParentName("001", categoriesDict);


        //var currentLanguage = LanguageUtil.GetLanguage();

        //var converter = new CategoryEntityModelConverter(DataContext);

        //var categories = (from n in DataContext.LP_Categories
        //                  where n.ParentID == null &&
        //                        n.DateDeleted == null &&
        //                        (n.Language == currentLanguage || n.Language == null || n.Language == "")
        //                  select n).ToList();

        //CategoryUtil.Sort(categories);

        //var models = categories.Select(n => converter.Convert(n)).ToList();

        //var categoriesModel = new CategoriesModel { List = models };
        //mainCategoriesControl.Model = categoriesModel;
    }

    private void FillVideos()
    {
        //var entities = (from n in DataContext.LP_Videos
        //                where n.DateDeleted == null
        //                orderby n.DateCreated descending
        //                select n).ToList();

        //var converter = new VideoEntityModelConverter(DataContext);

        //var models = (from n in entities
        //              let m = converter.Convert(n)
        //              select m).ToList();

        //var model = new VideosModel();
        //model.List = models;

        //videosControl.Model = model;
    }

    protected String GetGrandParentName(String code, IDictionary<Guid, LP_Category> categories)
    {
        var language = LanguageUtil.GetLanguage();

        var child = (from n in categories.Values
                     where n.Number == code && n.DateDeleted == null && (n.Language == language || n.Language == null || n.Language == "")
                     select n).FirstOrDefault();

        var parent = child;
        while (parent != null)
        {
            if (parent.ParentID == null)
                return parent.Name;

            parent = categories.GetValueOrDefault(parent.ParentID.Value);
        }

        return null;
    }
}