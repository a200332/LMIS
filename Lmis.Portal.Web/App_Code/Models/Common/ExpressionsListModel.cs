using System;
using System.Collections.Generic;

namespace Lmis.Portal.Web.Models.Common
{
	[Serializable]
	public class ExpressionsListModel
    {
        public List<ExpressionModel> Expressions { get; set; }
    }
}