<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportLogicsControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.SchemaManipulation.ReportLogicsControl" %>
<dx:ASPxGridView ID="gvData"
	runat="server"
	AutoGenerateColumns="False"
	ClientInstanceName="gvChartBindings"
	KeyFieldName="Key"
	Width="100%"
	ClientIDMode="AutoID"
	EnableRowsCache="False"
	EnableViewState="False">
	<columns>
		<dx:GridViewDataTextColumn Caption=" " FieldName="" VisibleIndex="0">
			<DataItemTemplate>
				<ce:ImageLinkButton runat="server" ToolTip="Edit" Target="_blank" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/edit.png" ID="btnEdit" OnCommand="btnEdit_OnCommand" />
				<ce:ImageLinkButton runat="server" ToolTip="Delete" Target="_blank" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/delete.png" ID="btnDelete" OnCommand="btnDelete_OnCommand" OnClientClick="return confirm('Are you sure you want to delete?');" />
			</DataItemTemplate>
		</dx:GridViewDataTextColumn>
		<dx:GridViewDataTextColumn Caption="Type" FieldName="Type" />
		<dx:GridViewDataTextColumn Caption="Logic" FieldName="Logic" />
	</columns>
</dx:ASPxGridView>