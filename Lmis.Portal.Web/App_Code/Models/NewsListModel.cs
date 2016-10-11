using System;
using System.Collections.Generic;

namespace Lmis.Portal.Web.Models
{
	[Serializable]
	public class NewsListModel
	{
		public List<NewsModel> List { get; set; }
	}
}