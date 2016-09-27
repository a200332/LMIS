using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Utils;
using CITI.EVO.Tools.Web.UI.Model.Interfaces;
using DevExpress.Web;

namespace CITI.EVO.Tools.Web.UI.Model.Common
{
	public class UIPropertyWorker
	{
		private static readonly bool _allowDefaultIfNull;

		private static readonly String _sourceAttributeName;

		static UIPropertyWorker()
		{
			_sourceAttributeName = ConfigurationManager.AppSettings["UIModelProcessor.SourceAttributeName"];
			_sourceAttributeName = (String.IsNullOrWhiteSpace(_sourceAttributeName) ? "Source" : _sourceAttributeName);

			var allowDefaultIfNull = ConfigurationManager.AppSettings["UIModelProcessor.AllowDefaultIfNull"];
			if (!bool.TryParse(allowDefaultIfNull, out _allowDefaultIfNull))
			{
				_allowDefaultIfNull = true;
			}
		}

		private readonly String _propertyName;
		private readonly String _propertyParams;

		private readonly PropertyInfo _propertyInfo;
		private readonly Type _propertyType;

		private readonly PropertyInfo _itemProperty;
		private readonly Type _itemType;

		private readonly MethodInfo _getMethod;
		private readonly Object[] _methodArgs;

		private readonly Object[] _paramValues;

		public UIPropertyWorker(String propertyName, String propertyParams, String dataTypeAttributeValue, Type type)
		{
			if (String.IsNullOrWhiteSpace(propertyName))
				throw new ArgumentNullException("propertyName");

			_propertyName = propertyName;
			_propertyParams = propertyParams;

			_propertyInfo = type.GetProperty(_propertyName);
			if (_propertyInfo == null)
			{
				var message = String.Format("Unable to find property '{0}.{1}'", type.FullName, _propertyName);
				throw new Exception(message);
			}

			_propertyType = GetPropertyDataType(dataTypeAttributeValue);

			if (IsMetaDataContainer(_propertyType))
			{
				_itemProperty = _propertyType.GetProperty("Item");
				_itemType = _itemProperty.PropertyType;

				var paramValue = GetPropertyParameter(_itemProperty);
				_paramValues = new[] { paramValue };

				_getMethod = _propertyType.GetMethod("TryGetValue");
				_methodArgs = new[] { paramValue, null };
			}
		}

		public void SetValue(Control control, Object modelObject)
		{
			if (control is IModelProcessor)
			{
				var container = (IModelProcessor)control;
				var propertyValue = _propertyInfo.GetValue(modelObject, _paramValues);

				propertyValue = (propertyValue ?? Activator.CreateInstance(_propertyType));

				container.SetModel(propertyValue, _propertyType);
			}
			else if (control is IComplexDataContainer)
			{
				ControlComplexValueSetter(control, modelObject);
			}
			else if (IsMetaDataContainer(_propertyType))
			{
				ControlMetaDataValueSetter(control, modelObject);
			}
			else
			{
				ControlSimpleValueSetter(control, modelObject);
			}
		}
		public void SetValue(Object modelObject, Control control)
		{
			if (control is IModelProcessor)
			{
				var container = (IModelProcessor)control;
				var controlModel = container.GetModel(_propertyType);

				_propertyInfo.SetValue(modelObject, controlModel, _paramValues);
			}
			else if (control is IComplexDataContainer)
			{
				ModelComplexValueGetter(modelObject, control);
			}
			else if (IsMetaDataContainer(_propertyType))
			{
				ModelMetaDataValueGetter(modelObject, control);
			}
			else
			{
				ModelSimpleValueGetter(modelObject, control);
			}
		}

		public Object GetValue(Control control)
		{
			if (control is IModelProcessor)
			{
				var container = (IModelProcessor)control;
				return container.GetModel(_propertyType);
			}

			if (control is IComplexDataContainer)
			{
				var container = (IComplexDataContainer)control;
				return container.Value;
			}

			if (IsMetaDataContainer(_propertyType))
			{

				var controlValue = GetControlValue(control, _itemType);
				var realValue = ConvertValue(controlValue, _itemType);

				return realValue;
			}
			else
			{
				var controlValue = GetControlValue(control, _propertyType);
				var realValue = ConvertValue(controlValue, _propertyType);

				return realValue;
			}
		}

		private void ControlSimpleValueSetter(Control control, Object modelObject)
		{
			var propertyValue = _propertyInfo.GetValue(modelObject, null);
			ControlPropertyValueSetter(control, propertyValue);
		}
		private void ControlComplexValueSetter(Control control, Object modelObject)
		{
			var sourceAttributeValue = UserInterfaceUtil.GetAttributeValue(control, _sourceAttributeName);
			sourceAttributeValue = (sourceAttributeValue ?? String.Empty);

			var declaringType = _propertyInfo.DeclaringType;

			if (!sourceAttributeValue.StartsWith(declaringType.FullName) &&
				!sourceAttributeValue.StartsWith(declaringType.Name))
			{
				var message = String.Format("Invalid source attribute '{0}'", sourceAttributeValue);
				throw new Exception(message);
			}

			var lastDotIndex = sourceAttributeValue.LastIndexOf('.') + 1;

			var propertyName = sourceAttributeValue.Substring(lastDotIndex);
			var sourcePropertyInfo = declaringType.GetProperty(propertyName);

			var propertyValue = _propertyInfo.GetValue(modelObject, _paramValues);
			var sourcePropertyValue = sourcePropertyInfo.GetValue(modelObject, _paramValues);

			var container = (IComplexDataContainer)control;

			container.Data = sourcePropertyValue;
			container.Value = propertyValue;
		}
		private void ControlMetaDataValueSetter(Control control, Object modelObject)
		{
			var propertyValue = _propertyInfo.GetValue(modelObject, null);

			var collection = _propertyInfo.GetValue(modelObject, null);
			if (collection == null)
			{
				var genericArgs = _propertyType.GetGenericArguments();

				var collType = typeof(Dictionary<,>);
				collType = collType.MakeGenericType(genericArgs);

				collection = Activator.CreateInstance(collType);
				_propertyInfo.SetValue(modelObject, collection, null);
			}

			var setValue = propertyValue;

			var @bool = (bool)_getMethod.Invoke(propertyValue, _methodArgs);
			setValue = (@bool ? _methodArgs[1] : null);

			ControlPropertyValueSetter(control, setValue);
		}
		private void ControlPropertyValueSetter(Control control, Object propertyValue)
		{
			var controlType = control.GetType();

			var setter = UIPropertyMapping.GetSetter(controlType);
			if (setter != null)
			{
				setter(control, _propertyType, propertyValue);
				return;
			}

			if (control is ISingleValueContainer)
			{
				var container = (ISingleValueContainer)control;
				container.Value = propertyValue;

				return;
			}

			if (control is RadioButton)
			{
				var container = (RadioButton)control;
				var @bool = (bool?)propertyValue;

				container.Checked = @bool.GetValueOrDefault();

				return;
			}

			if (control is CheckBox)
			{
				var container = (CheckBox)control;
				var @bool = (bool?)propertyValue;

				container.Checked = @bool.GetValueOrDefault();

				return;
			}

			if (control is TextBox)
			{
				var container = (TextBox)control;
				container.Text = Convert.ToString(propertyValue);

				return;
			}

			if (control is Label)
			{
				var container = (Label)control;
				container.Text = Convert.ToString(propertyValue);

				return;
			}

			if (control is ASPxSpinEdit)
			{
				var container = (ASPxSpinEdit)control;
				container.Value = propertyValue;

				return;
			}

			if (control is ASPxDateEdit)
			{
				var container = (ASPxDateEdit)control;
				container.Value = propertyValue;

				return;
			}

			if (control is ASPxTimeEdit)
			{
				var container = (ASPxTimeEdit)control;
				container.Value = propertyValue;

				return;
			}

			if (control is ASPxComboBox)
			{
				var container = (ASPxComboBox)control;

				var selItem = container.Items.FindByValue(propertyValue);
				if (selItem != null)
					container.SelectedItem = selItem;
				else if (container.DropDownStyle == DropDownStyle.DropDown)
				{
					container.Text = Convert.ToString(propertyValue);
					container.Value = Convert.ToString(propertyValue);
				}

				return;
			}

			if (control is ListControl)
			{
				var container = (ListControl)control;

				var collection = propertyValue as IEnumerable<String>;
				if (collection != null)
				{
					var @set = collection as ISet<String>;
					@set = (@set ?? new HashSet<String>(collection));

					foreach (ListItem item in container.Items)
					{
						item.Selected = @set.Contains(item.Value);
					}
				}
				else
				{
					foreach (ListItem item in container.Items)
					{
						item.Selected = (item.Value == Convert.ToString(propertyValue));
					}
				}

				return;
			}

			throw new Exception("Unable to detect control type");
		}

		private void ModelSimpleValueGetter(Object modelObject, Control control)
		{
			var controlValue = GetControlValue(control, _propertyType);
			var realValue = ConvertValue(controlValue, _propertyType);

			_propertyInfo.SetValue(modelObject, realValue, _paramValues);
		}
		private void ModelComplexValueGetter(Object modelObject, Control control)
		{
			var sourceAttributeValue = UserInterfaceUtil.GetAttributeValue(control, _sourceAttributeName);
			sourceAttributeValue = (sourceAttributeValue ?? String.Empty);

			var declaringType = _propertyInfo.DeclaringType;

			if (!sourceAttributeValue.StartsWith(declaringType.FullName) &&
				!sourceAttributeValue.StartsWith(declaringType.Name))
			{
				var message = String.Format("Invalid source attribute '{0}'", sourceAttributeValue);
				throw new Exception(message);
			}

			var lastDotIndex = sourceAttributeValue.LastIndexOf('.') + 1;

			var propertyName = sourceAttributeValue.Substring(lastDotIndex);
			var sourcePropertyInfo = declaringType.GetProperty(propertyName);

			var container = (IComplexDataContainer)control;

			var propertyValue = container.Value;
			var sourcePropertyValue = container.Data;

			_propertyInfo.SetValue(modelObject, propertyValue, _paramValues);
			sourcePropertyInfo.SetValue(modelObject, sourcePropertyValue, null);
		}
		private void ModelMetaDataValueGetter(Object modelObject, Control control)
		{
			var collection = _propertyInfo.GetValue(modelObject, null);
			if (collection == null)
			{
				var genericArgs = _propertyType.GetGenericArguments();

				var collType = typeof(Dictionary<,>);
				collType = collType.MakeGenericType(genericArgs);

				collection = Activator.CreateInstance(collType);
				_propertyInfo.SetValue(modelObject, collection, null);
			}

			var controlValue = GetControlValue(control, _itemType);
			var realValue = ConvertValue(controlValue, _itemType);

			_itemProperty.SetValue(collection, realValue, _paramValues);
		}

		private Object GetControlValue(Control control, Type type)
		{
			var controlType = control.GetType();

			var getter = UIPropertyMapping.GetGetter(controlType);



			if (getter != null)
			{
				return getter(control, type);
			}

			if (control is ISingleValueContainer)
			{
				var container = (ISingleValueContainer)control;
				return container.Value;
			}

			if (control is RadioButton)
			{
				var container = (RadioButton)control;
				return container.Checked;
			}

			if (control is CheckBox)
			{
				var container = (CheckBox)control;
				return container.Checked;
			}

			if (control is TextBox)
			{
				var container = (TextBox)control;
				return container.Text;
			}

			if (control is Label)
			{
				var container = (Label)control;
				return container.Text;
			}

			if (control is ASPxSpinEdit)
			{
				var container = (ASPxSpinEdit)control;
				return container.Value;
			}

			if (control is ASPxDateEdit)
			{
				var container = (ASPxDateEdit)control;
				return container.Value;
			}

			if (control is ASPxTimeEdit)
			{
				var container = (ASPxTimeEdit)control;
				return container.Value;
			}

			if (control is ListControl)
			{
				var container = (ListControl)control;
				var @set = new HashSet<String>();

				foreach (ListItem item in container.Items)
				{
					if (item.Selected)
					{
						@set.Add(item.Value);
					}
				}

				if (type == typeof(String))
				{
					foreach (var item in set)
					{
						return item;
					}

					return null;
				}

				return @set;
			}

			if (control is ASPxComboBox)
			{
				var container = (ASPxComboBox)control;

				var selItem = container.SelectedItem;
				if (selItem != null)
					return selItem.Value;

				if (container.DropDownStyle == DropDownStyle.DropDown)
					return container.Text;

				return null;
			}
         

            throw new Exception("Unable to detect control value");
		}
		private Object ConvertValue(Object value, Type type)
		{
			var converterFunc = UIPropertyMapping.GetConverter(type);
			if (converterFunc != null)
			{
				return converterFunc(value, type);
			}

			if (type.IsInstanceOfType(value))
			{
				return value;
			}

			var strValue = Convert.ToString(value, CultureInfo.CurrentUICulture);
			if (String.IsNullOrEmpty(strValue))
			{
				if (!type.IsValueType || ReflectionUtil.IsNullable(type))
				{
					return null;
				}

				if (_allowDefaultIfNull)
				{
					return Activator.CreateInstance(type);
				}

				var nullValueErrorText = String.Format("Null is not assignable to type [{0}]", type);
				throw new Exception(nullValueErrorText);
			}

			var sourceType = value.GetType();
			if (ReflectionUtil.IsNullable(sourceType))
			{
				sourceType = Nullable.GetUnderlyingType(sourceType);
			}

			var destinationType = type;
			if (ReflectionUtil.IsNullable(destinationType))
			{
				destinationType = Nullable.GetUnderlyingType(destinationType);
			}

			var converter = TypeDescriptor.GetConverter(destinationType);
			if (converter.CanConvertFrom(sourceType))
			{
				return converter.ConvertFrom(value);
			}

			converter = TypeDescriptor.GetConverter(sourceType);
			if (converter.CanConvertTo(destinationType))
			{
				return converter.ConvertTo(value, destinationType);
			}

			var unableConvertErrorText = String.Format("Unable to convert value [{0} - {1}] to type [{2}]", value, value.GetType(), type);
			throw new Exception(unableConvertErrorText);
		}

		private Object GetPropertyParameter(PropertyInfo propertyInfo)
		{
			var indexParams = propertyInfo.GetIndexParameters();
			if (indexParams.Length > 1)
			{
				var declaringType = propertyInfo.DeclaringType;

				var message = String.Format("To many indexer parameters of property '{0}.{1}'", declaringType.FullName, propertyInfo.Name);
				throw new Exception(message);
			}

			if (indexParams.Length > 0)
			{
				var indexParam = indexParams[0];
				if (indexParam.ParameterType != typeof(int) && indexParam.ParameterType != typeof(String))
				{
					var declaringType = propertyInfo.DeclaringType;

					var message = String.Format("Indexer parameter should be int or String type '{0}.{1}'", declaringType.FullName, propertyInfo.Name);
					throw new Exception(message);
				}

				return ConvertValue(_propertyParams, indexParam.ParameterType);
			}

			return null;
		}

		private Type GetPropertyDataType(String dataTypeAttributeValue)
		{
			if (String.IsNullOrWhiteSpace(dataTypeAttributeValue))
			{
				return _propertyInfo.PropertyType;
			}

			var type = Type.GetType(dataTypeAttributeValue);
			return type;
		}

		private bool IsMetaDataContainer(Type type)
		{
			return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IDictionary<,>));
		}
	}

}
