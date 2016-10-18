<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Lmis.Portal.Web.Pages.User.Dashboard" %>

<%@ Register Src="~/Controls/DataDisplay/ReportUnitsControl.ascx" TagPrefix="lmis" TagName="ReportUnitsControl" %>
<%@ Register Src="~/Controls/DataDisplay/CategoriesControl.ascx" TagPrefix="lmis" TagName="CategoriesControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="border-left: 1px solid #e5e5e5; display: table;">
        <div style="width: 3px; height: 35px; background-color: #29abe2; float: left;"></div>
        <div class="left" style="width: 256px;">
            <div class="align-left" style="padding: 10px;">
                <ce:ImageLinkButton runat="server" ID="btnConfiguration" Text="Configuration" NavigateUrl="~/Pages/User/ReportsConfig.aspx" />
            </div>
            <div class="wrapper"></div>
            <div>
                <lmis:CategoriesControl runat="server" ID="categoriesControl" TargetUrl="~/Pages/User/Dashboard.aspx" />
            </div>
        </div>
        <div class="left" style="width: 740px; /*border: 1px solid #e5e5e5; */">
            <%--		     <div style="width: 205px; height: 3px; background-color: #29abe2; "></div>--%>
            <%--			<div style="text-align: left; margin: 8px; ">
				<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/add.png" ID="btnReports" ToolTip="დამატება" style="display: none;" />
			</div>--%>
            <div class="clear"></div>
            <div>
                <lmis:ReportUnitsControl runat="server" ID="reportUnitsControl" />
            </div>
        </div>
    </div>
    <%--	<div>
		<act:ModalPopupExtender runat="server" ID="mpeReports" TargetControlID="btnReports"
			Enabled="true" BackgroundCssClass="modalBackground" PopupControlID="pnlReports"
			CancelControlID="btnReportsClose" />
		<asp:Panel runat="server" ID="pnlReports">
			<div class="popup">
				<div class="popup_fieldset">
					<div class="popup-title">Filter</div>
                            <div class="title_separator"></div>
					<div class="box">
						<asp:CheckBoxList runat="server" ID="lstReports" DataTextField="Name" DataValueField="ID" />
					</div>
				</div>
				<div class="fieldsetforicons">
					<div class="left" style="padding-right: 10px;">
						<ce:ImageLinkButton runat="server"  DefaultImageUrl="~/App_Themes/Default/Images/save.png" ID="btnReportsOK" ToolTip="დამატება" />
					</div>
					<div class="left">
						<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/close.png" ID="btnReportsClose" ToolTip="დახურვა" />
					</div>
				</div>
			</div>
		</asp:Panel>
	</div>--%>
</asp:Content>

