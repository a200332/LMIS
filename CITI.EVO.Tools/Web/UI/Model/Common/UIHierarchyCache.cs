using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using CITI.EVO.Tools.Utils;
using CITI.EVO.Tools.Web.UI.Model.Interfaces;

namespace CITI.EVO.Tools.Web.UI.Model.Common
{
	public static class UIHierarchyCache
	{
		private const String UIHierarchyCacheKey = "$[UIHierarchyCache]";

		public static IEnumerable<Control> GetChildren(Control parent)
		{
			var cache = InitializeCache();
			if (cache != null)
			{
				IEnumerable<Control> controls;
				if (cache.TryGetValue(parent, out controls))
					return controls;

				controls = GetAllChildren(parent);
				cache.Add(parent, controls);

				return controls;
			}

			var collection = GetAllChildren(parent);
			var children = new List<Control>(collection);

			return children;
		}

		private static IList<Control> GetAllChildren(Control parent)
		{
			var collection = UserInterfaceUtil.TraverseControls(parent, ShouldChildrenSkiped);
			var children = new List<Control>(collection);

			return children;
		}

		private static bool ShouldChildrenSkiped(Control control)
		{
			return (control is IModelProcessor);
		}
		private static IDictionary<Control, IEnumerable<Control>> InitializeCache()
		{
			var context = HttpContext.Current;
			if (context == null)
			{
				return null;
			}

			if (!context.Items.Contains(UIHierarchyCacheKey))
			{
				var dictionary = new Dictionary<Control, IEnumerable<Control>>();
				context.Items[UIHierarchyCacheKey] = dictionary;

				return dictionary;
			}
			else
			{
				var cacheItem = context.Items[UIHierarchyCacheKey];

				var dictionary = cacheItem as IDictionary<Control, IEnumerable<Control>>;
				if (dictionary == null)
				{
					dictionary = new Dictionary<Control, IEnumerable<Control>>();
					context.Items[UIHierarchyCacheKey] = dictionary;
				}

				return dictionary;
			}
		}
	}

}
