using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Utils;

public partial class Admin : System.Web.UI.MasterPage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		foreach (var childLink in GetCurrentUrlLinks(this))
		{
			if (childLink.CssClass == "mcolor")
				childLink.CssClass = "mcolor_active ";
		}
	}

	protected IEnumerable<HyperLink> GetCurrentUrlLinks(Control control)
	{
		var links = (from n in UserInterfaceUtil.TraverseChildren(this)
					 let l = n as HyperLink
					 where l != null
					 select l);

		foreach (var link in links)
		{
			var linkUrl = GetLinkAbsUrl(link);
			if (IsRequestLink(linkUrl))
			{
				yield return link;
			}
		}
	}

	protected bool IsRequestLink(String linkUrl)
	{
		if (linkUrl != null && linkUrl.Split('?').FirstOrDefault() == Request.Path)
		{
			return true;
		}

		return false;
	}

	protected String GetLinkAbsUrl(HyperLink hyperLink)
	{
		return ResolveUrl(hyperLink.NavigateUrl);
	}
}
