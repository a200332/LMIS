<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.SchemaManipulation.ReportControl" %>

<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>
<%@ Register Src="~/Controls/DataManipulation/LogicsControl.ascx" TagPrefix="lmis" TagName="LogicsControl" %>

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
			<dx:ASPxComboBox runat="server" ID="cbxCategory" Property="ReportModel.CategoryID" ValueType="System.Guid" ValueField="ID" TextField="Name" />
		</td>
	</tr>
	<tr>
		<td>Table</td>
		<td>
			<dx:ASPxComboBox runat="server" ID="cbxTable" Property="ReportModel.TableID" ValueType="System.Guid" ValueField="ID" TextField="Name" />
		</td>
	</tr>
	<tr>
		<td>Logic</td>
		<td>
			<dx:ASPxComboBox runat="server" ID="cbxLogic" Property="ReportModel.LogicID" ValueType="System.Guid" ValueField="ID" TextField="Name" />
		</td>
	</tr>
	<tr>
		<td>Type</td>
		<td>
			<dx:ASPxComboBox runat="server" ID="cbxType" Property="ReportModel.Type" ValueType="System.String">
				<Items>
					<dx:ListEditItem Text="Grid" Value="Grid" Selected="True" />
					<dx:ListEditItem Text="Line" Value="Line"  />
					<dx:ListEditItem Text="Point" Value="Point" />
					<dx:ListEditItem Text="FastPoint" Value="FastPoint" />
					<dx:ListEditItem Text="Spline" Value="Spline" />
					<dx:ListEditItem Text="StepLine" Value="StepLine" />
					<dx:ListEditItem Text="FastLine" Value="FastLine" />
					<dx:ListEditItem Text="Bar" Value="Bar" />
					<dx:ListEditItem Text="StackedBar" Value="StackedBar" />
					<dx:ListEditItem Text="StackedBar100" Value="StackedBar100" />
					<dx:ListEditItem Text="Column" Value="Column" />
					<dx:ListEditItem Text="StackedColumn" Value="StackedColumn" />
					<dx:ListEditItem Text="StackedColumn100" Value="StackedColumn100" />
					<dx:ListEditItem Text="Area" Value="Area" />
					<dx:ListEditItem Text="SplineArea" Value="SplineArea" />
					<dx:ListEditItem Text="StackedArea" Value="StackedArea" />
					<dx:ListEditItem Text="StackedArea100" Value="StackedArea100" />
					<dx:ListEditItem Text="Pie" Value="Pie" />
					<dx:ListEditItem Text="Doughnut" Value="Doughnut" />
					<dx:ListEditItem Text="Stock" Value="Stock" />
					<dx:ListEditItem Text="Candlestick" Value="Candlestick" />
					<dx:ListEditItem Text="Range" Value="Range" />
					<dx:ListEditItem Text="SplineRange" Value="SplineRange" />
					<dx:ListEditItem Text="RangeBar" Value="RangeBar" />
					<dx:ListEditItem Text="RangeColumn" Value="RangeColumn" />
					<dx:ListEditItem Text="Radar" Value="Radar" />
					<dx:ListEditItem Text="Polar" Value="Polar" />
					<dx:ListEditItem Text="ErrorBar" Value="ErrorBar" />
					<dx:ListEditItem Text="BoxPlot" Value="BoxPlot" />
				</Items>
			</dx:ASPxComboBox>
		</td>
	</tr>
	<tr>
		<td>Logics</td>
		<td>
			<lmis:LogicsControl runat="server" ID="logicsControl" Property="ReportModel.Logics" />
		</td>
	</tr>
</table>
