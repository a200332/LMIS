using System;
using Lmis.Portal.Web.Models.Common;

namespace Lmis.Portal.Web.Models
{
    public class LogicModel
    {
        public Guid? ID { get; set; }

		public String Name { get; set; }

	    public String Type { get; set; }

		public String Query { get; set; }

		public NamedExpressionsListModel GroupBy { get; set; }
        public NamedExpressionsListModel Select { get; set; }

        public ExpressionsListModel FilterBy { get; set; }
        public ExpressionsListModel OrderBy { get; set; }
    }
}