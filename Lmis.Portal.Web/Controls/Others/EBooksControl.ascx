<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EBooksControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Others.EBooksControl" %>
<dx:ASPxGridView ID="gvData"
	runat="server"
	AutoGenerateColumns="False"
	ClientInstanceName="gvData"
	KeyFieldName="ID"
	Width="100%"
	ClientIDMode="AutoID"
	EnableRowsCache="False"
	EnableViewState="False">
	<columns>
		<dx:GridViewDataTextColumn Caption=" " FieldName="" VisibleIndex="0" Name="colCommand">
			<DataItemTemplate>
				<ce:ImageLinkButton runat="server" ToolTip="View" Target="_blank" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/view.png" ID="btnView" OnCommand="btnView_OnCommand" />
				<ce:ImageLinkButton runat="server" ToolTip="Edit" Target="_blank" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/edit.png" ID="btnEdit" OnCommand="btnEdit_OnCommand" />
				<ce:ImageLinkButton runat="server" ToolTip="Delete" Target="_blank" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/delete.png" ID="btnDelete" OnCommand="btnDelete_OnCommand" OnClientClick="return confirm('Are you sure you want to delete?');" />
			</DataItemTemplate>
		</dx:GridViewDataTextColumn>
		<dx:GridViewDataTextColumn Caption="Title" FieldName="Title"/>
		<dx:GridViewDataTextColumn Caption="Url" FieldName="Url"/>
		<dx:GridViewDataTextColumn Caption="Description" FieldName="Description"/>
	</columns>
</dx:ASPxGridView>