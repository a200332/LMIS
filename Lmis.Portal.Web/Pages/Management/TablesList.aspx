<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="TablesList.aspx.cs" Inherits="Lmis.Portal.Web.Pages.Management.TablesList" %>

<%@ Register Src="~/Controls/SchemaManipulation/TablesControl.ascx" TagPrefix="lmis" TagName="TablesControl" %>
<%@ Register Src="~/Controls/SchemaManipulation/TableControl.ascx" TagPrefix="lmis" TagName="TableControl" %>
<%@ Register Src="~/Controls/SchemaManipulation/ColumnControl.ascx" TagPrefix="lmis" TagName="ColumnControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div>
		<ce:ImageLinkButton runat="server" ToolTip="Add Table" DefaultImageUrl="~/App_Themes/Default/images/add.png" ID="btnAddNew" OnClick="btnAddTable_OnClick" />
	</div>
	<div>
		<lmis:TablesControl runat="server" ID="tablesControl"
			OnEditTable="tablesControl_OnEditTable"
			OnDeleteTable="tablesControl_OnDeleteTable"
			OnAddNewColumn="tablesControl_OnAddNewColumn"
			OnEditColumn="tablesControl_OnEditColumn"
			OnDeleteColumn="tablesControl_OnDeleteColumn"
			OnSyncTable="tablesControl_OnSyncTable" />
	</div>
	<div>
		<act:ModalPopupExtender runat="server" ID="mpeAddEditTable" TargetControlID="btnAddEditTableFake"
			Enabled="true" BackgroundCssClass="modalBackground" PopupControlID="pnlAddEditTable"
			CancelControlID="btnCancelTable" />
		<asp:Button runat="server" ID="btnAddEditTableFake" Style="display: none" />
		<asp:Panel runat="server" ID="pnlAddEditTable">
			<div class="popup">
				<div>
				</div>
				<div class="popup_fieldset">
					<h2>Table</h2>
					<div class="title_separator"></div>
					<div class="box">
						<lmis:TableControl runat="server" ID="tableControl" />
					</div>
				</div>
				<div class="fieldsetforicons">
					<div class="left">
						<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save.png" ID="btnSaveTable" ToolTip="დამატება" OnClick="btnSaveTable_OnClick" />
					</div>
					<div class="right">
						<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/close_normal.png" ID="btnCancelTable" ToolTip="დახურვა" />
					</div>
				</div>
			</div>
		</asp:Panel>
	</div>
	<div>
		<act:ModalPopupExtender runat="server" ID="mpeAddEditColumn" TargetControlID="btnAddEditColumnFake"
			Enabled="true" BackgroundCssClass="modalBackground" PopupControlID="pnlAddEditColumn"
			CancelControlID="btnCancelColumn" />
		<asp:Button runat="server" ID="btnAddEditColumnFake" Style="display: none" />
		<asp:Panel runat="server" ID="pnlAddEditColumn">
			<div class="popup">
				<div>
				</div>
				<div class="popup_fieldset">
					<h2>Column</h2>
					<div class="title_separator"></div>
					<div class="box">
						<lmis:ColumnControl runat="server" ID="columnControl" />
					</div>
				</div>
				<div class="fieldsetforicons">
					<div class="left">
						<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save.png" ID="btnSaveColumn" ToolTip="დამატება" OnClick="btnSaveColumn_OnClick" />
					</div>
					<div class="right">
						<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/close.png" ID="btnCancelColumn" ToolTip="დახურვა" />
					</div>
				</div>
			</div>
		</asp:Panel>
	</div>
</asp:Content>

