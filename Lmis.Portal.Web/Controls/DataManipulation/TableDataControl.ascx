<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TableDataControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataManipulation.TableDataControl" %>
<div>
	<ce:ImageLinkButton runat="server" Text="Add" ID="btnAdd" OnClick="btnAdd_OnClick" DefaultImageUrl="~/App_Themes/Default/images/add.png" />
	<ce:ImageLinkButton runat="server" Text="Clear" ID="btnClear" OnClick="btnClear_OnClick" DefaultImageUrl="~/App_Themes/Default/images/clear.png" />
</div>
<div>
	<asp:FileUpload runat="server" ID="fuImport" />
	<ce:ImageLinkButton runat="server" Text="Import" ID="btnImport" OnClick="btnImport_OnClick" DefaultImageUrl="~/App_Themes/Default/images/upload.png" />
</div>
<div>
	<ce:ImageLinkButton runat="server" Text="Template" ID="btnTemplate" OnClick="btnTemplate_OnClick" DefaultImageUrl="~/App_Themes/Default/images/download.png" />
</div>
<div>
	<asp:SqlDataSource runat="server" ID="sqlDs"></asp:SqlDataSource>
	<dx:ASPxGridView runat="server" ID="gvData" DataSourceID="sqlDs"
		OnCellEditorInitialize="gvData_CellEditorInitialize"
		OnRowInserting="gvData_RowInserting"
		OnRowUpdating="gvData_RowUpdating"
		OnRowDeleting="gvData_OnRowDeleting">
		<SettingsEditing Mode="PopupEditForm" NewItemRowPosition="Top" />
	</dx:ASPxGridView>
</div>
