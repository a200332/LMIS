<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.SchemaManipulation.ReportControl" %>

<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="ReportModel.ID" />

<table>
	<tr>
		<td>Name</td>
		<td>
			<asp:TextBox runat="server" ID="tbxName" Property="ReportModel.Name" />
		</td>
	</tr>
	<tr>
		<td>Category</td>
		<td>
			<asp:DropDownList runat="server" ID="cbxCategory" Property="ReportModel.CategoryID" DataValueField="ID" DataTextField="Name" />
		</td>
	</tr>
	<tr>
		<td>Table</td>
		<td>
			<asp:DropDownList runat="server" ID="cbxTable" Property="ReportModel.TableID" DataValueField="ID" DataTextField="Name" />
		</td>
	</tr>
	<tr>
		<td>Logic</td>
		<td>
			<asp:DropDownList runat="server" ID="cbxLogic" Property="ReportModel.LogicID" DataValueField="ID" DataTextField="Name" />
		</td>
	</tr>
	<tr>
		<td>Type</td>
		<td>
			<asp:DropDownList runat="server" ID="cbxType" Property="ReportModel.Type">
				<Items>
					<asp:ListItem Text="Grid" Value="Grid" Selected="True" />
					<asp:ListItem Text="Line" Value="Line"  />
					<asp:ListItem Text="Point" Value="Point" />
					<asp:ListItem Text="FastPoint" Value="FastPoint" />
					<asp:ListItem Text="Spline" Value="Spline" />
					<asp:ListItem Text="StepLine" Value="StepLine" />
					<asp:ListItem Text="FastLine" Value="FastLine" />
					<asp:ListItem Text="Bar" Value="Bar" />
					<asp:ListItem Text="StackedBar" Value="StackedBar" />
					<asp:ListItem Text="StackedBar100" Value="StackedBar100" />
					<asp:ListItem Text="Column" Value="Column" />
					<asp:ListItem Text="StackedColumn" Value="StackedColumn" />
					<asp:ListItem Text="StackedColumn100" Value="StackedColumn100" />
					<asp:ListItem Text="Area" Value="Area" />
					<asp:ListItem Text="SplineArea" Value="SplineArea" />
					<asp:ListItem Text="StackedArea" Value="StackedArea" />
					<asp:ListItem Text="StackedArea100" Value="StackedArea100" />
					<asp:ListItem Text="Pie" Value="Pie" />
					<asp:ListItem Text="Doughnut" Value="Doughnut" />
					<asp:ListItem Text="Stock" Value="Stock" />
					<asp:ListItem Text="Candlestick" Value="Candlestick" />
					<asp:ListItem Text="Range" Value="Range" />
					<asp:ListItem Text="SplineRange" Value="SplineRange" />
					<asp:ListItem Text="RangeBar" Value="RangeBar" />
					<asp:ListItem Text="RangeColumn" Value="RangeColumn" />
					<asp:ListItem Text="Radar" Value="Radar" />
					<asp:ListItem Text="Polar" Value="Polar" />
					<asp:ListItem Text="ErrorBar" Value="ErrorBar" />
					<asp:ListItem Text="BoxPlot" Value="BoxPlot" />
				</Items>
			</asp:DropDownList>
		</td>
	</tr>
</table>
