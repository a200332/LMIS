using System;
using System.Collections.Generic;
using System.Linq;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;
using System.Web.UI;
using CITI.EVO.Tools.Extensions;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using Lmis.Portal.Web.Entites;
using System.Data;
using System.IO;
using System.Net.Mime;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.BLL;

namespace Lmis.Portal.Web.Controls.DataDisplay
{
    public partial class ReportUnitControl : BaseExtendedControl<ReportUnitModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (PostBackControl == btnExportReportOK)
            {
                btnExportReportOK_OnClick(sender, e);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
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

            if (pnlChartImage.Visible)
            {
                var dataSource = mainGrid.DataSource as DataTable;
                if (dataSource == null)
                    return;

                if (targetType == "PDF" || targetType == "Image")
                {
                    using (var chartImage = GetChartImage())
                    {
                        var fileName = ReportUnitHelper.GetDownloadFileName(targetType);
                        var reportBytes = ReportUnitHelper.GetReportChartBytes(targetType, dataSource, chartImage);

                        SendDownloadFile(fileName, reportBytes);
                    }
                }
                else
                {
                    var fileName = ReportUnitHelper.GetDownloadFileName(targetType);
                    var reportBytes = ReportUnitHelper.GetReportGridBytes(targetType, dataSource);

                    SendDownloadFile(fileName, reportBytes);
                }
            }
            else if (pnlMainGrid.Visible)
            {
                if (targetType == "Image")
                    return;

                var dataSource = mainGrid.DataSource as DataTable;
                if (dataSource == null)
                    return;

                var fileName = ReportUnitHelper.GetDownloadFileName(targetType);
                var reportBytes = ReportUnitHelper.GetReportGridBytes(targetType, dataSource);

                SendDownloadFile(fileName, reportBytes);
            }
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

            var entities = ReportUnitHelper.GetQueries(unitModel, DataContext);

            if (unitModel.Type == "Grid")
            {
                pnlMainGrid.Visible = true;
                pnlChartImage.Visible = false;
                pnlChartCommands.Visible = false;

                var entiry = entities.FirstOrDefault();
                if (entiry != null)
                    BindGridData(entiry);
            }
            else
            {
                pnlMainGrid.Visible = true;
                pnlChartImage.Visible = true;
                pnlChartCommands.Visible = true;

                var entiry = entities.FirstOrDefault();
                if (entiry != null)
                    BindChartData(entiry);
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

        protected void BindChartData(BindingInfoEntity entity)
        {
            lblReportTitle.Text = entity.Name;

            var selReportType = GetSelectedReportTypes().FirstOrDefault();

            var reportType = ReportUnitHelper.GetChartType(selReportType, entity.Type);
            var sqlDs = ReportUnitHelper.CreateDataSource(entity.SqlQuery);

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
                              let v = Convert.ToString(n[xMember])
                              where selXYValues.Contains(v)
                              select n);
            }

            var dataTable = ReportUnitHelper.GetChartDataTable(modelsGrp.Key, yMember, xMember, collection);

            var chartType = reportType.GetValueOrDefault(SeriesChartType.Line);

            FillChartData(dataTable, chartType);
            BindGridData(dataTable);

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

            if (selReportType == "Grid")
            {
                pnlMainGrid.Visible = true;
                pnlChartImage.Visible = false;
            }
            else
            {
                pnlMainGrid.Visible = false;
                pnlChartImage.Visible = true;
            }
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
                    axisY.Minimum = valuesSet.Min - 10;
                    axisY.Maximum = valuesSet.Max + 10;
                }

                mainChart.Series.Add(series);
            }
        }

        protected void BindGridData(BindingInfoEntity entiry)
        {
            var sqlDs = ReportUnitHelper.CreateDataSource(entiry.SqlQuery);

            var dataView = (DataView)sqlDs.Select(new DataSourceSelectArguments());
            if (dataView == null)
                return;

            lblReportTitle.Text = entiry.Name;

            var dataTable = ReportUnitHelper.GetGridDataTable(entiry, dataView.Cast<DataRowView>());

            BindGridData(dataTable);
        }
        protected void BindGridData(DataTable dataTable)
        {
            var keyFields = dataTable.Columns.Cast<DataColumn>().Select(n => n.ColumnName);
            var keyColumns = String.Join(";", keyFields);

            mainGrid.AutoGenerateColumns = (mainGrid.Columns.Count == 0);
            mainGrid.KeyFieldName = keyColumns;
            mainGrid.DataSource = dataTable;
            mainGrid.DataBind();
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

        protected System.Drawing.Image GetChartImage()
        {
            using (var stream = new MemoryStream())
            {
                mainChart.SaveImage(stream, ChartImageFormat.Bmp);
                stream.Seek(0, SeekOrigin.Begin);

                var image = System.Drawing.Image.FromStream(stream);
                return image;
            }
        }

        protected ISet<String> GetSelectedCaptions()
        {
            var values = GetListBoxSelectedValues(lstCaptions).ToHashSet();
            SetListBoxSelectedValues(lstCaptions, values);

            return values;
        }

        protected ISet<String> GetSelectedXYSeries()
        {
            var values = GetListBoxSelectedValues(lstXYSeries).ToHashSet();
            SetListBoxSelectedValues(lstXYSeries, values);

            return values;
        }

        protected ISet<String> GetSelectedReportTypes()
        {
            var values = GetListBoxSelectedValues(lstReportTypes).ToHashSet();
            SetListBoxSelectedValues(lstReportTypes, values);

            return values;
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

        protected void SetListBoxSelectedValues(ListControl listBox, ISet<String> values)
        {
            foreach (ListItem listItem in listBox.Items)
                listItem.Selected = values.Contains(listItem.Value);
        }
    }
}