﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models.Common;
using CITI.EVO.Tools.Extensions;
using CITI.EVO.Tools.Utils;

namespace Lmis.Portal.Web.Controls.Common
{
	public partial class ExpressionsListControl : BaseExtendedControl<ExpressionsListModel>
	{
		public List<ExpressionModel> Expressions
		{
			get
			{
				var list = ViewState["Expressions"] as List<ExpressionModel>;
				if (list == null)
				{
					list = new List<ExpressionModel>();
					ViewState["Expressions"] = list;
				}

				return list;
			}
			set { ViewState["Expressions"] = value; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			gvExpressions.DataSource = Expressions;
			gvExpressions.DataBind();
		}

		protected void Page_PreRender(object sender, EventArgs e)
		{
			gvExpressions.DataSource = Expressions;
			gvExpressions.DataBind();
		}

		protected void btnSave_OnClick(object sender, EventArgs e)
		{
			var model = expressionControl.Model;
			if (String.IsNullOrWhiteSpace(model.Expression))
				return;

			if (model.Key != null)
			{
				var oldModel = Expressions.FirstOrDefault(n => n.Key == model.Key);
				if (oldModel != null)
				{
					oldModel.Expression = model.Expression;
					oldModel.OutputType = model.OutputType;
				}
			}
			else
			{
				var newModel = new ExpressionModel
				{
					Key = Guid.NewGuid(),
					Expression = model.Expression,
					OutputType = model.OutputType
				};

				Expressions.Add(newModel);
			}

			mpeExpression.Hide();
		}

		protected void btnAdd_OnClick(object sender, EventArgs e)
		{
			expressionControl.Model = new ExpressionModel();

			mpeExpression.Show();
		}

		protected override void btnDelete_OnCommand(object sender, CommandEventArgs e)
		{
			var key = DataConverter.ToNullableGuid(e.CommandArgument);
			if (key == null)
				return;

			var model = Expressions.FirstOrDefault(n => n.Key == key);
			if (model == null)
				return;

			Expressions.Remove(model);
		}

		protected override void btnEdit_OnCommand(object sender, CommandEventArgs e)
		{
			var key = DataConverter.ToNullableGuid(e.CommandArgument);
			if (key == null)
				return;

			var model = Expressions.FirstOrDefault(n => n.Key == key);
			if (model == null)
				return;

			expressionControl.Model = model;

			mpeExpression.Show();
		}

		protected override void OnGetModel(object model, Type type)
		{
			var expModel = model as ExpressionsListModel;
			if (expModel == null)
				return;

			expModel.Expressions = Expressions;
		}

		protected override void OnSetModel(object model, Type type)
		{
			var expModel = model as ExpressionsListModel;
			if (expModel == null)
				return;

			Expressions = expModel.Expressions;
		}
	}
}