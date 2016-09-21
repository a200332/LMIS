using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using CITI.EVO.Tools.Utils;
using CITI.EVO.Tools.Web.UI.Model.Entities;

namespace CITI.EVO.Tools.Web.UI.Model.Common
{
	public class UIControlMapping
	{
		private readonly IDictionary<String, ISet<PropertyControlEntity>> _cache;

		private readonly Control _containerControl;
		private readonly String _attributeName;

		public UIControlMapping(Control containerControl, String attributeName)
		{
			_cache = new Dictionary<String, ISet<PropertyControlEntity>>();

			_containerControl = containerControl;
			_attributeName = attributeName;

			var allControls = UIHierarchyCache.GetChildren(_containerControl);

			foreach (var current in allControls)
			{
				var attrValue = UserInterfaceUtil.GetAttributeValue(current, _attributeName);

				var entity = PropertyNameParser(attrValue);
				if (entity == null)
				{
					continue;
				}

				ISet<PropertyControlEntity> @set;
				if (!_cache.TryGetValue(entity.ClassName, out @set))
				{
					@set = new HashSet<PropertyControlEntity>();
					_cache.Add(entity.ClassName, @set);
				}


				var propertyControl = new PropertyControlEntity(entity, current);
				@set.Add(propertyControl);
			}
		}

		public IEnumerable<PropertyControlEntity> GetControls(String typeName)
		{
			ISet<PropertyControlEntity> @set;
			if (_cache.TryGetValue(typeName, out @set))
			{
				foreach (var entity in @set)
					yield return entity;
			}
		}

		private PropertyClassEntity PropertyNameParser(String fullName)
		{
			if (String.IsNullOrWhiteSpace(fullName))
				return null;

			var open = 0;
			var sb = new StringBuilder();

			var stack = new Stack<String>();
			var @params = (String)null;

			for (int i = 0; i < fullName.Length; i++)
			{
				if (fullName[i] == '[')
				{
					open++;

					stack.Push(sb.ToString());
					sb.Clear();
				}
				else if (fullName[i] == ']')
				{
					if (--open == 0)
					{
						@params = sb.ToString();
						sb.Clear();
					}
				}
				else if (open == 0 && fullName[i] == '.')
				{
					var part = sb.ToString();
					if (String.IsNullOrWhiteSpace(part))
						throw new Exception(String.Format("Invalid property attribute value '{0}'", fullName));

					stack.Push(sb.ToString());
					sb.Clear();
				}
				else
				{
					sb.Append(fullName[i]);
				}
			}

			if (open != 0)
				throw new Exception(String.Format("Invalid property attribute value '{0}'", fullName));

			if (sb.Length > 0)
				stack.Push(sb.ToString());

			if (stack.Count < 2)
				throw new Exception(String.Format("Invalid property attribute value '{0}'", fullName));

			var propertyName = stack.Pop();
			var className = stack.Pop();

			var result = new PropertyClassEntity(className, propertyName, @params);
			return result;
		}
	}
}
