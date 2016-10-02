<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NamedExpressionControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Common.NamedExpressionControl" %>

<table>
	<tr>
		<td>Name:</td>
		<td>
			<asp:TextBox runat="server" ID="txtName" Property="NamedExpressionModel.Name" Width="164px" />
		</td>
	</tr>
	<tr>
		<td>Expression:</td>
		<td>
			<asp:TextBox runat="server" ID="txtExpression" Property="NamedExpressionModel.Expression" Width="164px" />
		</td>
	</tr>
	<tr>
		<td>Type:</td>
		<td>
			<asp:DropDownList runat="server" ID="ddlType" Property="NamedExpressionModel.OutputType">
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
