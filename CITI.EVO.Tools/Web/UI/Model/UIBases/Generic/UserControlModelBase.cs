namespace CITI.EVO.Tools.Web.UI.Model.UIBases.Generic
{
	public abstract class UserControlModelBase<TModel> : UserControlModelBase where TModel : class, new()
	{
		private TModel _model;
		public TModel Model
		{
			get
			{
				_model = (_model ?? GetModel<TModel>());
				return _model;
			}
			set
			{
				_model = value;
				SetModel(_model);
			}
		}
	}

}
