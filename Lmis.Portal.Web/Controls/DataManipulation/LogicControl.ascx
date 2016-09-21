<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LogicControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataManipulation.LogicControl" %>

<%@ Register Src="~/Controls/Common/NamedExpressionsListControl.ascx" TagPrefix="local" TagName="NamedExpressionsListControl" %>
<%@ Register Src="~/Controls/Common/ExpressionsListControl.ascx" TagPrefix="local" TagName="ExpressionsListControl" %>

<asp:RadioButtonList runat="server" ID="lstType" Property="LogicModel.Type" AutoPostBack="True" RepeatDirection="Horizontal">
	<Items>
		<asp:ListItem Text="Logic" Value="Logic" Selected="True" />
		<asp:ListItem Text="Query" Value="Query" />
	</Items>
</asp:RadioButtonList>

<asp:Panel runat="server" ID="pnlName">
	<asp:TextBox runat="server" Property="LogicModel.Name"></asp:TextBox>
</asp:Panel>
<asp:Panel runat="server" ID="pnlQuery">
	<asp:TextBox runat="server" TextMode="MultiLine" Property="LogicModel.Query"></asp:TextBox>
</asp:Panel>
<asp:Panel runat="server" ID="pnlLogic">
	<table>
		<tr>
			<td>
				<fieldset>
					<legend>Where conditions</legend>
					<local:ExpressionsListControl runat="server" ID="filterByControl" Property="LogicModel.FilterBy" />
				</fieldset>
			</td>
		</tr>
		<tr>
			<td>
				<fieldset>
					<legend>Group By</legend>
					<local:NamedExpressionsListControl runat="server" ID="groupByControl" Property="LogicModel.GroupBy" />
				</fieldset>
			</td>
		</tr>
		<tr>
			<td>
				<fieldset>
					<legend>Order By</legend>
					<local:ExpressionsListControl runat="server" ID="orderByControl" Property="LogicModel.OrderBy" />
				</fieldset>
			</td>
		</tr>
		<tr>
			<td>
				<fieldset>
					<legend>Select</legend>
					<local:NamedExpressionsListControl runat="server" ID="selectControl" Property="LogicModel.Select" />
				</fieldset>
			</td>
		</tr>
	</table>
</asp:Panel>

