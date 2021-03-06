﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Comparers;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Entites;
using Lmis.Portal.Web.Models;
using CITI.EVO.Tools.Extensions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Lmis.Portal.DAL.DAL;

namespace Lmis.Portal.Web.BLL
{
    public static class ReportUnitHelper
    {
        public static byte[] GetReportGridBytes(String targetType, DataSet dataSet)
        {
            if (targetType == "PDF")
            {
                using (var stream = new MemoryStream())
                {
                    var pdfDoc = new Document();
                    var writer = PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.Open();

                    foreach (DataTable dataTable in dataSet.Tables)
                    {
                        var table = GetPdfGrid(dataTable);
                        pdfDoc.Add(table);
                    }

                    pdfDoc.Close();

                    return stream.ToArray();
                }
            }

            if (targetType == "Excel")
            {
                return ExcelUtil.ConvertToExcel(dataSet);
            }

            if (targetType == "CSV")
            {
                var dataTable = dataSet.Tables.Cast<DataTable>().FirstOrDefault();
                return ExcelUtil.ConvertToCSV(dataTable);
            }

            return null;
        }

        public static byte[] GetReportChartBytes(String targetType, DataSet dataSet, System.Drawing.Image chartImage)
        {
            if (targetType == "PDF")
            {
                using (var stream = new MemoryStream())
                {
                    var pdfImage = GetPdfImage(chartImage);

                    var pdfDoc = new Document();
                    var writer = PdfWriter.GetInstance(pdfDoc, stream);

                    var fitWidth = pdfDoc.PageSize.Width - (pdfDoc.LeftMargin + pdfDoc.RightMargin);
                    var fitHeight = pdfDoc.PageSize.Height - (pdfDoc.TopMargin + pdfDoc.BottomMargin);

                    pdfImage.ScaleToFit(fitWidth, fitHeight);

                    pdfDoc.Open();
                    pdfDoc.Add(pdfImage);

                    foreach (DataTable dataTable in dataSet.Tables)
                    {
                        var pdfTable = GetPdfGrid(dataTable);
                        pdfDoc.Add(pdfTable);
                    }

                    pdfDoc.Close();

                    return stream.ToArray();
                }
            }

            if (targetType == "Image")
            {
                using (var stream = new MemoryStream())
                {
                    chartImage.Save(stream, ImageFormat.Png);
                    return stream.ToArray();
                }
            }

            return null;
        }

        public static SeriesChartType? GetChartType(String selChartType, String defaultChartType)
        {
            var chartTypeVal = DataConverter.ToNullableEnum<SeriesChartType>(selChartType);
            if (chartTypeVal != null)
                return chartTypeVal.Value;

            SeriesChartType value;
            if (Enum.TryParse(defaultChartType, true, out value))
                return value;

            return null;
        }

        public static IEnumerable<BindingInfoEntity> GetQueries(ReportUnitModel unitModel, PortalDataContext dataContext)
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
                    QueryType = logicModel.Type,
                    SqlQuery = logicModel.Query,
                    Ordered = IsDataOrdered(logicModel),
                    Bindings = list,
                };

                if (logicModel.Type != "Query")
                {
                    var queryGenerator = new QueryGenerator(dataContext, logicModel);
                    entity.SqlQuery = queryGenerator.SelectQuery();
                }

                yield return entity;
            }
        }

        public static DataTable GetChartDataTable(String groupMember, String yMember, String xMember, IEnumerable<DataRowView> collection, bool sortData)
        {
            var logicalComparer = new StringLogicalComparer();
            var comparisonComparer = new ComparisonComparer<Object>((x, y) => Compare(logicalComparer, x, y));

            var dataRowViewsXQuery = (from DataRowView n in collection
                                      let v = n[xMember]
                                      select new
                                      {
                                          Grouper = v,
                                          DataRow = n
                                      });

            if (sortData)
                dataRowViewsXQuery = dataRowViewsXQuery.OrderBy(n => n.Grouper, comparisonComparer);

            var dataRowViewsXLp = dataRowViewsXQuery.ToLookup(n => n.Grouper);
            var verticalColumns = dataRowViewsXLp.Select(n => n.Key).ToList();

            var horizontalColumnsQuery = (from DataRowView n in collection
                                          let v = GetGrouperValue(n, groupMember, yMember)
                                          select v);

            if (sortData)
                horizontalColumnsQuery = horizontalColumnsQuery.OrderBy(n => n, comparisonComparer);

            var horizontalColumns = horizontalColumnsQuery.Distinct().ToList();
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
                                             select new
                                             {
                                                 Grouper = v,
                                                 DataRow = n.DataRow
                                             };

                if (sortData)
                    dataRowViewsGroupQuery = dataRowViewsGroupQuery.OrderBy(n => n.Grouper, comparisonComparer);

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

                    var dbl = values.Sum();
                    if (dbl != null)
                        dbl = Math.Round(dbl.Value, 2);

                    var value = String.Format("{0:0.00}", dbl);
                    dataRow[horizontalColumn] = value;
                }

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        public static DataTable GetGridDataTable(BindingInfoEntity entity, IEnumerable<DataRowView> collection)
        {
            var dataTable = new DataTable(entity.Name);

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
                {
                    var value = dataRowView[pair.Key];

                    var dbl = DataConverter.ToNullableDouble(value);
                    if (dbl != null)
                        value = String.Format("{0:0.00}", Math.Round(dbl.Value, 2));

                    dataRow[pair.Value] = value;
                }

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        private static String GetGrouperValue(DataRowView dataRowView, String groupMemper, String xMember)
        {
            if (String.IsNullOrWhiteSpace(groupMemper))
                return xMember;

            var dataTable = dataRowView.DataView.Table;
            if (!dataTable.Columns.Contains(groupMemper))
                return xMember;

            return Convert.ToString(dataRowView[groupMemper]);
        }

        public static SqlDataSource CreateDataSource(String sqlQuery)
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

        public static String GetDownloadFileName(String targetType)
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

        private static int Compare(IComparer<String> comparer, Object x, Object y)
        {
            var xVal = Convert.ToString(x);
            var yVal = Convert.ToString(y);

            var order = comparer.Compare(xVal, yVal);
            return order;
        }

        private static PdfPTable GetPdfGrid(DataTable dataSource)
        {
            var sylfaenPath = String.Format("{0}\\fonts\\sylfaen.ttf", Environment.GetEnvironmentVariable("SystemRoot"));
            var sylfaen = BaseFont.CreateFont(sylfaenPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            var pdfFont = new Font(sylfaen, 11f, Font.NORMAL, BaseColor.BLACK);


            //var pdfFont = FontFactory.GetFont("Sylfaen", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 11F);
            var localFont = new System.Drawing.Font("Sylfaen", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            var widths = new List<int>();
            var table = new PdfPTable(dataSource.Columns.Count);

            foreach (DataColumn dataColumn in dataSource.Columns)
            {
                var maxWidth = (from n in dataSource.AsEnumerable()
                                let t = n[dataColumn]
                                let w = GetTextWidth(t, localFont)
                                select w).Max() + 1;

                widths.Add(maxWidth);

                var text = HttpUtility.HtmlDecode(dataColumn.ColumnName);
                var cell = new PdfPCell(new Phrase(12, text, pdfFont))
                {
                    BackgroundColor = new BaseColor(System.Drawing.Color.Gainsboro)
                };

                table.AddCell(cell);
            }

            table.SetWidths(widths.ToArray());

            foreach (DataRow dataRow in dataSource.Rows)
            {
                foreach (DataColumn dataColumn in dataSource.Columns)
                {
                    var value = Convert.ToString(dataRow[dataColumn]);
                    var text = HttpUtility.HtmlDecode(value);
                    var cell = new PdfPCell(new Phrase(12, text, pdfFont));

                    table.AddCell(cell);
                }
            }


            return table;
        }

        private static iTextSharp.text.Image GetPdfImage(System.Drawing.Image image)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Png);

                stream.Seek(0, SeekOrigin.Begin);

                var prfImage = iTextSharp.text.Image.GetInstance(image, ImageFormat.Png);
                return prfImage;
            }
        }

        private static bool IsDataOrdered(LogicModel logicModel)
        {
            if (logicModel == null)
                return false;

            if (logicModel.ExpressionsLogic == null)
                return false;

            if (logicModel.ExpressionsLogic.OrderBy == null)
                return false;

            if (logicModel.ExpressionsLogic.OrderBy.Expressions == null)
                return false;

            if (logicModel.ExpressionsLogic.OrderBy.Expressions.Count == 0)
                return false;

            return true;
        }

        private static int GetTextWidth(Object value, System.Drawing.Font font)
        {
            var text = Convert.ToString(value);
            if (String.IsNullOrWhiteSpace(text))
                return 1;

            using (var bmp = new System.Drawing.Bitmap(1, 1))
            {
                using (var graphics = System.Drawing.Graphics.FromImage(bmp))
                {
                    var size = graphics.MeasureString(text, font);
                    return (int)size.Width;
                }
            }
        }

        private static String GetConnectionString()
        {
            var connString = ConfigurationManager.ConnectionStrings["RepositoryConnectionString"];
            return connString.ConnectionString;
        }
    }
}