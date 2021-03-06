﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="TablesList.aspx.cs" Inherits="Lmis.Portal.Web.Pages.Management.TablesList" %>

<%@ Register Src="~/Controls/Management/TablesControl.ascx" TagPrefix="lmis" TagName="TablesControl" %>
<%@ Register Src="~/Controls/Management/TableControl.ascx" TagPrefix="lmis" TagName="TableControl" %>
<%@ Register Src="~/Controls/Management/ColumnControl.ascx" TagPrefix="lmis" TagName="ColumnControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div style="text-align: left;">
		<ce:ImageLinkButton runat="server" ToolTip="Add Table" DefaultImageUrl="~/App_Themes/Default/images/add.png" ID="btnAddNew" OnClick="btnAddTable_OnClick" />
        <asp:TextBox runat="server" ID="tbxKeyword"></asp:TextBox>
        <ce:ImageLinkButton runat="server" ToolTip="Add Table" DefaultImageUrl="~/App_Themes/Default/images/search.png" ID="btnSearch" OnClick="btnSearch_OnClick" />
	</div>
	<div>
		<lmis:TablesControl runat="server" ID="tablesControl"
			OnEditItem="tablesControl_OnEditItem"
			OnDeleteItem="tablesControl_OnDeleteItem"
			OnAddNewColumn="tablesControl_OnAddNewColumn"
			OnEditColumn="tablesControl_OnEditColumn"
			OnDeleteColumn="tablesControl_OnDeleteColumn"
			OnSyncTable="tablesControl_OnSyncTable"
            OnCopyTable="tablesControl_OnCopyTable" />
	</div>
	<div>
		<act:ModalPopupExtender runat="server" ID="mpeAddEditTable" TargetControlID="btnAddEditTableFake"
			Enabled="true" BackgroundCssClass="modalBackground" PopupControlID="pnlAddEditTable"
			CancelControlID="btnCancelTable" />
		<asp:Button runat="server" ID="btnAddEditTableFake" Style="display: none" />
		<asp:Panel runat="server" ID="pnlAddEditTable">
			<div class="popup">
				<div class="popup_fieldset">
					<div class="popup-title"><ce:Label runat="server">Table</ce:Label></div>
					<div class="title_separator"></div>
					<div class="box">
						<lmis:TableControl runat="server" ID="tableControl" />
					</div>
				</div>
				<div class="fieldsetforicons">
					<div class="left" style="padding-right: 10px;">
						<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save.png" ID="btnSaveTable" ToolTip="დამატება" OnClick="btnSaveTable_OnClick" />
					</div>
					<div class="left">
						<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/close.png" ID="btnCancelTable" ToolTip="დახურვა" />
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
				<div class="popup_fieldset">
					<div class="popup-title"><ce:Label runat="server">Column</ce:Label></div>
					<div class="title_separator"></div>
					<div class="box">
						<lmis:ColumnControl runat="server" ID="columnControl" />
					</div>
				</div>
				<div class="fieldsetforicons">
					<div class="left" style="padding-right: 10px;">
						<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save.png" ID="btnSaveColumn" ToolTip="დამატება" OnClick="btnSaveColumn_OnClick" />
					</div>
					<div class="left">
						<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/close.png" ID="btnCancelColumn" ToolTip="დახურვა" />
					</div>
				</div>
			</div>
		</asp:Panel>
	</div>
</asp:Content>

