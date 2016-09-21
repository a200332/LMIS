using System;
using System.Linq;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Converters.ModelToEntity;

namespace Lmis.Portal.Web.Pages.Management
{
	public partial class AddEditLogic : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			var logicID = DataConverter.ToNullableGuid(Request["LogicID"]);
			if (logicID == null)
				return;

			var entity = DataContext.LP_Logics.FirstOrDefault(n => n.ID == logicID);
			if (entity == null)
				return;

			var converter = new LogicEntityModelConverter(DataContext);
			var model = converter.Convert(entity);

			logicControl.Model = model;
		}

		protected void btnSaveLogic_OnClick(object sender, EventArgs e)
		{
			var logicID = DataConverter.ToNullableGuid(Request["LogicID"]);

			var entity = DataContext.LP_Logics.FirstOrDefault(n => n.ID == logicID);

			var converter = new LogicModelEntityConverter(DataContext);

			var model = logicControl.Model;
			if (entity == null)
			{
				entity = converter.Convert(model);
				DataContext.LP_Logics.InsertOnSubmit(entity);
			}
			else
			{
				converter.FillObject(entity, model);
			}

			DataContext.SubmitChanges();

			Response.Redirect("~/Pages/Management/LogicsList.aspx");
		}
	}
}