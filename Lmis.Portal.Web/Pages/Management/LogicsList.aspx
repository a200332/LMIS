<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="LogicsList.aspx.cs" Inherits="Lmis.Portal.Web.Pages.Management.LogicsList" %>


<%@ Register Src="~/Controls/DataManipulation/LogicsControl.ascx" TagPrefix="lmis" TagName="LogicsControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<div style="text-align: left;">
		<ce:ImageLinkButton runat="server" ToolTip="Add Logic" DefaultImageUrl="~/App_Themes/Default/images/add.png" ID="btnAddNew" OnClick="btnAddLogic_OnClick" />
	</div>
	<div>
		<lmis:LogicsControl runat="server" ID="logicsControl" OnEditItem="logicsControl_OnEditItem" OnDeleteItem="logicsControl_OnDeleteItem" OnViewItem="logicsControl_OnViewItem" />
	</div>
</asp:Content>

