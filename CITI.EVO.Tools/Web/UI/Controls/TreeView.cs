using System;
using System.Collections;
using System.Linq;
using System.Web.UI.WebControls;

namespace CITI.EVO.Tools.Web.UI.Controls
{
	public class TreeView : System.Web.UI.WebControls.TreeView
	{
		public String KeyFieldName { get; set; }

		public String ParentFieldName { get; set; }

		public String TextFieldName { get; set; }

		public new void DataBind()
		{
			var collection = DataSource as IEnumerable;
			if (collection == null)
				throw new Exception("DataSource must be collection");

			var lookup = collection.Cast<Object>().ToLookup(GetParentValue);

			var parents = lookup[null];

			foreach (var parent in parents)
			{
				var key = GetKeyValue(parent);

				var node = new TreeNode
				{
					Text = GetTextValue(parent),
					Value = Convert.ToString(key),
				};

				PopulateNodes(node, key, lookup);

				Nodes.Add(node);
			}

			base.DataBind();
		}

		private Object GetKeyValue(Object obj)
		{
			return GetFieldValue(obj, KeyFieldName);
		}

		private Object GetParentValue(Object obj)
		{
			return GetFieldValue(obj, ParentFieldName);
		}

		private String GetTextValue(Object obj)
		{
			var value = GetFieldValue(obj, TextFieldName);
			return Convert.ToString(value);
		}

		private Object GetFieldValue(Object obj, String name)
		{
			var type = obj.GetType();
			var property = type.GetProperty(name);

			var value = property.GetValue(obj);
			return value;
		}

		private void PopulateNodes(TreeNode parentNode, Object parentKey, ILookup<Object, Object> lookup)
		{
			var children = lookup[parentKey];
			foreach (var child in children)
			{
				var key = GetKeyValue(child);

				var node = new TreeNode
				{
					Text = GetTextValue(parentNode),
					Value = Convert.ToString(key),
				};

				node.ChildNodes.Add(node);

				PopulateNodes(node, key, lookup);
			}
		}
	}
}
