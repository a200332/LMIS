using System;
using System.Web.UI;
using CITI.EVO.Tools.Web.UI.Model.UIBases.Generic;
using Lmis.Portal.DAL.DAL;

namespace Lmis.Portal.Web.Bases
{
	/// <summary>
	/// Summary description for Class1
	/// </summary>
	public class BaseUserControl<TModel> : UserControlModelBase<TModel> where TModel : class, new()
	{
		private BasePage CurrentBasePage
		{
			get
			{
				return Page as BasePage;
			}
		}

		public PortalDataContext DataContext
		{
			get
			{
				if (Page == null)
					throw new InvalidCastException("Can not convert to base page type.");

				return CurrentBasePage.DataContext;
			}
		}

		protected Control PagePostBackControl
		{
			get
			{
				return CurrentBasePage.PostBackControl;
			}
		}
	}
}
