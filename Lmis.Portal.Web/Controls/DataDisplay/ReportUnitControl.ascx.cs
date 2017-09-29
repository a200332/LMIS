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
using AjaxControlToolkit;
using CITI.EVO.Tools.Helpers;
using CITI.EVO.Tools.Utils;
using CITI.EVO.Tools.Web.UI.Controls;
using Lmis.Portal.Web.BLL;
using CheckBoxList = System.Web.UI.WebControls.CheckBoxList;

namespace Lmis.Portal.Web.Controls.DataDisplay
{
    public partial class ReportUnitControl : BaseExtendedControl<ReportUnitModel>
    {
        public Unit ChartWidth
        {
            get { return mainChart.Width; }
            set { mainChart.Width = value; }
        }

        public Unit ChartHeight
        {
            get { return mainChart.Height; }
            set { mainChart.Height = value; }
        }

        public String ChartCssClass
        {
            get { return pnlChartImage.CssClass; }
            set { pnlChartImage.CssClass = value; }
        }

        public bool EnableFullscreen
        {
            get { return btnFullscreen.Visible; }
            set { btnFullscreen.Visible = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (PostBackControl == btnExportReportOK)
            {
                btnExportReportOK_OnClick(sender, e);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            dvReport.Style["width"] = String.Format("{0}px", ChartWidth.Value + 20);
            dvReport.Style["height"] = String.Format("{0}px", ChartHeight.Value + 100);

            dvReport.Style["overflow"] = "auto";
        }

        protected void mainChart_OnDataBound(object sender, EventArgs e)
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
                var dataSource = reportGridsControl.DataSource as DataSet;
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

                var dataSource = reportGridsControl.DataSource as DataSet;
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
            //mainGrid.DataSource = null;

            dvDescription.InnerHtml = unitModel.Description;
            trDescription.Visible = !String.IsNullOrWhiteSpace(unitModel.Description);

            dvInterpretation.InnerHtml = unitModel.Interpretation;
            trInterpretation.Visible = !String.IsNullOrWhiteSpace(unitModel.Interpretation);

            dvInformationSource.InnerHtml = unitModel.InformationSource;
            trInformationSource.Visible = !String.IsNullOrWhiteSpace(unitModel.InformationSource);

            try
            {
                pnlError.Visible = false;
                pnlMainGrid.Visible = true;
                pnlChartImage.Visible = true;

                BindUnitData(unitModel);
            }
            catch (Exception ex)
            {
                pnlError.Visible = true;
                pnlMainGrid.Visible = false;
                pnlChartImage.Visible = false;

                lblError.Text = Convert.ToString(ex);
            }
        }

        protected void BindUnitData(ReportUnitModel unitModel)
        {
            var entities = ReportUnitHelper.GetQueries(unitModel, DataContext);

            if (unitModel.Type == "Grid")
            {
                pnlMainGrid.Visible = true;
                pnlChartImage.Visible = false;
                tdChartCommands.Visible = false;

                var entiry = entities.FirstOrDefault();
                if (entiry != null)
                    BindGridData(entiry);
            }
            else
            {
                pnlMainGrid.Visible = true;
                pnlChartImage.Visible = true;
                tdChartCommands.Visible = true;

                BindChartData(entities, unitModel.XLabelAngle);
            }

            var url = new UrlHelper("~/Pages/User/ReportFullscreen.aspx");
            url["ReportID"] = unitModel.ID;

            btnFullscreen.NavigateUrl = url.ToEncodedUrl();
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

        protected void BindGridData(BindingInfoEntity entiry)
        {
            var sqlDs = ReportUnitHelper.CreateDataSource(entiry.SqlQuery);

            var dataView = (DataView)sqlDs.Select(new DataSourceSelectArguments());
            if (dataView == null)
                return;

            lblReportTitle.Text = entiry.Name;

            var dataTable = ReportUnitHelper.GetGridDataTable(entiry, dataView.Cast<DataRowView>());

            var dataSet = new DataSet();
            dataSet.Tables.Add(dataTable);

            BindGridData(dataSet);
        }

        protected void BindGridData(DataSet dataSet)
        {
            reportGridsControl.DataSource = dataSet;
            reportGridsControl.DataBind();
        }

        protected void BindChartData(IEnumerable<BindingInfoEntity> entities, int? xLabelAngle)
        {
            var namesSet = new HashSet<String>();

            var xMembersSet = new HashSet<String>();
            var yMembersSet = new HashSet<String>();

            var dataSet = new DataSet();

            foreach (var entity in entities)
            {
                if (entity.Bindings == null)
                    continue;

                var modelLp = entity.Bindings.ToLookup(n => n.Caption);

                var modelsGrp = modelLp.FirstOrDefault();
                if (modelsGrp == null)
                    continue;

                var byTargetLp = modelsGrp.ToLookup(n => n.Target);

                var groupMember = modelsGrp.Key;

                var yMember = byTargetLp["YValue"].Select(n => n.Source).Distinct().FirstOrDefault();
                var xMember = byTargetLp["XValue"].Select(n => n.Source).Distinct().FirstOrDefault();

                var sortData = false;
                if (entity.QueryType == "Logic")
                    sortData = !entity.Ordered;

                var collection = GetCollection(entity, groupMember, xMember);

                var dataTable = ReportUnitHelper.GetChartDataTable(groupMember, yMember, xMember, collection, sortData);
                dataTable.TableName = String.Format("{0}; {1}", xMember, yMember);

                dataSet.Tables.Add(dataTable);

                BindChartData(dataTable, entity.Type, groupMember, xMember, yMember, xLabelAngle);

                namesSet.Add(entity.Name);
                xMembersSet.Add(xMember);
                yMembersSet.Add(yMember);
            }

            BindGridData(dataSet);

            lblReportTitle.Text = String.Join(", ", namesSet);

            var xMembers = String.Join(", ", xMembersSet);
            var yMembers = String.Join(", ", yMembersSet);

            lblXYDescription.Text = String.Format("X - {0}; Y - {1}", xMembers, yMembers);
        }

        protected void BindChartData(DataTable dataTable, String defChartType, String groupMember, String xMember, String yMember, int? xLabelAngle)
        {
            var selReportType = GetSelectedReportTypes().FirstOrDefault();

            var reportType = ReportUnitHelper.GetChartType(selReportType, defChartType);

            var chartType = reportType.GetValueOrDefault(SeriesChartType.Line);

            FillChartData(dataTable, chartType, xLabelAngle);

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

        protected void FillChartData(DataTable dataTable, SeriesChartType chartType, int? xLabelAngle)
        {
            var valuesSet = new SortedSet<double>();

            var columns = dataTable.Columns.Cast<DataColumn>().ToList();
            var xColumn = columns.First();

            foreach (var dataColumn in columns)
            {
                if (dataColumn == xColumn)
                    continue;

                var xLegend = (chartType == SeriesChartType.Pie || chartType == SeriesChartType.Doughnut);

                var series = new Series
                {
                    Name = dataColumn.ColumnName,
                    Label = "#VALY{0,0.##}",
                    ToolTip = "#SERIESNAME #VALX/#VALY{0,0.##}",
                    Legend = "Default",
                    LegendText = (xLegend ? "#SERIESNAME/#VALX" : "#SERIESNAME"),
                    BorderWidth = 3,
                    ChartType = chartType,
                    IsValueShownAsLabel = true,
                };

                series.SmartLabelStyle.Enabled = true;

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var cell = dataRow[dataColumn];
                    var val = DataConverter.ToNullableDouble(cell);

                    var y = val.GetValueOrDefault();
                    var x = dataRow[xColumn];

                    var xNum = DataConverter.ToNullableDouble(x);
                    if (xNum == null)
                    {
                        var text = Convert.ToString(x);
                        text = (text ?? String.Empty);

                        if (text.Length > 25)
                            text = String.Format("{0}...", text.Substring(0, 22));

                        x = text;
                    }

                    series.Points.AddXY(x, y);
                    valuesSet.Add(y);
                }

                mainChart.Series.Add(series);
            }

            var defaultChartArea = mainChart.ChartAreas["Default"];

            var axisX = defaultChartArea.AxisX;
            axisX.IsLabelAutoFit = true;

            var labelX = axisX.LabelStyle;
            labelX.Format = "0,0.00";
            labelX.TruncatedLabels = true;

            if (chartType != SeriesChartType.Bar)
                labelX.Angle = xLabelAngle.GetValueOrDefault();

            var minVal = valuesSet.Min - 10;
            var maxVal = valuesSet.Max + 10;

            var axisY = defaultChartArea.AxisY;
            axisY.IsLabelAutoFit = true;
            axisY.Minimum = Math.Min(axisY.Minimum, minVal);
            axisY.Maximum = Math.Max(axisY.Maximum, maxVal);

            var labelY = axisY.LabelStyle;
            labelY.Format = "0,0.00";
            labelY.TruncatedLabels = true;
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

        protected IEnumerable<DataRowView> GetCollection(BindingInfoEntity entity, String groupMember, String xMember)
        {
            var sqlDs = ReportUnitHelper.CreateDataSource(entity.SqlQuery);

            var dataView = (DataView)sqlDs.Select(new DataSourceSelectArguments());
            if (dataView == null)
                return null;

            if (!String.IsNullOrWhiteSpace(groupMember))
                FillCaptionsList(dataView, groupMember);

            FillXYValuesList(dataView, xMember);

            var selCaptions = GetSelectedCaptions().ToHashSet();
            var selXYValues = GetSelectedXYSeries().ToHashSet();

            var collection = dataView.Cast<DataRowView>();

            if (selCaptions.Count > 0)
            {
                collection = (from n in collection
                              let v = Convert.ToString(n[groupMember])
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

            return collection;
        }

        protected String GetExportTargetType()
        {
            return Request.Form[lstFileTypes.UniqueID];
        }

        protected System.Drawing.Image GetChartImage()
        {
            using (var stream = new MemoryStream())
            {
                mainChart.SaveImage(stream, ChartImageFormat.Png);
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