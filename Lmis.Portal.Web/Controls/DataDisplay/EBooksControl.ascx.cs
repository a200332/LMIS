using System;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.DataDisplay
{
	public partial class EBooksControl : BaseExtendedControl<EBooksModel>
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override void OnSetModel(object model, Type type)
		{
			var eBooksModel = model as EBooksModel;
			if (eBooksModel == null)
				return;

			rptItems.DataSource = eBooksModel.List;
			rptItems.DataBind();
		}
	}
}