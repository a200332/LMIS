<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportLogicControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.SchemaManipulation.ReportLogicControl" %>
<table>
		<tr>
		<td>Table</td>
		<td>
			<dx:ASPxComboBox runat="server" ID="cbxTable" Property="ReportModel.TableID" ValueType="System.Guid" ValueField="ID" TextField="Name" AutoPostBack="True" OnSelectedIndexChanged="cbxTable_OnSelectedIndexChanged" />
		</td>
	</tr>
	<tr>
		<td>Logic</td>
		<td>
			<dx:ASPxComboBox runat="server" ID="cbxLogic" Property="ReportModel.LogicID" ValueType="System.Guid" ValueField="ID" TextField="Name" />
		</td>
	</tr>
</table>