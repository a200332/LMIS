using System.Web.UI;
using CITI.EVO.Tools.Collections;
using CITI.EVO.Tools.Extensions;
using CITI.EVO.Tools.Helpers;

namespace CITI.EVO.Tools.Web.Bases
{
	public class MasterPageBase : MasterPage
	{
		public new PageBase Page
		{
			get { return base.Page as PageBase; }
		}

		private UrlHelper requestUrl;
		public UrlHelper RequestUrl
		{
			get
			{
				requestUrl = (requestUrl ?? Request.RequestUrl());
				return requestUrl;
			}
		}

		public NameObjectCollection PageSession
		{
			get { return Page.PageSession; }
		}
	}
}