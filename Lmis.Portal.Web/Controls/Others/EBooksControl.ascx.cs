using System;
using System.Linq;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.Others
{
	public partial class EBooksControl : BaseExtendedControl<EBooksModel>
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override void OnSetModel(object model, Type type)
		{
			var eBooksModel = (EBooksModel)model;

			gvData.DataSource = eBooksModel.List;
			gvData.DataBind();
		}
	}
}