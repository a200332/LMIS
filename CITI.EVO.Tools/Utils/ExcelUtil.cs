﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace CITI.EVO.Tools.Utils
{
	public static class ExcelUtil
	{
		public static byte[] ConvertToExcel(DataSet dataSet)
		{
			ZipConstants.DefaultCodePage = Encoding.UTF8.CodePage;

			using (var stream = new MemoryStream())
			{
				var workbook = new XSSFWorkbook();
				FillWorkbook(workbook, dataSet);
				workbook.Write(stream);

				return stream.ToArray();
			}
		}

		public static void FillWorkbook(IWorkbook workbook, DataSet dataSet)
		{
			foreach (DataTable dataTable in dataSet.Tables)
			{
				var name = String.Format("#{0}", dataTable.TableName);
				var sheet = workbook.CreateSheet(name);

				FillSheet(sheet, dataTable);
			}
		}

		public static void FillSheet(ISheet sheet, DataTable dataTable)
		{
			var headerRow = sheet.CreateRow(0);

			var columns = new Dictionary<String, int>();

			foreach (DataColumn dataColumn in dataTable.Columns)
			{
				var index = columns.Count;
				var name = String.Format("#{0}", dataColumn.ColumnName);

				var cell = headerRow.CreateCell(index);
				cell.SetCellValue(name);

				columns.Add(name, index);
			}

			foreach (DataRow dataRow in dataTable.Rows)
			{
				var excelRow = sheet.CreateRow(sheet.LastRowNum + 1);

				foreach (var pair in columns)
				{
					var value = Convert.ToString(dataRow[pair.Key]);

					var cell = excelRow.CreateCell(pair.Value);
					cell.SetCellValue(value);
				}
			}
		}

		public static DataSet ConvertToDataSet(byte[] bytes)
		{
			var workbooks = ReadExcel(bytes);

			var dataSet = ConvertToDataSet(workbooks);
			return dataSet;
		}

		public static DataSet ConvertToDataSet(IWorkbook workbook)
		{
			var dataSet = new DataSet();

			var dataTables = ConvertToDataTables(workbook);
			foreach (var dataTable in dataTables)
			{
				dataSet.Tables.Add(dataTable);
			}

			return dataSet;
		}

		public static IEnumerable<DataTable> ConvertToDataTables(IWorkbook workbook)
		{
			for (int i = 0; i < workbook.NumberOfSheets; i++)
			{
				var sheet = workbook.GetSheetAt(i);
				if (sheet.SheetName.StartsWith("#"))
				{
					var dataTable = ConvertToDataTable(sheet);
					yield return dataTable;
				}
			}
		}

		public static DataTable ConvertToDataTable(ISheet sheet)
		{
			var dataTable = new DataTable(sheet.SheetName);

			var headerRow = sheet.GetRow(sheet.FirstRowNum);

			var mapping = new Dictionary<int, String>();

			for (int i = headerRow.FirstCellNum; i < headerRow.LastCellNum; i++)
			{
				var cell = headerRow.GetCell(i);

				var value = GetCellValue(cell);
				if (String.IsNullOrWhiteSpace(value) || !value.StartsWith("#"))
					continue;

				dataTable.Columns.Add(value);
				mapping.Add(i, value);
			}

			for (int i = sheet.FirstRowNum + 1; i < sheet.LastRowNum; i++)
			{
				var excelRow = sheet.GetRow(i);
				if (excelRow == null)
					continue;

				var dataRow = dataTable.NewRow();

				for (int j = headerRow.FirstCellNum; j < headerRow.LastCellNum; j++)
				{
					String name;
					if (mapping.TryGetValue(j, out name))
					{
						var cell = excelRow.GetCell(j);
						var value = GetCellValue(cell);

						dataRow[name] = value;
					}
				}

				dataTable.Rows.Add(dataRow);
			}

			return dataTable;
		}

		public static IWorkbook ReadExcel(byte[] bytes)
		{
			ZipConstants.DefaultCodePage = Encoding.UTF8.CodePage;

			using (var stream = new MemoryStream(bytes))
			{
				var workbook = new XSSFWorkbook(stream);
				return workbook;
			}
		}

		public static String GetCellValue(IRow row, int columnIndex)
		{
			var cell = row.GetCell(columnIndex);
			return GetCellValue(cell);
		}

		public static String GetCellValue(ICell cell)
		{
			if (cell != null)
			{
				Object val = null;

				switch (cell.CellType)
				{
					case CellType.BOOLEAN:
						val = cell.BooleanCellValue;
						break;
					case CellType.NUMERIC:
						val = cell.NumericCellValue;
						break;
					case CellType.STRING:
						val = cell.StringCellValue;
						break;
					case CellType.FORMULA:
						val = cell.NumericCellValue;
						break;
				}

				val = (val ?? String.Empty);

				return Convert.ToString(val);
			}

			return String.Empty;
		}
	}
}