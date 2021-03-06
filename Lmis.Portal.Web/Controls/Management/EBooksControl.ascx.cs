﻿using System;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.Management
{
	public partial class EBooksControl : BaseExtendedControl<EBooksModel>
	{
	    public event EventHandler<GenericEventArgs<Guid>> UpItem;
	    protected virtual void OnUpItem(Guid value)
	    {
	        if (UpItem != null)
	            UpItem(this, new GenericEventArgs<Guid>(value));
	    }

	    public event EventHandler<GenericEventArgs<Guid>> DownItem;
	    protected virtual void OnDownItem(Guid value)
	    {
	        if (DownItem != null)
	            DownItem(this, new GenericEventArgs<Guid>(value));
	    }

        protected void Page_Load(object sender, EventArgs e)
		{

		}

	    protected void btnUp_OnCommand(object sender, CommandEventArgs e)
	    {
	        var entityId = DataConverter.ToNullableGuid(e.CommandArgument);
	        if (entityId == null)
	            return;

	        OnUpItem(entityId.Value);
	    }

	    protected void btnDown_OnCommand(object sender, CommandEventArgs e)
	    {
	        var entityId = DataConverter.ToNullableGuid(e.CommandArgument);
	        if (entityId == null)
	            return;

	        OnDownItem(entityId.Value);
	    }

        protected override void OnSetModel(object model, Type type)
		{
			var eBooksModel = (EBooksModel)model;

			gvData.DataSource = eBooksModel.List;
			gvData.DataBind();
		}
	}
}