using System;

namespace CITI.EVO.Tools.Web.UI.Model.Interfaces
{
	public interface IModelProcessor
	{
		Object GetModel(Type type);

		void FillModel(Object model, Type type);

		void SetModel(Object model, Type type);
	}
}
