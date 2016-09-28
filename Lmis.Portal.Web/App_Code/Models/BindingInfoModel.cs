using System;

namespace Lmis.Portal.Web.Models
{
	[Serializable]
	public class BindingInfoModel
	{
		public String Caption { get; set; }

		public String Source { get; set; }

		public String Target { get; set; }
	}
}