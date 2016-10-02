using System;
using System.Collections.Generic;

namespace Lmis.Portal.Web.Models.Common
{
	[Serializable]
	public class NamedExpressionsListModel
    {
        public List<NamedExpressionModel> Expressions { get; set; }
    }
}