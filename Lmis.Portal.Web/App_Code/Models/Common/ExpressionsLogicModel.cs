using System;

namespace Lmis.Portal.Web.Models.Common
{
	[Serializable]
	public class ExpressionsLogicModel
	{
		public NamedExpressionsListModel GroupBy { get; set; }
		public NamedExpressionsListModel Select { get; set; }

		public ExpressionsListModel FilterBy { get; set; }
		public ExpressionsListModel OrderBy { get; set; }
	}
}