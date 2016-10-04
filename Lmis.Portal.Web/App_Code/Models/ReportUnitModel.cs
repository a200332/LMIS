using System;

namespace Lmis.Portal.Web.Models
{
	[Serializable]
	public class ReportUnitModel
	{
		public Guid? ID { get; set; }

		public String Type { get; set; }
		
		public Guid? CategoryID { get; set; }

		public String Name { get; set; }

		public String Description { get; set; }

		public ReportLogicsModel ReportLogics { get; set; }
		
	}
}