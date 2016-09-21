<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddEditLogic.aspx.cs" Inherits="Lmis.Portal.Web.Pages.Management.AddEditLogic" %>

<%@ Register Src="~/Controls/DataManipulation/LogicControl.ascx" TagPrefix="lmis" TagName="LogicControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div>
		<lmis:LogicControl runat="server" ID="logicControl" />
	</div>
	<div>
		<div class="left">
			<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save_normal.png" ID="btnSaveLogic" ToolTip="დამატება" OnClick="btnSaveLogic_OnClick" />
		</div>
		<div class="right">
			<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/close_normal.png" ID="btnCancelLogic" ToolTip="დახურვა" />
		</div>
	</div>
</asp:Content>

