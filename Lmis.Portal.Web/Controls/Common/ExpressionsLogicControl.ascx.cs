﻿using System;
using System.Collections.Generic;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models.Common;

namespace Lmis.Portal.Web.Controls.Common
{
	public partial class ExpressionsLogicControl : BaseExtendedControl<ExpressionsLogicModel>
	{
		public IList<String> Fields
		{
			get { return ViewState["Fields"] as List<String>; }
			set
			{
				ViewState["Fields"] = value;
				//selectControl.Fields = value;
				//orderByControl.Fields = value;
				//groupByControl.Fields = value;
				//filterByControl.Fields = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}