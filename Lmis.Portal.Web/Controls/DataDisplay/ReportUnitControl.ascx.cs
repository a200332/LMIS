using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.BLL;
using Lmis.Portal.Web.Models;
using System.Web.UI;
using CITI.EVO.Tools.Extensions;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using DevExpress.Web;
using Lmis.Portal.Web.Entites;
using System.Data;
using CITI.EVO.Tools.Utils;

namespace Lmis.Portal.Web.Controls.DataDisplay
{
	public partial class ReportUnitControl : BaseExtendedControl<ReportUnitModel>
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected override void OnSetModel(object model, Type type)
		{
			var unitModel = model as ReportUnitModel;
			if (unitModel == null)
				return;

			var entities = GetQueries(unitModel);

			if (unitModel.Type == "Grid")
			{
				pnlChart.Visible = false;
				pnlGrid.Visible = true;

				var entiry = entities.FirstOrDefault();
				if (entiry != null)
					BindGridData(entiry);
			}
			else
			{
				pnlChart.Visible = true;
				pnlGrid.Visible = false;

				var entiry = entities.FirstOrDefault();
				if (entiry != null)
					BindChartData(entiry);
			}
		}

		private void BindChartData(BindingInfoEntity entity)
		{
			var chartType = GetChartType(entity.Type);
			var sqlDs = CreateDataSource(entity.SqlQuery);

			if (entity.Bindings == null)
				return;

			var modelLp = entity.Bindings.ToLookup(n => n.Caption);

			var modelsGrp = modelLp.FirstOrDefault();
			if (modelsGrp == null)
				return;

			var byTargetLp = modelsGrp.ToLookup(n => n.Target);

			var yMembers = byTargetLp["YValue"];
			var yMember = String.Join(",", yMembers.Select(n => n.Source));

			var xMembers = byTargetLp["XValue"];
			var xMember = String.Join(",", xMembers.Select(n => n.Source));

			var dataView = (DataView)sqlDs.Select(new DataSourceSelectArguments());
			if (dataView == null)
				return;

			var selCaption = (from n in Request.Form.AllKeys
							  where n.StartsWith(cbxCaption.ClientID)
							  let m = Request.Form[n]
							  select m).FirstOrDefault();

			var selXYValue = (from n in Request.Form.AllKeys
							  where n.StartsWith(cbxXYSeries.ClientID)
							  let m = Request.Form[n]
							  select m).FirstOrDefault();

			var captions = (from DataRowView n in dataView
							let v = n[modelsGrp.Key]
							orderby v
							select v).Distinct().ToList();

			captions.Insert(0, String.Empty);

			cbxCaption.DataSource = captions;
			cbxCaption.DataBind();

			cbxCaption.TrySetSelectedValue(selCaption);

			var xyValues = (from DataRowView n in dataView
							let v = n[xMember]
							orderby v
							select v).Distinct().ToList();

			xyValues.Insert(0, String.Empty);

			cbxXYSeries.DataSource = xyValues;
			cbxXYSeries.DataBind();

			cbxXYSeries.TrySetSelectedValue(selXYValue);

			var collection = (from DataRowView n in dataView
							  select n);

			if (!String.IsNullOrWhiteSpace(selCaption))
			{
				collection = (from n in collection
							  let v = Convert.ToString(n[modelsGrp.Key])
							  where v == selCaption
							  select n);
			}

			if (!String.IsNullOrWhiteSpace(selXYValue))
			{
				collection = (from n in collection
							  let v = Convert.ToString(n[yMember])
							  where v == selXYValue
							  select n);
			}

			mainChart.DataBindCrossTable(collection, modelsGrp.Key, xMember, yMember, String.Empty, PointSortOrder.Ascending);

			foreach (var series in mainChart.Series)
			{
				series.Legend = "Default";
				series.BorderWidth = 2;
				series.ChartType = chartType;
				series.IsValueShownAsLabel = true;
				series.ToolTip = "#SERIESNAME #VALX/#VALY";
				series.Palette = ChartColorPalette.BrightPastel;
			}

			mainChart.ApplyPaletteColors();

			mainChart.DataSource = null;
			mainGrid.DataSource = null;
		}

		private void BindGridData(BindingInfoEntity entiry)
		{
			var sqlDs = CreateDataSource(entiry.SqlQuery);

			mainGrid.Columns.Clear();

			foreach (var model in entiry.Bindings)
			{
				var col = new GridViewDataColumn
				{
					Caption = model.Caption,
					FieldName = model.Source
				};

				mainGrid.Columns.Add(col);
			}

			mainGrid.AutoGenerateColumns = (mainGrid.Columns.Count == 0);
			mainGrid.DataSource = sqlDs;

			mainChart.DataSource = null;
		}

		protected IEnumerable<BindingInfoEntity> GetQueries(ReportUnitModel unitModel)
		{
			var reportLogicsModel = unitModel.ReportLogics;
			if (reportLogicsModel == null || reportLogicsModel.List == null)
				yield break;

			foreach (var reportLogicModel in reportLogicsModel.List)
			{
				var logicModel = reportLogicModel.Logic;

				var list = (List<BindingInfoModel>)null;

				var bindings = reportLogicModel.Bindings;
				if (bindings == null || bindings.List == null)
					list = new List<BindingInfoModel>();
				else
					list = bindings.List;

				var entity = new BindingInfoEntity
				{
					SqlQuery = logicModel.Query,
					Type = reportLogicModel.Type,
					Bindings = list,
				};

				if (logicModel.Type != "Query")
				{
					var queryGenerator = new QueryGenerator(DataContext, logicModel);
					entity.SqlQuery = queryGenerator.SelectQuery();
				}

				yield return entity;
			}
		}

		private SeriesChartType GetChartType(String type)
		{
			SeriesChartType value;
			if (Enum.TryParse(type, true, out value))
				return value;

			return SeriesChartType.Line;
		}

		private SqlDataSource CreateDataSource(String sqlQuery)
		{
			var sqlDs = new SqlDataSource
			{
				ConnectionString = GetConnectionString(),
				SelectCommand = sqlQuery,
				CacheKeyDependency = sqlQuery.ComputeMd5(),
				CacheExpirationPolicy = DataSourceCacheExpiry.Sliding,
				CacheDuration = 900,
				EnableCaching = true,
			};

			return sqlDs;
		}

		private String GetConnectionString()
		{
			var connString = ConfigurationManager.ConnectionStrings["RepositoryConnectionString"];
			return connString.ConnectionString;
		}
	}
}