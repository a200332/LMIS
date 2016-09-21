<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TablesControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.SchemaManipulation.TablesControl" %>
<div style="width: 80px;">
	<ce:ImageLinkButton ID="btnEdit" runat="server" ToolTip="Edit"
		CommandArgument='<%# Eval("Key") %>'
		DefaultImageUrl="~/App_Themes/Default/images/edit.png"
		OnClick="btnEdit_OnClick" />
	<ce:ImageLinkButton ID="btnDelete" runat="server" ToolTip="Delete"
		DefaultImageUrl="~/App_Themes/Default/images/delete.png"
		OnClick="btnDelete_OnClick" />
	<ce:ImageLinkButton ID="btnAddChild" runat="server" ToolTip="Add"
		DefaultImageUrl="~/App_Themes/Default/images/add.png"
		OnClick="btnAddChild_OnClick" />
	<ce:ImageLinkButton ID="btnSynch" runat="server" ToolTip="Synchronize"
		DefaultImageUrl="~/App_Themes/Default/images/sync.png"
		OnClick="btnSynch_OnClick" />
	<ce:ImageLinkButton ID="btnTableData" runat="server" ToolTip="Table Data"
		DefaultImageUrl="~/App_Themes/Default/images/edit.png"
		OnClick="btnSynch_OnClick" />
</div>
<div>
	<ce:TreeView runat="server" ID="tvData" KeyFieldName="ID" ParentFieldName="ParentID" TextFieldName="Name" OnSelectedNodeChanged="tvData_OnSelectedNodeChanged"></ce:TreeView>
</div>
