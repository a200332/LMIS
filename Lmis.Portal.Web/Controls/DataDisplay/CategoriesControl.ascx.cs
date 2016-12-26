using System;
using System.Collections.Generic;
using System.Linq;
using CITI.EVO.Tools.Extensions;
using CITI.EVO.Tools.Helpers;
using CITI.EVO.Tools.Utils;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Web.ASPxTreeList.Internal;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.DataDisplay
{
    public partial class CategoriesControl : BaseExtendedControl<CategoriesModel>
    {
        public String TargetUrl
        {
            get { return Convert.ToString(ViewState["TargetUrl"]); }
            set { ViewState["TargetUrl"] = value; }
        }

        private ILookup<Guid?, LP_Category> _allCategories;
        protected ILookup<Guid?, LP_Category> AllCategories
        {
            get
            {
                if (_allCategories == null)
                {
                    var query = (from n in DataContext.LP_Categories
                                 where n.DateDeleted == null
                                 select n);

                    _allCategories = query.ToLookup(n => n.ParentID);
                }

                return _allCategories;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnSetModel(object model, Type type)
        {
            var categoriesModel = (CategoriesModel)model;

            tlData.DataSource = categoriesModel.List;
            tlData.DataBind();

            ExpandTo();
        }

        protected void ExpandTo()
        {
            var categoryID = DataConverter.ToNullableGuid(Request["CategoryID"]);
            if (categoryID == null)
                return;

            var nodes = (from n in tlData.GetAllNodes()
                         let m = n.DataItem as CategoryModel
                         where m.ID == categoryID
                         select n);

            foreach (var node in nodes)
            {
                ExpandNode(node);
                //SetSelected(node);

                node.Focus();
            }
        }

        protected String GetReportsLink(object eval)
        {
            var urlHelper = new UrlHelper(TargetUrl);
            urlHelper["CategoryID"] = eval;

            return urlHelper.ToString();
        }

        protected String GetImageLink(object eval)
        {
            return String.Format("~/Handlers/GetImage.ashx?Type=Category&ID={0}", eval);
        }

        protected void ExpandNode(TreeListNode node)
        {
            var parent = node.ParentNode;

            while (parent != null)
            {
                parent.Expanded = true;
                parent = parent.ParentNode;
            }
        }

        protected void SetSelected(TreeListNode node)
        {
            var parent = node;

            while (parent != null)
            {
                parent.Selected = true;
                parent = parent.ParentNode;
            }
        }

        protected String GetShortName(object dataItem)
        {
            var templateDataItem = dataItem as TreeListTemplateDataItem;
            if (templateDataItem == null || templateDataItem.Row == null)
                return null;

            var name = Convert.ToString(templateDataItem.Row.GetValue("Name"));
            var number = Convert.ToString(templateDataItem.Row.GetValue("Number"));

            if (!String.IsNullOrWhiteSpace(number))
                name = String.Format("{0} - {1}", number, name);

            if (name.Length > 30)
            {
                var trimed = name.Substring(0, 27);
                name = String.Format("{0}...", trimed);
            }

            return name;
        }

        protected String GetFullName(object dataItem)
        {
            var templateDataItem = dataItem as TreeListTemplateDataItem;
            if (templateDataItem == null || templateDataItem.Row == null)
                return null;

            var name = Convert.ToString(templateDataItem.Row.GetValue("Name"));
            var number = Convert.ToString(templateDataItem.Row.GetValue("Number"));

            if (!String.IsNullOrWhiteSpace(number))
                name = String.Format("{0} - {1}", number, name);

            return name;
        }

        protected bool GetLinkVisible(object eval)
        {
            var id = DataConverter.ToNullableGuid(eval);
            if (id == null)
                return true;

            var count = AllCategories[id].Count();
            if (count > 0)
                return false;

            return true;
        }

        protected bool GetLabelVisible(object eval)
        {
            var id = DataConverter.ToNullableGuid(eval);
            if (id == null)
                return false;

            var count = AllCategories[id].Count();
            if (count > 0)
                return true;

            return false;
        }
    }
}