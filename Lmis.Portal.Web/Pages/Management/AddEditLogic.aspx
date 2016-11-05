<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="AddEditLogic.aspx.cs" Inherits="Lmis.Portal.Web.Pages.Management.AddEditLogic" %>

<%@ Register Src="~/Controls/Management/LogicControl.ascx" TagPrefix="lmis" TagName="LogicControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div style="margin-bottom: 60px;">
		<div class="left" style="padding-right: 10px; ">
			<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save.png" ID="btnSaveLogic" ToolTip="დამატება" OnClick="btnSaveLogic_OnClick" />
		</div>
		<div class="left">
			<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/close.png" ID="btnCancelLogic" ToolTip="დახურვა" />
		</div>
	</div>
	<div>
		<lmis:LogicControl runat="server" ID="logicControl" />
	</div>
</asp:Content>

