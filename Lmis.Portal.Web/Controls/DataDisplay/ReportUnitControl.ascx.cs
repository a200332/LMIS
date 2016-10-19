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
using System.Drawing;
using System.IO;
using System.Net.Mime;
using CITI.EVO.Tools.Utils;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = System.Drawing.Font;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace Lmis.Portal.Web.Controls.DataDisplay
{
	public partial class ReportUnitControl : BaseExtendedControl<ReportUnitModel>
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			var eventTarget = Request.Form["__EVENTTARGET"];
			if (eventTarget == btnExportReportOK.ClientID)
			{
				btnExportReportOK_OnClick(sender, e);
			}
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

		protected void btnExportReportOK_OnClick(object sender, EventArgs e)
		{
			var targetType = GetExportTargetType();

			if (pnlGrid.Visible)
			{
				if (targetType == "PDF" || targetType == "Image")
					return;

				var dataSource = mainGrid.DataSource as DataTable;
				if (dataSource == null)
					return;

				var fileName = GetDownloadFileName(targetType);
				var reportBytes = GetReportGridBytes(targetType, dataSource);

				SendDownloadFile(fileName, reportBytes);
			}
			else if (pnlChart.Visible)
			{
				if (targetType == "PDF" || targetType == "Image")
				{
					var fileName = GetDownloadFileName(targetType);
					var reportBytes = GetReportChartBytes(targetType);

					SendDownloadFile(fileName, reportBytes);
				}
				else
				{
					var dataSource = chartGrid.DataSource as DataTable;
					if (dataSource == null)
						return;

					var fileName = GetDownloadFileName(targetType);
					var reportBytes = GetReportGridBytes(targetType, dataSource);

					SendDownloadFile(fileName, reportBytes);
				}
			}
		}

		protected void SendDownloadFile(String fielName, byte[] bytes)
		{
			var disposition = new ContentDisposition
			{
				Inline = true,
				FileName = fielName
			};

			Response.Clear();
			Response.Buffer = true;
			Response.ContentType = "application/octet-stream";
			Response.AddHeader("Content-Disposition", disposition.ToString());

			Response.BinaryWrite(bytes);
			Response.End();
		}

		protected String GetDownloadFileName(String targetType)
		{
			if (targetType == "Excel")
				return String.Format("report_{0:dd.MM.yyyy}.xlsx", DateTime.Now);

			if (targetType == "CSV")
				return String.Format("report_{0:dd.MM.yyyy}.csv", DateTime.Now);

			if (targetType == "PDF")
				return String.Format("report_{0:dd.MM.yyyy}.pdf", DateTime.Now);

			if (targetType == "Image")
				return String.Format("report_{0:dd.MM.yyyy}.png", DateTime.Now);

			throw new Exception();
		}

		protected byte[] GetReportChartBytes(String targetType)
		{
			if (targetType == "PDF")
			{
				using (var stream = new MemoryStream())
				{

					var image = GetPdfImage();
					var table = GetPdfGrid();

					var pdfDoc = new iTextSharp.text.Document();
					var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, stream);

					var fitWidth = pdfDoc.PageSize.Width - (pdfDoc.LeftMargin + pdfDoc.RightMargin);
					var fitHeight = pdfDoc.PageSize.Height - (pdfDoc.TopMargin + pdfDoc.BottomMargin);

					image.ScaleToFit(fitWidth, fitHeight);

					pdfDoc.Open();

					pdfDoc.Add(image);
					pdfDoc.Add(table);

					pdfDoc.Close();

					return stream.ToArray();
				}
			}

			if (targetType == "Image")
			{
				using (var stream = new MemoryStream())
				{
					mainChart.SaveImage(stream, ChartImageFormat.Png);
					return stream.ToArray();
				}
			}

			return null;
		}

		protected iTextSharp.text.Image GetPdfImage()
		{
			using (var imgStream = new MemoryStream())
			{
				mainChart.SaveImage(imgStream, ChartImageFormat.Png);
				imgStream.Seek(0, SeekOrigin.Begin);

				var image = iTextSharp.text.Image.GetInstance(imgStream);
				return image;
			}
		}

		protected PdfPTable GetPdfGrid()
		{
			var pdfFont = FontFactory.GetFont("Segoe UI", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 11F);
			var localFont = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);

			var dataSource = mainGrid.DataSource as DataTable;
			if (dataSource == null)
			{
				dataSource = chartGrid.DataSource as DataTable;
				if (dataSource == null)
					return null;
			}

			var widths = new List<int>();
			var table = new PdfPTable(dataSource.Columns.Count);

			foreach (DataColumn dataColumn in dataSource.Columns)
			{
				var maxWidth = (from n in dataSource.AsEnumerable()
								let t = n[dataColumn]
								let w = GetTextWidth(t, localFont)
								select w).Max() + 1;

				widths.Add(maxWidth);

				var text = Server.HtmlDecode(dataColumn.ColumnName);
				var cell = new PdfPCell(new iTextSharp.text.Phrase(12, text, pdfFont))
				{
					BackgroundColor = new iTextSharp.text.BaseColor(Color.Gainsboro)
				};

				table.AddCell(cell);
			}

			table.SetWidths(widths.ToArray());

			foreach (DataRow dataRow in dataSource.Rows)
			{
				foreach (DataColumn dataColumn in dataSource.Columns)
				{
					var value = Convert.ToString(dataRow[dataColumn]);
					var text = Server.HtmlDecode(value);
					var cell = new PdfPCell(new iTextSharp.text.Phrase(12, text, pdfFont));

					table.AddCell(cell);
				}
			}


			return table;
		}

		private int GetTextWidth(Object value, Font font)
		{
			var text = Convert.ToString(value);
			if (String.IsNullOrWhiteSpace(text))
				return 1;

			using (var bmp = new Bitmap(1, 1))
			{
				using (var graphics = Graphics.FromImage(bmp))
				{
					var size = graphics.MeasureString(text, font);
					return (int)size.Width;
				}
			}
		}

		protected byte[] GetReportGridBytes(String targetType, DataTable dataSource)
		{
			if (targetType == "Excel")
			{
				if (String.IsNullOrWhiteSpace(dataSource.TableName))
					dataSource.TableName = "Sheet 1";

				return ExcelUtil.ConvertToExcel(dataSource);
			}

			if (targetType == "CSV")
			{
				return ExcelUtil.ConvertToCSV(dataSource);
			}

			return null;
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
				pnlGrid.Visible = true;
				pnlChart.Visible = false;
				pnlChartCommands.Visible = false;

				var entiry = entities.FirstOrDefault();
				if (entiry != null)
					BindGridData(entiry);
			}
			else
			{
				pnlGrid.Visible = false;
				pnlChart.Visible = true;
				pnlChartCommands.Visible = true;

				var entiry = entities.FirstOrDefault();
				if (entiry != null)
					BindChartData(entiry);
			}
		}


		protected void BindChartData(BindingInfoEntity entity)
		{
			lblReportTitle.Text = entity.Name;

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

			if (!String.IsNullOrWhiteSpace(modelsGrp.Key))
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

			var dataTable = GetChartDataTable(modelsGrp.Key, yMember, xMember, collection);

			FillChartData(dataTable, chartType);
			BindChartGrid(dataTable);

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
		}

		protected void FillChartData(DataTable dataTable, SeriesChartType chartType)
		{
			var valuesSet = new SortedSet<double>();

			var columns = dataTable.Columns.Cast<DataColumn>().ToList();
			var yColumn = columns.First();

			foreach (var dataColumn in columns)
			{
				if (dataColumn == yColumn)
					continue;

				var series = new Series
				{
					Name = dataColumn.ColumnName,
					Legend = "Default",
					BorderWidth = 2,
					ChartType = chartType,
					IsValueShownAsLabel = true,
					ToolTip = "#SERIESNAME #VALX/#VALY{0.00}",
					Label = "#VALY{0.00}",
				};

				foreach (DataRow dataRow in dataTable.Rows)
				{
					var y = DataConverter.ToNullableDouble(dataRow[dataColumn]).GetValueOrDefault();
					var x = dataRow[yColumn];

					series.Points.AddXY(x, y);
					valuesSet.Add(y);
				}

				var mainChartArea = mainChart.ChartAreas["MainChartArea"];
				if (mainChartArea != null)
				{
					var axisY = mainChartArea.AxisY;

					axisY.LabelStyle.Format = "0.00";
					axisY.Minimum = valuesSet.Min - 1;
					axisY.Maximum = valuesSet.Max + 1;
				}

				mainChart.Series.Add(series);
			}
		}


		protected void BindGridData(BindingInfoEntity entiry)
		{
			var sqlDs = CreateDataSource(entiry.SqlQuery);

			mainGrid.Columns.Clear();

			lblReportTitle.Text = entiry.Name;

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

		protected void BindChartGrid(DataTable dataTable)
		{
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
			var dataRowViewsXQuery = (from DataRowView n in collection
									  let v = n[xMember]
									  orderby v
									  select new
									  {
										  Grouper = v,
										  DataRow = n
									  });

			var dataRowViewsXLp = dataRowViewsXQuery.ToLookup(n => n.Grouper);
			var verticalColumns = dataRowViewsXLp.Select(n => n.Key).ToList();

			var horizontalColumnsQuery = (from DataRowView n in collection
										  let v = GetGrouperValue(n, groupMember, yMember)
										  orderby v
										  select v).Distinct();

			var horizontalColumns = horizontalColumnsQuery.ToList();
			horizontalColumns.Insert(0, xMember);

			var dataTable = new DataTable();
			foreach (var horizontalColumn in horizontalColumns)
				dataTable.Columns.Add(horizontalColumn).AllowDBNull = true;

			foreach (var verticalColumn in verticalColumns)
			{
				var dataRow = dataTable.NewRow();
				dataRow[xMember] = Convert.ToString(verticalColumn);

				var dataRowViewsGroupQuery = from n in dataRowViewsXLp[verticalColumn]
											 let v = GetGrouperValue(n.DataRow, groupMember, yMember)
											 orderby v
											 select new
											 {
												 Grouper = v,
												 DataRow = n.DataRow
											 };

				var dataRowViewsGroupLp = dataRowViewsGroupQuery.ToLookup(n => n.Grouper);

				foreach (var horizontalColumn in horizontalColumns)
				{
					if (horizontalColumn == xMember)
						continue;

					var dataRowViewsGroupGrp = dataRowViewsGroupLp[horizontalColumn];

					var values = (from n in dataRowViewsGroupGrp
								  let v = n.DataRow[yMember]
								  let m = DataConverter.ToNullableDouble(v)
								  where m != null
								  select m);

					var value = values.Sum();
					dataRow[horizontalColumn] = value;
				}

				dataTable.Rows.Add(dataRow);
			}

			return dataTable;
		}


		protected String GetGrouperValue(DataRowView dataRowView, String groupMemper, String xMember)
		{
			if (String.IsNullOrWhiteSpace(groupMemper))
				return xMember;

			var dataTable = dataRowView.DataView.Table;
			if (!dataTable.Columns.Contains(groupMemper))
				return xMember;

			return Convert.ToString(dataRowView[groupMemper]);
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

		protected String GetExportTargetType()
		{
			return Request.Form[lstFileTypes.UniqueID];
		}

		protected IEnumerable<String> GetSelectedCaptions()
		{
			return GetListBoxSelectedValues(lstCaptions);
		}

		protected IEnumerable<String> GetSelectedXYSeries()
		{
			return GetListBoxSelectedValues(lstXYSeries);
		}

		protected IEnumerable<String> GetSelectedChartTypes()
		{
			return GetListBoxSelectedValues(lstChartTypes);
		}

		protected IEnumerable<String> GetListBoxSelectedValues(ListControl listBox)
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
			var selChartType = GetSelectedChartTypes().FirstOrDefault();

			var chartTypeVal = DataConverter.ToNullableEnum<SeriesChartType>(selChartType);
			if (chartTypeVal != null)
				return chartTypeVal.Value;

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