using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web
{
	public partial class Site : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			foreach (var childLink in GetCurrentUrlLinks(this))
			{
				if (childLink.CssClass == "mcolor")
					childLink.CssClass = "mcolor_active ";
			}

			FillMainLinks();
		}

		protected void btEngLang_Click(object sender, EventArgs e)
		{
			LanguageUtil.SetLanguage("en-US");
		}

		protected void btGeoLang_Click(object sender, EventArgs e)
		{
			LanguageUtil.SetLanguage("ka-GE");
		}

		private void FillMainLinks()
		{
			var page = Page as BasePage;
			if (page == null)
				return;

			var dbContext = page.DataContext;

			var entities = (from n in dbContext.LP_Links
							where n.DateDeleted == null && n.ParentID == null
							orderby n.DateCreated
							select n).ToList();

			var converter = new LinkEntityModelConverter(dbContext);

			var models = (from n in entities
						  let m = converter.Convert(n)
						  select m).ToList();

			var model = new LinksModel();
			model.List = models;

			mainLinksControl.Model = model;
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
}
