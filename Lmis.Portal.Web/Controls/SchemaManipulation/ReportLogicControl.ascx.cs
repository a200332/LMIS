using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Extensions;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.BLL;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Entites;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.SchemaManipulation
{
	public partial class ReportLogicControl : BaseExtendedControl<ReportLogicModel>
	{
		public ReportModel ParentModel
		{
			get
			{
				var parentControl = (from n in UserInterfaceUtil.TraverseParents(this)
									 let m = n as BaseExtendedControl<ReportModel>
									 where m != null
									 select m).FirstOrDefault();

				return parentControl.Model;
			}
		}

		public List<BindingInfoModel> Bindings
		{
			get
			{
				var list = ViewState["Bindings"] as List<BindingInfoModel>;
				list = (list ?? new List<BindingInfoModel>());

				return list;
			}
			set
			{
				ViewState["Bindings"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			ApplyViewMode();
			FillComboBoxes();
			FillBindingsGrids();
		}

		protected void Page_PreRender(object sender, EventArgs e)
		{
			FillBindingsGrids();
		}

		protected void cbxTable_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			OnDataChanged();
		}

		protected void cbxLogic_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			OnDataChanged();
		}

		protected void btnEditChartBinding_OnCommand(object sender, CommandEventArgs e)
		{
			OnDataChanged();
		}

		protected void btnDeleteChartBinding_OnCommand(object sender, CommandEventArgs e)
		{
			if (Bindings == null)
			{
				OnDataChanged();
				return;
			}

			var entity = (from n in GetChartBindings()
						  where n.Key == Convert.ToString(e.CommandArgument)
						  select n).FirstOrDefault();

			if (entity != null)
			{
				if (entity.XBinding != null)
					Bindings.Remove(entity.XBinding);

				if (entity.YBinding != null)
					Bindings.Remove(entity.YBinding);
			}

			OnDataChanged();
		}

		protected void btnEditGridBinding_OnCommand(object sender, CommandEventArgs e)
		{
			OnDataChanged();
		}

		protected void btnDeleteGridBinding_OnCommand(object sender, CommandEventArgs e)
		{
			if (Bindings == null)
			{
				OnDataChanged();
				return;
			}

			var entity = (from n in GetGridBindings()
						  where n.Key == Convert.ToString(e.CommandArgument)
						  select n).FirstOrDefault();

			if (entity != null)
				Bindings.Remove(entity.Binding);

			OnDataChanged();
		}

		protected void btnSaveBinding_OnClick(object sender, EventArgs e)
		{
			Bindings = (Bindings ?? new List<BindingInfoModel>());

			if (ParentModel.Type == "Grid")
			{
				var binding = new BindingInfoModel
				{
					ID = Guid.NewGuid(),
					Caption = tbxGridCaption.Text,
					Source = cbxGridSource.TryGetStringValue(),
				};

				Bindings.Add(binding);
			}
			else if (ParentModel.Type == "Chart")
			{
				var xBinding = new BindingInfoModel
				{
					ID = Guid.NewGuid(),
					Caption = cbxChartCaption.TryGetStringValue(),
					Source = cbxChartXValue.TryGetStringValue(),
					Target = "XValue"
				};

				var yBinding = new BindingInfoModel
				{
					ID = Guid.NewGuid(),
					Caption = cbxChartCaption.TryGetStringValue(),
					Source = cbxChartYValue.TryGetStringValue(),
					Target = "YValue"
				};

				Bindings = (Bindings ?? new List<BindingInfoModel>());

				Bindings.Add(xBinding);
				Bindings.Add(yBinding);
			}

			OnDataChanged();
		}

		protected override void OnGetModel(object model, Type type)
		{
			var reportLogicModel = model as ReportLogicModel;
			if (reportLogicModel == null)
				return;

			var logicID = cbxLogic.TryGetGuidValue();
			if (logicID != null)
			{
				var logicEntity = DataContext.LP_Logics.Single(n => n.ID == logicID);

				var converter = new LogicEntityModelConverter(DataContext);
				var logicModel = converter.Convert(logicEntity);

				reportLogicModel.Logic = logicModel;
			}

			var bindingsModel = reportLogicModel.Bindings;
			if (bindingsModel == null)
			{
				bindingsModel = new BindingInfosModel();
				reportLogicModel.Bindings = bindingsModel;
			}

			bindingsModel.List = Bindings;
		}

		protected override void OnSetModel(object model, Type type)
		{
			var reportLogicModel = model as ReportLogicModel;
			if (reportLogicModel == null || reportLogicModel.Bindings == null)
				return;

			cbxLogic.SelectedItem = null;
			cbxTable.SelectedItem = null;

			if (reportLogicModel.Logic != null)
			{
				var logicModel = reportLogicModel.Logic;

				cbxLogic.TrySetSelectedValue(logicModel.ID);
				cbxTable.TrySetSelectedValue(logicModel.SourceID);
			}

			Bindings = reportLogicModel.Bindings.List;
		}

		protected void ApplyViewMode()
		{
			if (ParentModel.Type == "Grid")
			{
				pnlChartBinding.Visible = false;
				pnlChartBindings.Visible = false;
				trChartType.Visible = false;

				pnlGridBinding.Visible = true;
				pnlGridBindings.Visible = true;
			}
			else if (ParentModel.Type == "Chart")
			{
				pnlChartBinding.Visible = true;
				pnlChartBindings.Visible = true;
				trChartType.Visible = true;

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

				cbxChartCaption.DataSource = columns;
				cbxChartCaption.DataBind();

				cbxGridSource.DataSource = columns;
				cbxGridSource.DataBind();
			}
		}

		protected void FillBindingsGrids()
		{
			if (Bindings == null)
				return;

			if (ParentModel.Type == "Grid")
				FillGridBindings();
			else if (ParentModel.Type == "Chart")
				FillChartBindings();
		}

		protected void FillGridBindings()
		{
			var bindings = GetGridBindings();

			gvGridBindings.DataSource = bindings;
			gvGridBindings.DataBind();
		}

		protected void FillChartBindings()
		{
			var bindings = GetChartBindings();

			gvChartBindings.DataSource = bindings;
			gvChartBindings.DataBind();
		}

		private IEnumerable<GridBindingEntity> GetGridBindings()
		{
			var query = (from n in Bindings
						 select new GridBindingEntity
						 {
							 Key = GetDataKey(GetBindingID(n)),
							 Caption = n.Caption,
							 Target = n.Target,
							 Source = n.Source,
							 Binding = n,
						 });

			return query;
		}

		private IEnumerable<ChartBindingEntity> GetChartBindings()
		{
			var comparer = StringComparer.OrdinalIgnoreCase;

			var bindingsLp = Bindings.ToLookup(n => n.Caption, comparer);

			var query = (from n in bindingsLp
						 let lp = n.ToLookup(m => m.Target)

						 let xInfo = lp["XValue"].FirstOrDefault()
						 let yInfo = lp["YValue"].FirstOrDefault()

						 select new ChartBindingEntity
						 {
							 Key = GetDataKey(GetBindingID(xInfo), GetBindingID(yInfo)),
							 Caption = n.Key,
							 XValue = GetBindingSource(xInfo),
							 YValue = GetBindingSource(yInfo),
							 XBinding = xInfo,
							 YBinding = yInfo,
						 });

			return query;
		}

		private String GetDataKey(params Object[] items)
		{
			var text = String.Join(",", items);
			return text.ComputeMd5();
		}

		private Guid? GetBindingID(BindingInfoModel item)
		{
			if (item != null)
				return item.ID;

			return null;
		}

		private String GetBindingSource(BindingInfoModel item)
		{
			if (item != null)
				return item.Source;

			return null;
		}
	}
}