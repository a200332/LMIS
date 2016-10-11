using System;
using System.Linq;
using CITI.EVO.Tools.Helpers;
using CITI.EVO.Tools.Utils;
using DevExpress.Web.ASPxTreeList;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.DataDisplay
{
	public partial class CategoriesControl : BaseExtendedControl<CategoriesModel>
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		public String TargetUrl
		{
			get { return Convert.ToString(ViewState["TargetUrl"]); }
			set { ViewState["TargetUrl"] = value; }
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
	}
}