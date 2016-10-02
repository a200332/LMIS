using System;
using Lmis.Portal.Web.Models.Common;

namespace Lmis.Portal.Web.Models
{
	[Serializable]
    public class LogicModel
    {
        public Guid? ID { get; set; }

		public String Name { get; set; }

	    public String Type { get; set; }

		public String Query { get; set; }

		public Guid? SourceID { get; set; }

		public String SourceType { get; set; }

		public ExpressionsLogicModel ExpressionsLogic { get; set; }
    }
}