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

		protected void Page_PreRender(object sender, EventArgs e)
		{
			lnkChartGrid.HRef = String.Format("javascript:showHide('{0}', '{1}');", dvChartGrid.ClientID, dvChartImage.ClientID);
			lnkChartImage.HRef = String.Format("javascript:showHide('{0}', '{1}');", dvChartImage.ClientID, dvChartGrid.ClientID);
		}

		protected void btnCaptionsOK_OnClick(object sender, EventArgs e)
		{

		}

		protected void btnXYSeriesOK_OnClick(object sender, EventArgs e)
		{

		}

		protected void btnExport_OnClick(object sender, EventArgs e)
		{

		}

		protected override void OnSetModel(object model, Type type)
		{
			var unitModel = model as ReportUnitModel;
			if (unitModel == null)
				return;

			mainChart.DataSource = null;
			mainGrid.DataSource = null;

			dvDescription.InnerHtml = unitModel.Description;
			trDescription.Visible = !String.IsNullOrWhiteSpace(unitModel.Description);

			dvInterpretation.InnerHtml = unitModel.Interpretation;
			trInterpretation.Visible = !String.IsNullOrWhiteSpace(unitModel.Interpretation);

			dvInformationSource.InnerHtml = unitModel.InformationSource;
			trInformationSource.Visible = !String.IsNullOrWhiteSpace(unitModel.InformationSource);

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

		protected void BindChartData(BindingInfoEntity entity)
		{
			lblChartTitle.Text = entity.Name;

			var chartType = GetChartType(entity.Type);
			var sqlDs = CreateDataSource(entity.SqlQuery);

			var dataView = (DataView)sqlDs.Select(new DataSourceSelectArguments());
			if (dataView == null)
				return;

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

			FillCaptionsList(dataView, modelsGrp.Key);
			FillXYValuesList(dataView, xMember);

			var selCaptions = GetSelectedCaptions().ToHashSet();
			var selXYValues = GetSelectedXYSeries().ToHashSet();

			var collection = (from DataRowView n in dataView
							  select n);

			if (selCaptions.Count > 0)
			{
				collection = (from n in collection
							  let v = Convert.ToString(n[modelsGrp.Key])
							  where selCaptions.Contains(v)
							  select n);
			}

			if (selXYValues.Count > 0)
			{
				collection = (from n in collection
							  let v = Convert.ToString(n[yMember])
							  where selXYValues.Contains(v)
							  select n);
			}

			mainChart.DataBindCrossTable(collection, modelsGrp.Key, xMember, yMember, String.Empty);

			foreach (var series in mainChart.Series)
			{
				series.Legend = "Default";
				series.BorderWidth = 2;
				series.ChartType = chartType;
				series.IsValueShownAsLabel = true;
				series.ToolTip = "#SERIESNAME #VALX/#VALY";
				series.Palette = ChartColorPalette.BrightPastel;
			}

			lblXYDescription.Text = String.Format("X - {0}, Y - {1}", xMember, yMember);
			//var defaultTitle = mainChart.Titles["Default"];
			//if (defaultTitle != null)
			//	defaultTitle.Text = entity.Name;

			//var leftTitle = mainChart.Titles["Left"];
			//if (leftTitle != null)
			//	leftTitle.Text = xMember;

			//var bottomTitle = mainChart.Titles["Bottom"];
			//if (bottomTitle != null)
			//	bottomTitle.Text = yMember;

			mainChart.ApplyPaletteColors();

			BindChartGrid(collection, modelsGrp.Key, xMember, yMember);
		}

		protected void BindGridData(BindingInfoEntity entiry)
		{
			var sqlDs = CreateDataSource(entiry.SqlQuery);

			mainGrid.Columns.Clear();

			lblGridTitle.Text = entiry.Name;

			var keyFields = entiry.Bindings.Select(n => n.Source);
			var keyColumns = String.Join(";", keyFields);

			foreach (var model in entiry.Bindings)
			{
				var col = new GridViewDataColumn
				{
					Caption = model.Caption,
					FieldName = model.Source,
				};

				mainGrid.Columns.Add(col);
			}

			mainGrid.AutoGenerateColumns = (mainGrid.Columns.Count == 0);
			mainGrid.KeyFieldName = keyColumns;
			mainGrid.DataSource = sqlDs;
		}

		protected void BindChartGrid(IEnumerable<DataRowView> collection, String groupMember, String yMember, String xMember)
		{
			var dataTable = GetChartDataTable(groupMember, yMember, xMember, collection);

			chartGrid.DataSource = dataTable;
			chartGrid.DataBind();
		}

		protected DataTable GetGridDataTable(BindingInfoEntity entity, IEnumerable<DataRowView> collection)
		{
			var dataTable = new DataTable("Data");

			var columns = new Dictionary<String, String>();

			foreach (var model in entity.Bindings)
			{
				dataTable.Columns.Add(model.Caption);
				columns.Add(model.Source, model.Caption);
			}

			foreach (var dataRowView in collection)
			{
				var dataRow = dataTable.NewRow();

				foreach (var pair in columns)
					dataRow[pair.Value] = dataRowView[pair.Key];

				dataTable.Rows.Add(dataRow);
			}

			return dataTable;
		}

		protected DataTable GetChartDataTable(String groupMember, String yMember, String xMember, IEnumerable<DataRowView> collection)
		{
			var dataRowViewsYQuery = (from DataRowView n in collection
									  let v = n[yMember]
									  orderby v
									  select new
									  {
										  Grouper = v,
										  DataRow = n
									  });

			var dataRowViewsYLp = dataRowViewsYQuery.ToLookup(n => n.Grouper);

			var xColumns = dataRowViewsYLp.Select(n => n.Key).ToList();

			var yColumnsQuery = (from DataRowView n in collection
								 let v = Convert.ToString(n[groupMember])
								 orderby v
								 select v).Distinct();

			var yColumns = yColumnsQuery.ToList();
			yColumns.Insert(0, yMember);

			var dataTable = new DataTable();
			foreach (var yColumn in yColumns)
			{
				dataTable.Columns.Add(yColumn).AllowDBNull = true;
			}

			foreach (var xColumn in xColumns)
			{
				var dataRow = dataTable.NewRow();
				dataRow[yMember] = Convert.ToString(xColumn);

				var dataRowViewsYGrp = dataRowViewsYLp[xColumn];

				var dataRowViewsGroupQuery = from n in dataRowViewsYGrp
											 let d = n.DataRow
											 let v = d[groupMember]
											 orderby v
											 select new
											 {
												 Grouper = v,
												 DataRow = d
											 };

				var dataRowViewsGroupLp = dataRowViewsGroupQuery.ToLookup(n => n.Grouper);

				foreach (var yColumn in yColumns)
				{
					if (yColumn == yMember)
						continue;

					var dataRowViewsGroupGrp = dataRowViewsGroupLp[yColumn];

					var values = (from n in dataRowViewsGroupGrp
								  let v = n.DataRow[xMember]
								  let m = DataConverter.ToNullableDecimal(v)
								  where m != null
								  select m);

					var value = values.Sum();
					dataRow[yColumn] = value;
				}

				dataTable.Rows.Add(dataRow);
			}

			return dataTable;
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
					Name = unitModel.Name,
					Type = reportLogicModel.Type,
					SqlQuery = logicModel.Query,
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

		protected void FillCaptionsList(DataView dataView, String member)
		{
			FillFilterListBox(lstCaptions, dataView, member);
		}

		protected void FillXYValuesList(DataView dataView, String member)
		{
			FillFilterListBox(lstXYSeries, dataView, member);
		}

		protected void FillFilterListBox(CheckBoxList listBox, DataView dataView, String member)
		{
			var values = (from DataRowView n in dataView
						  let v = n[member]
						  orderby v
						  select v).Distinct().ToList();

			listBox.DataSource = values;
			listBox.DataBind();

			var selValues = GetListBoxSelectedValues(listBox).ToHashSet();

			foreach (ListItem listItem in listBox.Items)
				listItem.Selected = selValues.Contains(listItem.Value);
		}

		protected IEnumerable<String> GetSelectedCaptions()
		{
			return GetListBoxSelectedValues(lstCaptions);
		}

		protected IEnumerable<String> GetSelectedXYSeries()
		{
			return GetListBoxSelectedValues(lstXYSeries);
		}

		protected IEnumerable<String> GetListBoxSelectedValues(CheckBoxList listBox)
		{
			var values = (from n in Request.Form.AllKeys
						  where !String.IsNullOrWhiteSpace(n) &&
								n.StartsWith(listBox.UniqueID)
						  let m = Request.Form[n]
						  select m);

			return values;
		}

		protected SeriesChartType GetChartType(String type)
		{
			SeriesChartType value;
			if (Enum.TryParse(type, true, out value))
				return value;

			return SeriesChartType.Line;
		}

		protected SqlDataSource CreateDataSource(String sqlQuery)
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

		protected String GetConnectionString()
		{
			var connString = ConfigurationManager.ConnectionStrings["RepositoryConnectionString"];
			return connString.ConnectionString;
		}
	}
}