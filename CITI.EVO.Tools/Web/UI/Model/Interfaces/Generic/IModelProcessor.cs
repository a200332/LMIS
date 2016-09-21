namespace CITI.EVO.Tools.Web.UI.Model.Interfaces.Generic
{
	public interface IModelProcessor : CITI.EVO.Tools.Web.UI.Model.Interfaces.IModelProcessor
	{
		TModel GetModel<TModel>() where TModel : class, new();

		void FillModel<TModel>(TModel model) where TModel : class;

		void SetModel<TModel>(TModel model) where TModel : class;
	}
}
