using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Helpers;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;
using CITI.EVO.Tools.Extensions;

public partial class Controls_DataDisplay_CategoriesControl : BaseExtendedControl<CategoriesModel>
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
		tvData.DataSource = categoriesModel.List;
		tvData.DataBind();

		ExpandTo();
	}

	protected void tvData_OnSelectedNodeChanged(object sender, EventArgs e)
	{
		var value = tvData.SelectedValue;
		var url = String.Format("~/Handlers/CategoryImage.ashx?CategoryID={0}", value);

		Response.Redirect(url);
	}

	protected void ExpandTo()
	{
		var categoryID = DataConverter.ToNullableGuid(Request["CategoryID"]);
		if (categoryID == null)
			return;

		var nodes = (from n in tvData.GetAllNodes()
					 let m = n.DataItem as CategoryModel
					 where m.ID == categoryID
					 select n);

		foreach (var node in nodes)
		{
			ExpandNode(node);
			//SetSelected(node);

			node.Selected = true;
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
		return String.Format("~/Handlers/CategoryImage.ashx?CategoryID={0}", eval);
	}

	protected void ExpandNode(TreeNode node)
	{
		var parent = node.Parent;

		while (parent != null)
		{
			parent.Expanded = true;
			parent = parent.Parent;
		}
	}

	protected void SetSelected(TreeNode node)
	{
		var parent = node;

		while (parent != null)
		{
			parent.Selected = true;
			parent = parent.Parent;
		}
	}
}