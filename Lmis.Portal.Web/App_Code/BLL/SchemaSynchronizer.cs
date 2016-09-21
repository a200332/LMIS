using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using CITI.EVO.Tools.Extensions;
using DdbManager;
using Lmis.Portal.Web.Models;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace Lmis.Portal.Web.BLL
{
	public class SchemaSynchronizer
	{
		private readonly TableModel _model;
		private readonly String _databaseName;

		public SchemaSynchronizer(TableModel model)
		{
			_model = model;
			_databaseName = GetDatabaseName();
		}

		public void CreateNew()
		{
			var connection = GetServerConnection();
			var server = new Server(connection);

			var database = CreateOrGetDb(server, _databaseName);

			DropTable(database, _model.Name);
			Update(database, new Dictionary<String, String>());
		}

		public void Update()
		{
			Update(null);
		}
		public void Update(IDictionary<String, String> renameMapping)
		{
			var connection = GetServerConnection();
			var server = new Server(connection);

			var database = CreateOrGetDb(server, _databaseName);

			Update(database, renameMapping);

			server.Alter();
		}

		private void Update(Database database, IDictionary<String, String> renameMapping)
		{
			renameMapping = (renameMapping ?? new Dictionary<String, String>());

			var table = CreateOrGetTable(database, _model.Name);

			var modelColumns = _model.Columns.Select(n => n.Name).ToHashSet();

			foreach (Column dbColumn in table.Columns)
			{
				String newName;
				if (renameMapping.TryGetValue(dbColumn.Name, out newName))
					dbColumn.Name = newName;
			}

			foreach (Column dbColumn in table.Columns)
			{
				if (!modelColumns.Contains(dbColumn.Name))
					dbColumn.MarkForDrop(true);
			}


			foreach (var columnModel in _model.Columns)
			{
				var dbColumn = CreateOrGetColumn(table, columnModel);
			}

			if (database.State == SqlSmoState.Creating)
				database.Create();
			else
				database.Alter();

			if (table.State == SqlSmoState.Creating)
				table.Create();
			else
				table.Alter();
		}

		private void DropTable(Database database, String tableName)
		{
			if (database.Tables.Contains(tableName))
			{
				var table = database.Tables[tableName];
				table.Drop();
			}
		}

		private Database CreateOrGetDb(Server server, String dbName)
		{
			if (server.Databases.Contains(dbName))
				return server.Databases[dbName];

			var database = new Database(server, dbName);
			server.Databases.Add(database);

			return database;
		}

		private Table CreateOrGetTable(Database database, String tableName)
		{
			if (database.Tables.Contains(tableName))
				return database.Tables[tableName];

			var table = new Table(database, tableName);
			database.Tables.Add(table);

			return table;
		}

		private Column CreateOrGetColumn(Table table, ColumnModel columnModel)
		{
			if (table.Columns.Contains(columnModel.Name))
				return table.Columns[columnModel.Name];

			var dataType = GetDataType(columnModel.Type);
			var column = new Column(table, columnModel.Name, dataType);

			table.Columns.Add(column);

			return column;
		}
		private DataType GetDataType(String dataType)
		{
			if (dataType == "String")
				return DataType.NVarChar(400);

			if (dataType == "Integer")
				return DataType.BigInt;

			if (dataType == "Float")
				return DataType.Float;

			if (dataType == "DateTime")
				return DataType.DateTime;

			return DataType.Variant;
		}

		private ServerConnection GetServerConnection()
		{
			var connString = GetConnectionString();

			var sqlConn = new SqlConnection(connString);
			var srvConn = new ServerConnection(sqlConn);

			return srvConn;
		}

		private String GetConnectionString()
		{
			var connString = ConfigurationManager.ConnectionStrings["RepositoryConnectionString"];
			return connString.ConnectionString;
		}

		private String GetDatabaseName()
		{
			var connString = ConfigurationManager.ConnectionStrings["RepositoryConnectionString"];
			var connBuilder = new SqlConnectionStringBuilder(connString.ConnectionString);

			return connBuilder.InitialCatalog;
		}


	}
}