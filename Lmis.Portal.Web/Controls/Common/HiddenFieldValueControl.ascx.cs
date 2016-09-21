using System;
using CITI.EVO.Tools.Web.UI.Model.Interfaces;

namespace Lmis.Portal.Web.Controls.Common
{
	public partial class HiddenFieldValueControl : System.Web.UI.UserControl, ISingleValueContainer
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		public Object Value
		{
			get { return hdValue.Value; }
			set { hdValue.Value = Convert.ToString(value); }
		}
	}
}