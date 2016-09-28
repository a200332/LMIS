using System;
using System.Collections.Generic;

namespace Lmis.Portal.Web.Models
{
	[Serializable]
	public class ReportModel
	{
		public Guid? ID { get; set; }

		public String Type { get; set; }

		public Guid? CategoryID { get; set; }

		public String Name { get; set; }

		public ReportLogicsModel ReportLogics { get; set; }
	}
}