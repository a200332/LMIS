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

				BindGridData(entiry);
			}
			else
			{
				pnlChart.Visible = true;
				pnlGrid.Visible = false;

				BindChartData(entities);
			}
		}

		private void BindChartData(IEnumerable<BindingInfoEntity> entities)
		{
			foreach (var entity in entities)
			{
				var chartType = GetChartType(entity.Type);
				var sqlDs = CreateDataSource(entity.SqlQuery);

				var series = CreateSeries(entity.Bindings, sqlDs, chartType);
				mainChart.Series.Add(series);
			}

			mainChart.DataSource = null;
			mainGrid.DataSource = null;
		}

		private void BindGridData(BindingInfoEntity entiry)
		{
			var sqlDs = CreateDataSource(entiry.SqlQuery);

			mainGrid.Columns.Clear();

			foreach (var model in entiry.Bindings)
			{
				var col = new GridViewDataColumn();
				col.Caption = model.Caption;
				col.FieldName = model.Source;

				mainGrid.Columns.Add(col);
			}

			mainGrid.AutoGenerateColumns = (mainGrid.Columns.Count == 0);
			mainGrid.DataSource = sqlDs;

			mainChart.DataSource = null;
		}

		private Series CreateSeries(IEnumerable<BindingInfoModel> models, SqlDataSource sqlDs, SeriesChartType chartType)
		{
			var modelLp = models.ToLookup(n => n.Caption);

			var modelsGrp = modelLp.FirstOrDefault();

			var byTargetLp = modelsGrp.ToLookup(n => n.Target);

			var yMembers = byTargetLp["YMember"];
			var yMember = String.Join(",", yMembers.Select(n => n.Source));

			var xMembers = byTargetLp["XMember"];
			var xMember = String.Join(",", xMembers.Select(n => n.Source));

			var series = new Series(modelsGrp.Key);
			series.YValueMembers = yMember;
			series.XValueMember = xMember;
			series.ChartType = chartType;

			FillValues(series, sqlDs);

			return series;
		}

		private void FillValues(Series series, SqlDataSource sqlDs)
		{
			var dataView = (DataView)sqlDs.Select(new DataSourceSelectArguments());
			var dataTable = dataView.ToTable();

			var yFields = series.YValueMembers.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			var xField = series.XValueMember;

			foreach (DataRow dataRow in dataTable.Rows)
			{
				var dataPoint = new DataPoint();

				var xValue = dataRow[xField];
				var yValues = yFields.Select(n => dataRow[n]);

				dataPoint.SetValueXY(xValue, yValues);

				series.Points.Add(dataPoint);
			}
		}

		protected IEnumerable<BindingInfoEntity> GetQueries(ReportUnitModel unitModel)
		{
			var reportLogicsModel = unitModel.ReportLogics;
			if (reportLogicsModel == null || reportLogicsModel.List == null)
				yield break;

			foreach (var reportLogicModel in reportLogicsModel.List)
			{
				var logicModel = reportLogicModel.Logic;

				var entity = new BindingInfoEntity
				{
					SqlQuery = logicModel.Query,
					Type = reportLogicModel.Type,
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