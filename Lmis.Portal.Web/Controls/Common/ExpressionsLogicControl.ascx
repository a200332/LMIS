<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExpressionsLogicControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Common.ExpressionsLogicControl" %>

<%@ Register Src="~/Controls/Common/NamedExpressionsListControl.ascx" TagPrefix="local" TagName="NamedExpressionsListControl" %>
<%@ Register Src="~/Controls/Common/ExpressionsListControl.ascx" TagPrefix="local" TagName="ExpressionsListControl" %>

<table>
	<tr>
		<td>
			<fieldset>
				<legend>Where conditions</legend>
				<local:ExpressionsListControl runat="server" ID="filterByControl" Property="ExpressionsLogicModel.FilterBy" />
			</fieldset>
		</td>
	</tr>
	<tr>
		<td>
			<fieldset>
				<legend>Group By</legend>
				<local:ExpressionsListControl runat="server" ID="groupByControl" Property="ExpressionsLogicModel.GroupBy" />
			</fieldset>
		</td>
	</tr>
	<tr>
		<td>
			<fieldset>
				<legend>Order By</legend>
				<local:ExpressionsListControl runat="server" ID="orderByControl" Property="ExpressionsLogicModel.OrderBy" />
			</fieldset>
		</td>
	</tr>
	<tr>
		<td>
			<fieldset>
				<legend>Select</legend>
				<local:NamedExpressionsListControl runat="server" ID="selectControl" Property="ExpressionsLogicModel.Select" />
			</fieldset>
		</td>
	</tr>
</table>
