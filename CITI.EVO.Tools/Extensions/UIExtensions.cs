using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Utils;

namespace CITI.EVO.Tools.Extensions
{
	public static class UIExtensions
	{
		public static IEnumerable<TreeNode> GetAllNodes(this TreeView treeView)
		{
			var roots = treeView.Nodes.Cast<TreeNode>();
			var stack = new Stack<TreeNode>(roots);

			while (stack.Count > 0)
			{
				var node = stack.Pop();

				var children = node.ChildNodes.Cast<TreeNode>();
				foreach (var child in children)
					stack.Push(child);

				yield return node;
			}
		}

		public static Object GetFormViewState(StateBag viewState)
		{
			var stackTrace = new StackTrace();

			var frames = stackTrace.GetFrames();
			if (frames == null)
			{
				throw new Exception();
			}

			var frame = frames[1];
			var method = frame.GetMethod();

			if (!method.Name.StartsWith("get_"))
			{
				throw new Exception();
			}

			return null;
		}

		public static bool TrySetSelectedValue(this ListControl list, Object value)
		{
			if (list == null)
				return false;

			var flag = false;

			foreach (ListItem listItem in list.Items)
			{
				var equals = (listItem.Value == Convert.ToString(value));
				listItem.Selected = equals;

				if (!flag)
				{
					flag = equals;
				}
			}

			return flag;
		}

		public static String TryGetStringValue(this ListControl list)
		{
			var selectedItem = list.SelectedItem;
			if (selectedItem == null)
			{
				return null;
			}

			var value = DataConverter.ToString(selectedItem.Value);
			return value;
		}

		public static Guid? TryGetGuidValue(this ListControl list, bool emptyGuidAsNull = true)
		{
			var selectedItem = list.SelectedItem;
			if (selectedItem == null)
			{
				return null;
			}

			var value = DataConverter.ToNullableGuid(selectedItem.Value);
			if (value == Guid.Empty && emptyGuidAsNull)
			{
				value = null;
			}

			return value;
		}

		public static int? TryGetIntValue(this ITextControl textControl)
		{
			if (textControl == null)
			{
				return null;
			}

			return DataConverter.ToNullableInt(textControl.Text);
		}

		public static DateTime? TryGetDateTimeValue(this ITextControl textControl)
		{
			if (textControl == null)
			{
				return null;
			}

			return DataConverter.ToNullableDateTime(textControl.Text);
		}

		public static double? TryGetDoubleValue(this ITextControl textControl)
		{
			if (textControl == null)
			{
				return null;
			}

			return DataConverter.ToNullableDouble(textControl.Text);
		}

		public static decimal? TryGetDecimalValue(this ITextControl textControl)
		{
			if (textControl == null)
			{
				return null;
			}

			return DataConverter.ToNullableDecimal(textControl.Text);
		}

		public static void DataBind(this BaseDataBoundControl control, Object dataSource)
		{
			control.DataSource = dataSource;
			control.DataBind();
		}

		public static String RenderString(this Control control)
		{
			using (var stringWriter = new StringWriter())
			{
				using (var htmlWriter = new HtmlTextWriter(stringWriter))
				{
					control.RenderControl(htmlWriter);

					htmlWriter.Flush();
					stringWriter.Flush();

					return stringWriter.ToString();
				}
			}
		}
	}
}
