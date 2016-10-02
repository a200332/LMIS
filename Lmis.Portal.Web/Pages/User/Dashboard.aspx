<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Lmis.Portal.Web.Pages.User.Dashboard" %>

<%@ Register Src="~/Controls/DataDisplay/ReportUnitsControl.ascx" TagPrefix="lmis" TagName="ReportUnitsControl" %>
<%@ Register Src="~/Controls/DataDisplay/CategoriesControl.ascx" TagPrefix="lmis" TagName="CategoriesControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div class="left" style="width: 256px;">
		<lmis:CategoriesControl runat="server" ID="categoriesControl" />
	</div>
	<div class="left" style="width: 740px;">
		<div>
			<asp:CheckBoxList runat="server" ID="lstReports" DataTextField="Name" DataValueField="ID" RepeatDirection="Horizontal" />
			<ce:ImageLinkButton runat="server" ID="btnApply" DefaultImageUrl="~/App_Themes/Default/Images/search.png" ToolTip="Apply"></ce:ImageLinkButton>
		</div>
		<div>
			<lmis:ReportUnitsControl runat="server" ID="reportUnitsControl" />
		</div>
	</div>
</asp:Content>

