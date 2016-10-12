using System;
using System.Linq;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Converters.ModelToEntity;
using Lmis.Portal.Web.Models;
using Lmis.Portal.Web.Utils;

namespace Lmis.Portal.Web.Pages.Management
{
	public partial class AddEditReport : BasePage
	{
		public Guid? ReportID
		{
			get { return DataConverter.ToNullableGuid(Request["ReportID"]); }
		}

		public Guid? CategoryID
		{
			get { return DataConverter.ToNullableGuid(Request["CategoryID"]); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			UserUtil.GotoLoginIfNoSuperadmin();

			if (!IsPostBack)
			{
				var model = new ReportModel { CategoryID = CategoryID };

				var entity = DataContext.LP_Reports.FirstOrDefault(n => n.ID == ReportID);
				if (entity != null)
				{
					var converter = new ReportEntityModelConverter(DataContext);
					model = converter.Convert(entity);
				}

				reportControl.Model = model;
			}

		}

		protected void btnSaveReport_OnClick(object sender, EventArgs e)
		{
			var model = reportControl.Model;

			var converter = new ReportModelEntityConverter(DataContext);

			var entity = DataContext.LP_Reports.FirstOrDefault(n => n.ID == model.ID);
			if (entity == null)
			{
				entity = converter.Convert(model);
				DataContext.LP_Reports.InsertOnSubmit(entity);
			}
			else
			{
				converter.FillObject(entity, model);
			}

			DataContext.SubmitChanges();

			var url = String.Format("~/Pages/Management/ReportsList.aspx?CategoryID={0}", CategoryID);
			Response.Redirect(url);
		}

		protected void btnCancelReport_OnClick(object sender, EventArgs e)
		{
			var url = String.Format("~/Pages/Management/ReportsList.aspx?CategoryID={0}", CategoryID);
			Response.Redirect(url);
		}
	}
}