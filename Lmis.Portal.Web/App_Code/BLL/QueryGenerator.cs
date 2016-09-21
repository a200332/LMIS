using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using CITI.EVO.Tools.Extensions;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.BLL
{
	public class QueryGenerator
	{
		private const String truncateFormat = "TRUNCATE TABLE {0}";
		private const String insertFormat = "INSERT INTO {0}({1}) VALUES ({2})";
		private const String selectFormat = "SELECT {0} FROM {1}";
		private const String updateFormat = "UPDATE {0} SET {1}";
		private const String deleteFormat = "DELETE FROM {0}";

		private const String whereFormat = "WHERE {0}";

		private const String groupFormat = "GROUP BY {0}";
		private const String orderFormat = "ORDER BY {0}";

		private readonly TableModel _tableModel;
		private readonly LogicModel _logicModel;

		private readonly String _tableName;

		private readonly IDictionary<String, SqlDbType> _dbTypes;

		private readonly IDictionary<String, String> _allColumns;
		private readonly IDictionary<String, String> _primaryColumns;

		private readonly IDictionary<String, String> _allColumnsParams;
		private readonly IDictionary<String, String> _primaryColumnsParams;

		public QueryGenerator(TableModel tableModel, LogicModel logicModel)
		{
			_tableModel = tableModel;
			_logicModel = logicModel;

			_tableName = GetCorrectName(_tableModel.Name);

			var dbTypesQuery = (from n in tableModel.Columns
								let t = GetDataType(n.Type)
								let m = GetCorrectName(n.Name)
								select new KeyValuePair<String, SqlDbType>(m, t));

			_dbTypes = dbTypesQuery.ToDictionary();

			var allColumnsQuery = (from n in _tableModel.Columns
								   let m = GetCorrectName(n.Name)
								   select new KeyValuePair<String, String>(n.Name, m));

			_allColumns = allColumnsQuery.ToDictionary();

			var primaryColumnsQuery = (from n in _tableModel.Columns
									   where n.IsPrimary
									   let m = GetCorrectName(n.Name)
									   select new KeyValuePair<String, String>(n.Name, m));

			_primaryColumns = primaryColumnsQuery.ToDictionary();

			if (_primaryColumns.Count == 0)
				_primaryColumns = _allColumns;

			var allParamsQuery = (from n in _allColumns
								  let p = String.Format("@v{0}", n.Key.ComputeCrc16())
								  select new KeyValuePair<String, String>(n.Value, p));

			_allColumnsParams = allParamsQuery.ToDictionary();

			var primaryParamsQuery = (from n in _primaryColumns
									  let p = String.Format("@p{0}", n.Key.ComputeCrc16())
									  select new KeyValuePair<String, String>(n.Value, p));

			_primaryColumnsParams = primaryParamsQuery.ToDictionary();
		}

		public IDictionary<String, SqlDbType> DbTypes
		{
			get { return _dbTypes; }
		}

		public IDictionary<String, String> AllColumns
		{
			get { return _allColumns; }
		}

		public IDictionary<String, String> PrimaryColumns
		{
			get { return _primaryColumns; }
		}

		public IDictionary<String, String> AllColumnsParams
		{
			get { return _allColumnsParams; }
		}

		public IDictionary<String, String> PrimaryColumnsParams
		{
			get { return _primaryColumnsParams; }
		}

		public String SelectQuery()
		{
			var selectFields = GetSelect().ToList();
			var filterFields = GetFilters().ToList();
			var groupFields = GetGroupers().ToList();
			var orderFields = GetOrders().ToList();

			var queryParts = new List<String>();

			if (selectFields.Count > 0)
			{
				var fields = String.Join(", ", selectFields);
				var select = String.Format(selectFormat, fields, _tableName);

				queryParts.Add(select);
			}
			else
			{
				var names = String.Join(", ", _allColumns.Values);

				var select = String.Format(selectFormat, names, _tableName);
				queryParts.Add(select);
			}

			if (filterFields.Count > 0)
			{
				var fields = String.Join(" AND ", filterFields);
				var filters = String.Format(whereFormat, fields);

				queryParts.Add(filters);
			}

			if (groupFields.Count > 0)
			{
				var fields = String.Join(", ", filterFields);
				var groupers = String.Format(groupFormat, fields);

				queryParts.Add(groupers);
			}

			if (orderFields.Count > 0)
			{
				var fields = String.Join(", ", orderFields);
				var orders = String.Format(orderFormat, fields);

				queryParts.Add(orders);
			}

			var sqlQuery = String.Join(Environment.NewLine, queryParts);
			return sqlQuery;
		}

		public String InsertQuery()
		{
			var @params = _allColumns.Select(n => _allColumnsParams[n.Value]);

			var columnsText = String.Join(", ", _allColumns.Values);
			var valuesText = String.Join(", ", @params);

			var query = String.Format(insertFormat, _tableName, columnsText, valuesText);
			return query;
		}

		public String UpdateQuery()
		{
			var setTexts = (from n in _allColumnsParams
							let m = String.Format("{0} = {1}", n.Key, n.Value)
							select m);

			var whereTexts = (from n in _primaryColumnsParams
							  let m = String.Format("{0} = {1}", n.Key, n.Value)
							  select m);

			var setText = String.Join(", ", setTexts);
			var whereText = String.Join(" AND ", whereTexts);

			using (var writer = new StringWriter())
			{
				writer.WriteLine(updateFormat, _tableName, setText);

				if (!String.IsNullOrWhiteSpace(whereText))
					writer.WriteLine(whereFormat, whereText);

				return writer.ToString();
			}
		}

		public String DeleteQuery()
		{
			var whereTexts = (from n in _primaryColumnsParams
							  let m = String.Format("{0} = {1}", n.Key, n.Value)
							  select m);

			var whereText = String.Join(" AND ", whereTexts);

			using (var writer = new StringWriter())
			{
				writer.WriteLine(deleteFormat, _tableName);

				if (!String.IsNullOrWhiteSpace(whereText))
					writer.WriteLine(whereFormat, whereText);

				return writer.ToString();
			}
		}

		public String DeleteAllQuery()
		{
			return String.Format(deleteFormat, _tableName);
		}

		public String TruncateQuery()
		{
			return String.Format(truncateFormat, _tableName);
		}

		private IEnumerable<String> GetFilters()
		{
			if (_logicModel == null ||
				_logicModel.FilterBy == null ||
				_logicModel.FilterBy.Expressions == null)
				yield break;

			foreach (var exp in _logicModel.FilterBy.Expressions)
			{
				if (exp != null && !String.IsNullOrWhiteSpace(exp.Expression))
					yield return exp.Expression;
			}
		}

		private IEnumerable<String> GetOrders()
		{
			if (_logicModel == null ||
				_logicModel.OrderBy == null ||
				_logicModel.OrderBy.Expressions == null)
				yield break;

			foreach (var exp in _logicModel.OrderBy.Expressions)
			{
				if (exp != null && !String.IsNullOrWhiteSpace(exp.Expression))
					yield return exp.Expression;
			}
		}

		private IEnumerable<String> GetGroupers()
		{
			if (_logicModel == null ||
				_logicModel.GroupBy == null ||
				_logicModel.GroupBy.Expressions == null)
				yield break;

			foreach (var exp in _logicModel.GroupBy.Expressions)
			{
				if (!String.IsNullOrWhiteSpace(exp.Name))
					yield return String.Format("{0} AS {1}", exp.Expression, exp.Name);
				else
					yield return exp.Expression;
			}
		}

		private IEnumerable<String> GetSelect()
		{
			if (_logicModel == null ||
				_logicModel.Select == null ||
				_logicModel.Select.Expressions == null)
				yield break;

			foreach (var exp in _logicModel.Select.Expressions)
			{
				if (exp != null && !String.IsNullOrWhiteSpace(exp.Expression))
				{
					if (!String.IsNullOrWhiteSpace(exp.Name))
						yield return String.Format("{0} AS {1}", exp.Expression, exp.Name);
					else
						yield return exp.Expression;
				}
			}
		}

		private String GetCorrectName(String name)
		{
			if (name.StartsWith("[") && name.EndsWith("]"))
				return name;

			return String.Format("[{0}]", name);
		}

		private SqlDbType GetDataType(String dataType)
		{
			if (dataType == "String")
				return SqlDbType.NVarChar;

			if (dataType == "Integer")
				return SqlDbType.BigInt;

			if (dataType == "Float")
				return SqlDbType.Float;

			if (dataType == "DateTime")
				return SqlDbType.DateTime;

			return SqlDbType.Variant;
		}
	}
}