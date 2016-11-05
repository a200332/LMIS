<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="ReportsList.aspx.cs" Inherits="Lmis.Portal.Web.Pages.Management.ReportsList" %>

<%@ Register Src="~/Controls/Management/ReportsControl.ascx" TagPrefix="lmis" TagName="ReportsControl" %>
<%@ Register Src="~/Controls/DataDisplay/CategoriesControl.ascx" TagPrefix="lmis" TagName="CategoriesControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table>
		<tr>
			<td style="width: 30%;">
				<lmis:CategoriesControl runat="server" ID="categoriesControl" TargetUrl="~/Pages/Management/ReportsList.aspx" />
			</td>
			<td style="width: 70%; vertical-align: top;">
				<div style="text-align: left;">
					<ce:ImageLinkButton runat="server" ToolTip="Add Report" DefaultImageUrl="~/App_Themes/Default/images/add.png" ID="btnAddNew" OnClick="btnAddReport_OnClick" />
				</div>
				<div>
					<lmis:ReportsControl runat="server"
						ID="reportsControl"
						OnViewItem="reportsControl_OnViewItem"
						OnEditItem="reportsControl_OnEditItem"
						OnDeleteItem="reportsControl_OnDeleteItem" />
				</div>
			</td>
		</tr>
	</table>
</asp:Content>

