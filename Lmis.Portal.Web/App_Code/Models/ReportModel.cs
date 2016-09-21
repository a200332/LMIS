using System;

namespace Lmis.Portal.Web.Models
{
	[Serializable]
	public class ReportModel
	{
		public Guid? ID { get; set; }

		public String Type { get; set; }

		public Guid? LogicID { get; set; }

		public Guid? CategoryID { get; set; }

		public Guid? TableID { get; set; }

		public String Name { get; set; }
	}
}