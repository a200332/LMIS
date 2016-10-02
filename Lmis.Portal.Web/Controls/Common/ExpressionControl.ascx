<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExpressionControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Common.ExpressionControl" %>

<table>
	<tr>
		<td>Expression:</td>
		<td>
			<asp:TextBox runat="server" ID="txtExpression" Property="ExpressionModel.Expression" Width="164px" />
		</td>
	</tr>
	<tr>
		<td>Type:</td>
		<td>
			<asp:DropDownList runat="server" ID="ddlType" Property="ExpressionModel.OutputType">
				<Items>
					<asp:ListItem Text="Unspecified" Value="Unspecified" />
					<asp:ListItem Text="Text" Value="Text" />
					<asp:ListItem Text="Number" Value="Number" />
					<asp:ListItem Text="DateTime" Value="DateTime" />
				</Items>
			</asp:DropDownList>
		</td>
	</tr>
</table>
