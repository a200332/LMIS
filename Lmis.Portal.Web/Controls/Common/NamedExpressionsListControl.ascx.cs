using System;
using System.Collections.Generic;
using System.Web.UI;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models.Common;
using CITI.EVO.Tools.Extensions;

namespace Lmis.Portal.Web.Controls.Common
{
	public partial class NamedExpressionsListControl : BaseExtendedControl<NamedExpressionsListModel>
	{
		public List<NamedExpressionModel> Expressions
		{
			get
			{
				var list = ViewState["Expressions"] as List<NamedExpressionModel>;
				if (list == null)
				{
					list = new List<NamedExpressionModel>();
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
			var list = new List<NamedExpressionModel>();
			if (Expressions != null)
				list.AddRange(Expressions);

			var emptyEntry = new NamedExpressionModel
			{
				Name = "- New -",
			};

			list.Insert(0, emptyEntry);

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
				var entry = new NamedExpressionModel
				{
					Name = txtName.Text,
					Expression = txtExpression.Text,
					OutputType = ddlType.TryGetStringValue(),
				};

				Expressions.Insert(0, entry);
			}
			else
			{
				var entry = Expressions[lstExpressions.SelectedIndex - 1];
				entry.Name = txtName.Text;
				entry.Expression = txtExpression.Text;
				entry.OutputType = ddlType.TryGetStringValue();
			}
		}

		protected void btnDelete_OnClick(object sender, ImageClickEventArgs e)
		{
			if (lstExpressions.SelectedIndex < 1)
				return;

			Expressions.RemoveAt(lstExpressions.SelectedIndex - 1);

			txtName.Text = String.Empty;
			txtExpression.Text = String.Empty;
			ddlType.TrySetSelectedValue(null);
		}

		protected void lstExpressions_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstExpressions.SelectedIndex < 1)
			{
				txtName.Text = String.Empty;
				txtExpression.Text = String.Empty;
				return;
			}

			var entry = Expressions[lstExpressions.SelectedIndex - 1];

			txtName.Text = entry.Name;
			txtExpression.Text = entry.Expression;
			ddlType.TrySetSelectedValue(entry.OutputType);
		}

		protected override void OnGetModel(object model, Type type)
		{
			var expModel = model as NamedExpressionsListModel;
			if (expModel == null)
			{
				return;
			}

			expModel.Expressions = Expressions;
		}

		protected override void OnSetModel(object model, Type type)
		{
			var expModel = model as NamedExpressionsListModel;
			if (expModel == null)
			{
				return;
			}

			Expressions = expModel.Expressions;
		}
	}
}