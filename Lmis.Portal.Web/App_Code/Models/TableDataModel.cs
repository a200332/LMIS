using System;
using System.Collections.Generic;

namespace Lmis.Portal.Web.Models
{
	[Serializable]
	public class TableDataModel
	{
		public TableModel Table { get; set; }

		public LogicModel Logic { get; set; }
	}
}