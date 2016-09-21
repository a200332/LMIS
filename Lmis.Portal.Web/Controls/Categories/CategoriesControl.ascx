<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoriesControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Categories.CategoriesControl" %>
<div>
	<ce:ImageLinkButton runat="server" ToolTip="Edit" DefaultImageUrl="~/App_Themes/Default/images/edit.png" ID="btnEdit" OnCommand="btnEdit_OnCommand" />
	<ce:ImageLinkButton runat="server" ToolTip="Delete" DefaultImageUrl="~/App_Themes/Default/images/delete.png" ID="btnDelete" OnCommand="btnDelete_OnCommand" />
	<ce:ImageLinkButton runat="server" ToolTip="Add" DefaultImageUrl="~/App_Themes/Default/images/add.png" ID="btnAddChild" OnCommand="btnAddChild_OnCommand" />
</div>
<div>
	<ce:TreeView runat="server" KeyFieldName="ID" ParentFieldName="ParentID" TextFieldName="Name" ID="tvData"></ce:TreeView>
</div>
