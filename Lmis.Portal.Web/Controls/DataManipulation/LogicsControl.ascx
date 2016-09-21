<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LogicsControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataManipulation.LogicsControl" %>
<ce:DataGrid ID="gvData"
	runat="server"
	AutoGenerateColumns="False"
	ClientInstanceName="gvData"
	KeyFieldName="ID"
	Width="100%"
	ClientIDMode="AutoID"
	EnableRowsCache="False"
	EnableViewState="False">
	<Columns>
		<asp:TemplateField HeaderText="">
			<ItemTemplate>
				<ce:ImageLinkButton runat="server" ToolTip="View" Target="_blank" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/view.png" ID="btnView" OnCommand="btnView_OnCommand" />
				<ce:ImageLinkButton runat="server" ToolTip="Edit" Target="_blank" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/edit.png" ID="btnEdit" OnCommand="btnEdit_OnCommand" />
				<ce:ImageLinkButton runat="server" ToolTip="Delete" Target="_blank" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/delete.png" ID="btnDelete" OnCommand="btnDelete_OnCommand" />
			</ItemTemplate>
		</asp:TemplateField>
		<asp:BoundField DataField="Name"  HeaderText="Name"/>
	</Columns>
</ce:DataGrid>