using System;
using System.Linq;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.BLL;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Models;
using Lmis.Portal.Web.Utils;

namespace Lmis.Portal.Web.Pages.Management
{
	public partial class TableData : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			UserUtil.GotoLoginIfNoSuperadmin();

			var tableID = DataConverter.ToNullableGuid(Request["TableID"]);

			var table = DataContext.LP_Tables.FirstOrDefault(n => n.ID == tableID);
			if (table == null)
				return;

			var converter = new TableEntityModelConverter(DataContext);
			var model = converter.Convert(table);

			var logic = new LogicModel
			{
				SourceID = model.ID,
				SourceType = "Table",
			};

			var tableDataModel = new TableDataModel
			{
				Table = model,
				Logic = logic
			};

			tableDataControl.Model = tableDataModel;
		}
	}
}