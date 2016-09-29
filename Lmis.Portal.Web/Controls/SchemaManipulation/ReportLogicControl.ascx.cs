using System;
using System.Linq;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Extensions;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.BLL;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.SchemaManipulation
{
	public partial class ReportLogicControl : BaseExtendedControl<ReportLogicModel>
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			ApplyViewMode();
			FillComboBoxes();
		}

		protected void cbxTable_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			OnDataChanged();
		}

		protected void cbxLogic_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			OnDataChanged();
		}

		protected void btnEditBinding_OnCommand(object sender, CommandEventArgs e)
		{
			OnDataChanged();
		}

		protected void btnDeleteBinding_OnCommand(object sender, CommandEventArgs e)
		{
			OnDataChanged();
		}

		protected void btnSaveBinding_OnClick(object sender, EventArgs e)
		{
			OnDataChanged();
		}

		protected void ApplyViewMode()
		{
			var parentControl = (from n in UserInterfaceUtil.TraverseParents(this)
						  let m = n as BaseExtendedControl<ReportModel>
						  where m != null
						  select m).FirstOrDefault();

			var parentModel = parentControl.Model;
			if (parentModel.Type == "Grid")
			{
				pnlChartBinding.Visible = false;
				pnlChartBindings.Visible = false;

				pnlGridBinding.Visible = true;
				pnlGridBindings.Visible = true;
			}
			else if (parentModel.Type == "Chart")
			{
				pnlChartBinding.Visible = true;
				pnlChartBindings.Visible = true;

				pnlGridBinding.Visible = false;
				pnlGridBindings.Visible = false;
			}
		}

		protected void FillComboBoxes()
		{
			var tables = DataContext.LP_Tables.Where(n => n.DateDeleted == null);

			cbxTable.DataSource = tables;
			cbxTable.DataBind();

			var selTableID = cbxTable.TryGetGuidValue();
			if (selTableID != null)
			{
				var logics = DataContext.LP_Logics.Where(n => n.DateDeleted == null && n.TableID == selTableID);

				cbxLogic.DataSource = logics;
				cbxLogic.DataBind();
			}

			var selLogicID = cbxLogic.TryGetGuidValue();
			if (selLogicID != null)
			{
				var logic = DataContext.LP_Logics.Single(n => n.ID == selLogicID);

				var converter = new LogicEntityModelConverter(DataContext);
				var model = converter.Convert(logic);

				var queryGen = new QueryGenerator(DataContext, model);
				var columns = queryGen.OutputColumns;

				cbxChartXValue.DataSource = columns;
				cbxChartXValue.DataBind();

				cbxChartYValue.DataSource = columns;
				cbxChartYValue.DataBind();

				cbxGridSource.DataSource = columns;
				cbxGridSource.DataBind();
			}
		}
	}
}