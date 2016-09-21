using System;
using System.Collections.Generic;
using System.Web.UI;
using CITI.EVO.Tools.Cache;
using CITI.EVO.Tools.Extensions;

namespace CITI.EVO.Tools.Web.UI.Model.Common
{
	public static class UIPropertyMapping
	{
		public static Action<Control, Type, Object> GetSetter(Type type)
		{
			var mapping = CommonObjectCache.InitObjectCache("$[UIPropertyMapping_Setters]", () => new Dictionary<Type, Action<Control, Type, Object>>());
			return mapping.GetValueOrDefault(type);
		}
		public static Func<Control, Type, Object> GetGetter(Type type)
		{
			var mapping = CommonObjectCache.InitObjectCache("$[UIPropertyMapping_Getters]", () => new Dictionary<Type, Func<Control, Type, Object>>());
			return mapping.GetValueOrDefault(type);
		}
		public static Func<Object, Type, Object> GetConverter(Type type)
		{
			var mapping = CommonObjectCache.InitObjectCache("$[UIPropertyMapping_Converters]", () => new Dictionary<Type, Func<Object, Type, Object>>());
			return mapping.GetValueOrDefault(type);
		}

		public static void RemoveSetter(Type type)
		{
			var mapping = CommonObjectCache.InitObjectCache("$[UIPropertyMapping_Setters]", () => new Dictionary<Type, Action<Control, Type, Object>>());
			mapping.Remove(type);
		}
		public static void RemoveGetter(Type type)
		{
			var mapping = CommonObjectCache.InitObjectCache("$[UIPropertyMapping_Getters]", () => new Dictionary<Type, Func<Control, Type, Object>>());
			mapping.Remove(type);
		}
		public static void RemoveConverter(Type type)
		{
			var mapping = CommonObjectCache.InitObjectCache("$[UIPropertyMapping_Converters]", () => new Dictionary<Type, Func<Object, Type, Object, Type>>());
			mapping.Remove(type);
		}

		public static void RegisterSetter(Type type, Action<Control, Type, Object> action)
		{
			var mapping = CommonObjectCache.InitObjectCache("$[UIPropertyMapping_Setters]", () => new Dictionary<Type, Action<Control, Type, Object>>());
			mapping[type] = action;
		}
		public static void RegisterGetter(Type type, Func<Control, Type, Object> func)
		{
			var mapping = CommonObjectCache.InitObjectCache("$[UIPropertyMapping_Getters]", () => new Dictionary<Type, Func<Control, Type, Object>>());
			mapping[type] = func;
		}
		public static void RegisterConverter(Type type, Func<Object, Type, Object, Type> func)
		{
			var mapping = CommonObjectCache.InitObjectCache("$[UIPropertyMapping_Converters]", () => new Dictionary<Type, Func<Object, Type, Object, Type>>());
			mapping[type] = func;
		}
	}

}
