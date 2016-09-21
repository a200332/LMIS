using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using CITI.EVO.Tools.Utils;
using CITI.EVO.Tools.Web.UI.Model.Entities;

namespace CITI.EVO.Tools.Web.UI.Model.Common
{
	public class UIModelProcessor
	{
		private static readonly String _propertyAttributeName;
		private static readonly String _dataTypeAttributeName;

		static UIModelProcessor()
		{
			_propertyAttributeName = ConfigurationManager.AppSettings["UIModelProcessor.PropertyAttributeName"];
			_propertyAttributeName = (String.IsNullOrWhiteSpace(_propertyAttributeName) ? "Property" : _propertyAttributeName);

			_dataTypeAttributeName = ConfigurationManager.AppSettings["UIModelProcessor.DataTypeAttributeName"];
			_dataTypeAttributeName = (String.IsNullOrWhiteSpace(_dataTypeAttributeName) ? "DataType" : _dataTypeAttributeName);
		}

		private readonly Control _containerControl;
		private readonly UIControlMapping _controlMapping;

		public UIModelProcessor(Control containerControl)
		{
			_containerControl = containerControl;
			_controlMapping = new UIControlMapping(_containerControl, _propertyAttributeName);
		}

		public TModel GetModel<TModel>() where TModel : class, new()
		{
			return (TModel)GetModel(typeof(TModel));
		}

		public void FillModel<TModel>(TModel model) where TModel : class
		{
			FillModel(model, typeof(TModel));
		}

		public void SetModel<TModel>(TModel model) where TModel : class
		{
			SetModel(model, typeof(TModel));
		}

		public Object GetModel(Type type)
		{
			var newModel = Activator.CreateInstance(type);
			FillModel(newModel, type);

			return newModel;
		}

		public void FillModel(Object model, Type type)
		{
			var controls = GetModelControls(type);

			foreach (var entity in controls)
			{
				var control = entity.Control;

				var dataTypeAttributeValue = UserInterfaceUtil.GetAttributeValue(control, _dataTypeAttributeName);
				dataTypeAttributeValue = (dataTypeAttributeValue ?? String.Empty);

				var worker = new UIPropertyWorker(entity.PropertyName, entity.PropertyParams, dataTypeAttributeValue, type);
				worker.SetValue(model, control);
			}
		}

		public void SetModel(Object model, Type type)
		{
			var controls = GetModelControls(type);

			foreach (var entity in controls)
			{
				var control = entity.Control;

				var dataTypeAttributeValue = UserInterfaceUtil.GetAttributeValue(control, _dataTypeAttributeName);
				dataTypeAttributeValue = (dataTypeAttributeValue ?? String.Empty);

				var worker = new UIPropertyWorker(entity.PropertyName, entity.PropertyParams, dataTypeAttributeValue, type);
				worker.SetValue(control, model);
			}
		}

		private IEnumerable<PropertyControlEntity> GetModelControls(Type type)
		{
			var controls = _controlMapping.GetControls(type.FullName);
			foreach (var entity in controls)
			{
				yield return entity;
			}

			controls = _controlMapping.GetControls(type.Name);
			foreach (var entity in controls)
			{
				yield return entity;
			}
		}
	}

}
