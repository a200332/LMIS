using System;
using System.Collections.Generic;
using System.Web.UI;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models.Common;
using CITI.EVO.Tools.Extensions;

namespace Lmis.Portal.Web.Controls.Common
{
	public partial class ExpressionsListControl : BaseExtendedControl<ExpressionsListModel>
	{
		protected List<ExpressionModel> Expressions
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

		}

		protected void Page_PreRender(object sender, EventArgs e)
		{
			var list = new List<ExpressionModel>();
			if (Expressions != null)
				list.AddRange(Expressions);

			list.Insert(0, new ExpressionModel { Expression = "- New -" });

			var index = lstExpressions.SelectedIndex;
			if (index >= list.Count)
				index = list.Count - 1;

			lstExpressions.DataSource = list;
			lstExpressions.DataBind();

			lstExpressions.SelectedIndex = index;
		}

		protected void btnSave_OnClick(object sender, ImageClickEventArgs e)
		{
			if (String.IsNullOrWhiteSpace(txtExpression.Text))
				return;

			if (lstExpressions.SelectedIndex < 1)
			{
				var entry = new ExpressionModel
				{
					Expression = txtExpression.Text,
					OutputType = ddlType.TryGetStringValue(),
				};

				Expressions.Insert(0, entry);
			}
			else
			{
				var entry = Expressions[lstExpressions.SelectedIndex - 1];
				entry.Expression = txtExpression.Text;
				entry.OutputType = ddlType.TryGetStringValue();
			}
		}

		protected void btnDelete_OnClick(object sender, ImageClickEventArgs e)
		{
			if (lstExpressions.SelectedIndex < 1)
				return;

			Expressions.RemoveAt(lstExpressions.SelectedIndex - 1);

			txtExpression.Text = String.Empty;
			ddlType.SelectedIndex = 0;
		}

		protected void lstExpressions_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstExpressions.SelectedIndex < 1)
			{
				txtExpression.Text = String.Empty;
				return;
			}

			var entry = Expressions[lstExpressions.SelectedIndex - 1];
			txtExpression.Text = entry.Expression;
			ddlType.TrySetSelectedValue(entry.OutputType);
		}

		protected override void OnGetModel(object model, Type type)
		{
			var expModel = model as ExpressionsListModel;
			if (expModel == null)
			{
				return;
			}

			expModel.Expressions = Expressions;
		}

		protected override void OnSetModel(object model, Type type)
		{
			var expModel = model as ExpressionsListModel;
			if (expModel == null)
			{
				return;
			}

			Expressions = expModel.Expressions;
		}
	}
}