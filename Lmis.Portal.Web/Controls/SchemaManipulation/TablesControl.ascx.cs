using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Utils;
using DevExpress.Web.ASPxTreeList.Internal;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Common;
using Lmis.Portal.Web.Entites;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.SchemaManipulation
{
	public partial class TablesControl : BaseExtendedControl<TablesModel>
	{
		public event EventHandler<GenericEventArgs<Guid>> EditTable;
		protected virtual void OnEditTable(Guid value)
		{
			if (EditTable != null)
				EditTable(this, new GenericEventArgs<Guid>(value));
		}

		public event EventHandler<GenericEventArgs<Guid>> DeleteTable;
		protected virtual void OnDeleteTable(Guid value)
		{
			if (DeleteTable != null)
				DeleteTable(this, new GenericEventArgs<Guid>(value));
		}

		public event EventHandler<GenericEventArgs<Guid>> AddNewColumn;
		protected virtual void OnAddNewColumn(Guid value)
		{
			if (AddNewColumn != null)
				AddNewColumn(this, new GenericEventArgs<Guid>(value));
		}

		public event EventHandler<GenericEventArgs<Guid>> EditColumn;
		protected virtual void OnEditColumn(Guid value)
		{
			if (EditColumn != null)
				EditColumn(this, new GenericEventArgs<Guid>(value));
		}

		public event EventHandler<GenericEventArgs<Guid>> DeleteColumn;
		protected virtual void OnDeleteColumn(Guid value)
		{
			if (DeleteColumn != null)
				DeleteColumn(this, new GenericEventArgs<Guid>(value));
		}

		public event EventHandler<GenericEventArgs<Guid>> SyncTable;
		protected virtual void OnSyncTable(Guid value)
		{
			if (SyncTable != null)
				SyncTable(this, new GenericEventArgs<Guid>(value));
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override void OnSetModel(object model, Type type)
		{
			var tablesModel = (TablesModel)model;
			if (tablesModel.List != null)
			{
				var list = GetEntities(tablesModel.List).ToList();

				tlData.DataSource = list;
				tlData.DataBind();
			}
		}

		protected void btnEdit_OnCommand(object sender, CommandEventArgs e)
		{
			var command = Convert.ToString(e.CommandArgument);

			var match = Regex.Match(command, @"^(?<Id>.+?)/(?<Type>.+?)$");

			var entityId = DataConverter.ToNullableGuid(match.Groups["Id"].Value);
			var typeName = match.Groups["Type"].Value;

			if (entityId == null)
				return;

			if (typeName == "table")
				OnEditTable(entityId.Value);
			else if (typeName == "column")
				OnEditColumn(entityId.Value);
		}

		protected void btnDelete_OnCommand(object sender, CommandEventArgs e)
		{
			var command = Convert.ToString(e.CommandArgument);

			var match = Regex.Match(command, @"^(?<Id>.+?)/(?<Type>.+?)$");

			var entityId = DataConverter.ToNullableGuid(match.Groups["Id"].Value);
			var typeName = match.Groups["Type"].Value;

			if (entityId == null)
				return;

			if (typeName == "table")
				OnDeleteTable(entityId.Value);
			else if (typeName == "column")
				OnDeleteColumn(entityId.Value);
		}

		protected void btnAddChild_OnCommand(object sender, CommandEventArgs e)
		{
			var command = Convert.ToString(e.CommandArgument);

			var match = Regex.Match(command, @"^(?<Id>.+?)/(?<Type>.+?)$");

			var entityId = DataConverter.ToNullableGuid(match.Groups["Id"].Value);
			var typeName = match.Groups["Type"].Value;

			if (entityId == null)
				return;

			if (typeName == "Table")
				OnAddNewColumn(entityId.Value);
		}

		protected void btnSynch_OnCommand(object sender, CommandEventArgs e)
		{
			var command = Convert.ToString(e.CommandArgument);

			var match = Regex.Match(command, @"^(?<Id>.+?)/(?<Type>.+?)$");

			var entityId = DataConverter.ToNullableGuid(match.Groups["Id"].Value);
			var typeName = match.Groups["Type"].Value;

			if (entityId == null)
				return;

			if (typeName == "Table")
				OnSyncTable(entityId.Value);
		}

		protected IEnumerable<ParentChildEntity> GetEntities(IEnumerable<TableModel> tables)
		{
			foreach (var tableModel in tables)
			{
				var tblEntity = new ParentChildEntity
				{
					Key = String.Format("{0}/Table", tableModel.ID),
					ID = tableModel.ID,
					Name = tableModel.Name,
				};

				yield return tblEntity;

				if (tableModel.Columns != null)
				{
					foreach (var columnModel in tableModel.Columns)
					{
						var colEntity = new ParentChildEntity
						{
							Key = String.Format("{0}/Column", columnModel.ID),
							ID = columnModel.ID,
							Name = columnModel.Name,
							ParentID = tblEntity.ID,
							Type = columnModel.Type
						};

						yield return colEntity;
					}
				}
			}
		}

		protected bool GetEditVisible(object dataItem)
		{
			return true;
		}

		protected bool GetDeleteVisible(object dataItem)
		{
			return true;
		}

		protected bool GetAddVisible(object dataItem)
		{
			var entity = (TreeListTemplateDataItem)dataItem;
			var value = Convert.ToString(entity.Row.GetValue("Key"));

			if (value.EndsWith("Column"))
				return false;

			return true;
		}

		protected bool GetSynchVisible(object dataItem)
		{
			var entity = (TreeListTemplateDataItem)dataItem;
			var value = Convert.ToString(entity.Row.GetValue("Key"));

			if (value.EndsWith("Column"))
				return false;

			return true;
		}

		protected bool GetTableDataVisible(object dataItem)
		{
			var entity = (TreeListTemplateDataItem)dataItem;
			var value = Convert.ToString(entity.Row.GetValue("Key"));

			if (value.EndsWith("Column"))
				return false;

			return true;
		}

		protected String GetTableDataUrl(object dataItem)
		{
			var entity = (TreeListTemplateDataItem)dataItem;

			var key = Convert.ToString(entity.Row.GetValue("Key"));
			var Id = Convert.ToString(entity.Row.GetValue("ID"));

			if (key.EndsWith("Column"))
				return "#";

			var url = String.Format("~/Pages/Management/TableData.aspx?TableID={0}", Id);
			return url;
		}
	}
}