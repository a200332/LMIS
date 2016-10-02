using System;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Entites
{
	public class ChartBindingEntity
	{
		public String Key { get; set; }

		public String Caption { get; set; }

		public String XValue { get; set; }

		public String YValue { get; set; }

		public BindingInfoModel XBinding { get; set; }

		public BindingInfoModel YBinding { get; set; }

	}
}