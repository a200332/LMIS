using System;
using System.Collections.Generic;
using System.Linq;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.SchemaManipulation
{
	public partial class ReportControl : BaseExtendedControl<ReportModel>
	{
		public ReportLogicsModel ReportLogics
		{
			get { return ViewState["ReportLogics"] as ReportLogicsModel; }
			set { ViewState["ReportLogics"] = value; }
		}

		protected void Page_Init(object sender, EventArgs e)
		{
			FillComboBoxes();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected void Page_PreRender(object sender, EventArgs e)
		{
			reportLogicsControl.Model = ReportLogics;
		}

		protected void btnSaveReportLogic_OnClick(object sender, EventArgs e)
		{
			var reportLogicModel = reportLogicControl.Model;

			ReportLogics = (ReportLogics ?? new ReportLogicsModel());

			ReportLogics.List = (ReportLogics.List ?? new List<ReportLogicModel>());
			ReportLogics.List.Add(reportLogicModel);
		}

		protected void btnNewReportLogic_OnClick(object sender, EventArgs e)
		{
			reportLogicControl.Model = new ReportLogicModel();
			mpeAddEditReportLogic.Show();
		}

		protected void reportLogicControl_OnDataChanged(object sender, EventArgs e)
		{
			mpeAddEditReportLogic.Show();
		}

		protected override void OnGetModel(object model, Type type)
		{
			var reportModel = model as ReportModel;
			if (reportModel == null)
				return;

			reportModel.ReportLogics = ReportLogics;
		}

		protected override void OnSetModel(object model, Type type)
		{
			var reportModel = model as ReportModel;
			if (reportModel == null)
				return;

			ReportLogics = reportModel.ReportLogics;
		}

		protected void FillComboBoxes()
		{
			var categories = DataContext.LP_Categories.Where(n => n.DateDeleted == null);

			cbxCategory.DataSource = categories;
			cbxCategory.DataBind();

			var languages = LanguageUtil.GetLanguages();

			cbxLanguage.DataSource = languages;
			cbxLanguage.DataBind();
		}
	}
}
