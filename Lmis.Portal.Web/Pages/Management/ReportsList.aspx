<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ReportsList.aspx.cs" Inherits="Lmis.Portal.Web.Pages.Management.ReportsList" %>

<%@ Register Src="~/Controls/SchemaManipulation/ReportsControl.ascx" TagPrefix="lmis" TagName="ReportsControl" %>
<%@ Register Src="~/Controls/SchemaManipulation/ReportControl.ascx" TagPrefix="lmis" TagName="ReportControl" %>
<%@ Register Src="~/Controls/DataDisplay/CategoriesControl.ascx" TagPrefix="lmis" TagName="CategoriesControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table>
		<tr>
			<td>
				<lmis:CategoriesControl runat="server" ID="categoriesControl" />
			</td>
			<td>
				<div>
					<ce:ImageLinkButton runat="server" ToolTip="Add Report" DefaultImageUrl="~/App_Themes/Default/images/add.png" ID="btnAddNew" OnClick="btnAddReport_OnClick" />
				</div>
				<div>

					<lmis:ReportsControl runat="server"
						ID="reportsControl"
						OnViewReport="reportsControl_OnViewReport"
						OnEditReport="reportsControl_OnEditReport"
						OnDeleteReport="reportsControl_OnDeleteReport" />
				</div>
			</td>
		</tr>
	</table>



	<div>
		<act:ModalPopupExtender runat="server" ID="mpeAddEditReport" TargetControlID="btnAddEditReportFake"
			Enabled="true" BackgroundCssClass="modalBackground" PopupControlID="pnlAddEditReport"
			CancelControlID="btnCancelReport" />
		<asp:Button runat="server" ID="btnAddEditReportFake" Style="display: none" />
		<asp:Panel runat="server" ID="pnlAddEditReport">
			<div class="popup">
				<div>
				</div>
				<div class="popup_fieldset">
					<h2>Table</h2>
					<div class="title_separator"></div>
					<div class="box">
						<lmis:ReportControl runat="server" ID="reportControl" />
					</div>
				</div>
				<div class="fieldsetforicons">
					<div class="left">
						<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save_normal.png" ID="btnSaveReport" ToolTip="დამატება" OnClick="btnSaveReport_OnClick" />
					</div>
					<div class="right">
						<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/close_normal.png" ID="btnCancelReport" ToolTip="დახურვა" />
					</div>
				</div>
			</div>
		</asp:Panel>
	</div>
</asp:Content>

