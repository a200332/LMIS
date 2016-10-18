using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using CITI.EVO.Tools.Extensions;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Models;
using Lmis.Portal.Web.Models.Common;

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

		private readonly String _querySource;

		private readonly String _sourceType;

		private readonly LogicModel _logicModel;
		private readonly PortalDataContext _dbContext;

		private readonly ExpressionsLogicModel _expressionsLogicModel;

		private readonly ISet<String> _outputColumns;

		private readonly IDictionary<String, SqlDbType> _dbTypes;

		private readonly IDictionary<String, String> _allColumns;
		private readonly IDictionary<String, String> _primaryColumns;

		private readonly IDictionary<String, String> _allColumnsParams;
		private readonly IDictionary<String, String> _primaryColumnsParams;

		public QueryGenerator(PortalDataContext dbContext, LogicModel logicModel)
		{
			_dbContext = dbContext;
			_logicModel = logicModel;
			_sourceType = _logicModel.SourceType;
			_expressionsLogicModel = logicModel.ExpressionsLogic;

			if (_logicModel.SourceType == "Table")
			{
				var table = dbContext.LP_Tables.First(n => n.ID == _logicModel.SourceID);

				var converter = new TableEntityModelConverter(_dbContext);
				var model = converter.Convert(table);

				var columns = model.Columns;
				_querySource = GetCorrectName(model.Name);

				var allColumnsQuery = (from n in columns
									   let m = GetCorrectName(n.Name)
									   select new KeyValuePair<String, String>(n.Name, m));

				_allColumns = allColumnsQuery.ToDictionary();

				var primaryColumnsQuery = (from n in columns
										   where n.IsPrimary
										   let m = GetCorrectName(n.Name)
										   select new KeyValuePair<String, String>(n.Name, m));

				_primaryColumns = primaryColumnsQuery.ToDictionary();

				var dbTypesQuery = (from n in columns
									let t = GetDataType(n.Type)
									let m = GetCorrectName(n.Name)
									select new KeyValuePair<String, SqlDbType>(m, t));

				_dbTypes = dbTypesQuery.ToDictionary();

				_outputColumns = GetOutputs().ToHashSet();

			}
			else if (_logicModel.SourceType == "Logic")
			{
				var logic = dbContext.LP_Logics.First(n => n.ID == _logicModel.SourceID);

				var converter = new LogicEntityModelConverter(_dbContext);
				var model = converter.Convert(logic);

				var queryGen = new QueryGenerator(_dbContext, model);

				var selectQuery = queryGen.SelectQuery(true);

				_querySource = String.Format("({0})", selectQuery);

				var allColumnsQuery = (from n in queryGen.OutputColumns
									   let m = GetCorrectName(n)
									   select new KeyValuePair<String, String>(n, m));

				_allColumns = allColumnsQuery.ToDictionary();

				var dbTypesQuery = (from n in queryGen.OutputColumns
									let m = GetCorrectName(n)
									let t = queryGen.DbTypes[m]
									select new KeyValuePair<String, SqlDbType>(m, t));

				_dbTypes = dbTypesQuery.ToDictionary();

				_outputColumns = GetOutputs().ToHashSet();
			}

			if (_primaryColumns == null || _primaryColumns.Count == 0)
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

		public ISet<String> OutputColumns
		{
			get { return _outputColumns; }
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
			return SelectQuery(false);
		}
		public String SelectQuery(bool noOrders)
		{
			var selectFields = GetSelect().ToList();
			var filterFields = GetFilters().ToList();
			var groupFields = GetGroupers().ToList();
			var orderFields = GetOrders().ToList();

			var queryParts = new List<String>();

			if (selectFields.Count > 0)
			{
				var fields = String.Join(", ", selectFields);
				var select = String.Format(selectFormat, fields, _querySource);

				queryParts.Add(select);
			}
			else
			{
				var names = String.Join(", ", _allColumns.Values);

				var select = String.Format(selectFormat, names, _querySource);
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
				var fields = String.Join(", ", groupFields);
				var groupers = String.Format(groupFormat, fields);

				queryParts.Add(groupers);
			}

			if (!noOrders)
			{
				if (orderFields.Count > 0)
				{
					var fields = String.Join(", ", orderFields);
					var orders = String.Format(orderFormat, fields);

					queryParts.Add(orders);
				}
			}

			var sqlQuery = String.Join(Environment.NewLine, queryParts);
			return sqlQuery;
		}

		public String InsertQuery()
		{
			if (_sourceType != "Table")
				throw new NotSupportedException();

			var @params = _allColumns.Select(n => _allColumnsParams[n.Value]);

			var columnsText = String.Join(", ", _allColumns.Values);
			var valuesText = String.Join(", ", @params);

			var query = String.Format(insertFormat, _querySource, columnsText, valuesText);
			return query;
		}

		public String UpdateQuery()
		{
			if (_sourceType != "Table")
				throw new NotSupportedException();

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
				writer.WriteLine(updateFormat, _querySource, setText);

				if (!String.IsNullOrWhiteSpace(whereText))
					writer.WriteLine(whereFormat, whereText);

				return writer.ToString();
			}
		}

		public String DeleteQuery()
		{
			if (_sourceType != "Table")
				throw new NotSupportedException();

			var whereTexts = (from n in _primaryColumnsParams
							  let m = String.Format("{0} = {1}", n.Key, n.Value)
							  select m);

			var whereText = String.Join(" AND ", whereTexts);

			using (var writer = new StringWriter())
			{
				writer.WriteLine(deleteFormat, _querySource);

				if (!String.IsNullOrWhiteSpace(whereText))
					writer.WriteLine(whereFormat, whereText);

				return writer.ToString();
			}
		}

		public String DeleteAllQuery()
		{
			if (_sourceType != "Table")
				throw new NotSupportedException();

			return String.Format(deleteFormat, _querySource);
		}

		public String TruncateQuery()
		{
			if (_sourceType != "Table")
				throw new NotSupportedException();

			return String.Format(truncateFormat, _querySource);
		}

		private IEnumerable<String> GetFilters()
		{
			return GetExpressions("FilterBy");
		}

		private IEnumerable<String> GetOrders()
		{
			return GetExpressions("OrderBy");
		}

		private IEnumerable<String> GetGroupers()
		{
			return GetExpressions("GroupBy");
		}

		private IEnumerable<String> GetOutputs()
		{
			if (_expressionsLogicModel == null)
				yield break;

			var expressionsListModel = _expressionsLogicModel.Select;
			if (expressionsListModel == null || expressionsListModel.Expressions == null)
				yield break;

			foreach (var expressionModel in expressionsListModel.Expressions)
				yield return expressionModel.Name;
		}

		private IEnumerable<String> GetSelect()
		{
			return GetExpressions("Select");
		}

		private IEnumerable<String> GetExpressions(String type)
		{
			if (type == "Select")
			{
				if (_expressionsLogicModel != null)
					return GetExpressions(_expressionsLogicModel.Select);
			}

			if (type == "GroupBy")
			{
				if (_expressionsLogicModel != null)
					return GetExpressions(_expressionsLogicModel.GroupBy);
			}

			if (type == "OrderBy")
			{
				if (_expressionsLogicModel != null)
					return GetExpressions(_expressionsLogicModel.OrderBy);
			}

			if (type == "FilterBy")
			{
				if (_expressionsLogicModel != null)
					return GetExpressions(_expressionsLogicModel.FilterBy);
			}

			return Enumerable.Empty<String>();
		}
		private IEnumerable<String> GetExpressions(ExpressionsListModel model)
		{
			if (model == null || model.Expressions == null)
				yield break;

			foreach (var exp in model.Expressions)
			{
				if (exp != null && !String.IsNullOrWhiteSpace(exp.Expression))
					yield return exp.Expression;
			}
		}
		private IEnumerable<String> GetExpressions(NamedExpressionsListModel model)
		{
			if (model == null || model.Expressions == null)
				yield break;

			foreach (var exp in model.Expressions)
			{
				if (exp != null && !String.IsNullOrWhiteSpace(exp.Expression))
				{
					if (!String.IsNullOrWhiteSpace(exp.Name))
					{
						var name = GetCorrectName(exp.Name);
						yield return String.Format("{0} AS {1}", exp.Expression, name);
					}
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