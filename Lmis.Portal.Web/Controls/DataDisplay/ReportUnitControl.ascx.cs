using System;
using System.Configuration;
using System.Text.RegularExpressions;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.BLL;
using Lmis.Portal.Web.Models;
using System.Web.UI;
using CITI.EVO.Tools.Extensions;
using System.Web.UI.DataVisualization.Charting;

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

			var sqlQuery = GetQuery(unitModel);

			sqlDs.ConnectionString = GetConnectionString();
			sqlDs.SelectCommand = sqlQuery;
			sqlDs.CacheKeyDependency = sqlQuery.ComputeMd5();
			sqlDs.CacheExpirationPolicy = DataSourceCacheExpiry.Sliding;
			sqlDs.CacheDuration = 900;

			if (unitModel.Type == "Grid")
			{
				pnlChart.Visible = false;
				pnlGrid.Visible = true;

				mainChart.DataSource = null;
				mainGrid.DataSource = sqlDs;
			}
			else
			{
				pnlChart.Visible = true;
				pnlGrid.Visible = false;

				var series = mainChart.Series["MainSeries"];
				series.ChartType = GetChartType(unitModel.Type);

				mainChart.DataSource = sqlDs;
				mainGrid.DataSource = null;
			}
		}

		protected String GetQuery(ReportUnitModel unitModel)
		{
			var tableModel = unitModel.Table;
			var logicModel = unitModel.Logic;

			if (logicModel.Type == "Query")
				return logicModel.Query;

			var queryGenerator = new QueryGenerator(tableModel, logicModel);
			var sqlQuery = queryGenerator.SelectQuery();

			return sqlQuery;
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
	}
}