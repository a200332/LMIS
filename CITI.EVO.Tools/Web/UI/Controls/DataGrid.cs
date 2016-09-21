using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace CITI.EVO.Tools.Web.UI.Controls
{
	public class DataGrid : GridView
	{
		protected override void OnPreRender(EventArgs e)
		{
			if (HeaderRow != null)
				HeaderRow.TableSection = TableRowSection.TableHeader;

			base.OnPreRender(e);
		}
	}
}
