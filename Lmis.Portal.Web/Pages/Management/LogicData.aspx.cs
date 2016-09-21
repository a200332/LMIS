using System;
using System.Linq;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.BLL;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Pages.Management
{
	public partial class LogicData : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			var logicID = DataConverter.ToNullableGuid(Request["LogicID"]);
			var tableID = DataConverter.ToNullableGuid(Request["TableID"]);

			var logic = DataContext.LP_Logics.FirstOrDefault(n => n.ID == logicID);
			if (logic == null)
				return;

			var table = DataContext.LP_Tables.FirstOrDefault(n => n.ID == tableID);
			if (table == null)
				return;

			var logicConverter = new LogicEntityModelConverter(DataContext);
			var tableConverter = new TableEntityModelConverter(DataContext);

			var logicModel = logicConverter.Convert(logic);
			var tableModel = tableConverter.Convert(table);

			var tableDataModel = new TableDataModel
			{
				Table = tableModel,
				Logic = logicModel,
			};

			tableDataControl.Model = tableDataModel;
		}
	}
}