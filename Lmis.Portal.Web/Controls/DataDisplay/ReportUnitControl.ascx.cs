using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.BLL;
using Lmis.Portal.Web.Models;
using System.Web.UI;
using CITI.EVO.Tools.Extensions;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

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

			var sqlQueries = GetQueries(unitModel);

			if (unitModel.Type == "Grid")
			{
				var sqlQuery = sqlQueries.FirstOrDefault();

				var sqlDs = CreateDataSource(sqlQuery);

				pnlChart.Visible = false;
				pnlGrid.Visible = true;

				mainChart.DataSource = null;
				mainGrid.DataSource = sqlDs;
			}
			else
			{
				pnlChart.Visible = true;
				pnlGrid.Visible = false;

				foreach (var sqlQuery in sqlQueries)
				{
					var chartType = GetChartType(unitModel.Type);
					var sqlDs = CreateDataSource(sqlQuery);

					var series = CreateSeries(sqlDs, chartType);
					mainChart.Series.Add(series);
				}

				mainChart.DataSource = null;
				mainGrid.DataSource = null;
			}
		}

		private Series CreateSeries(SqlDataSource sqlDs, SeriesChartType chartType)
		{
			var series = new Series("");
			series.YValueMembers = "";
			series.XValueMember = "";
			series.ChartType = chartType;

			FillValues(series, sqlDs);

			return series;
		}

		private void FillValues(Series series, SqlDataSource sqlDs)
		{

		}

		protected IEnumerable<String> GetQueries(ReportUnitModel unitModel)
		{
			var logicsModel = unitModel.Logics;
			if (logicsModel == null || logicsModel.List == null)
				yield break;

			foreach (var logicModel in logicsModel.List)
			{
				var sqlQuery = logicModel.Query;

				if (logicModel.Type != "Query")
				{
					var queryGenerator = new QueryGenerator(DataContext, logicModel);
					sqlQuery = queryGenerator.SelectQuery();
				}

				yield return sqlQuery;
			}
		}

		private String GetConnectionString()
		{
			var connString = ConfigurationManager.ConnectionStrings["RepositoryConnectionString"];
			return connString.ConnectionString;
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
				CacheDuration = 900
			};

			return sqlDs;
		}
	}
}