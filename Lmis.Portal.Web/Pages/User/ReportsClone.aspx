﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ReportsClone.aspx.cs" Inherits="Lmis.Portal.Web.Pages.User.ReportsClone" %>

<%@ Register Src="~/Controls/DataDisplay/CategoriesControl.ascx" TagPrefix="lmis" TagName="CategoriesControl" %>
<%@ Register Src="~/Controls/DataDisplay/ReportUnitsControl.ascx" TagPrefix="lmis" TagName="ReportUnitsControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="border-left: 1px solid #e5e5e5; display: table;">
        <table>
            <tr>
                <td style="vertical-align: top;">
                    <div class="left" style="width: 256px;">
                        <div class="align-left config-txt">
                            <div class="left config-icons">
                                <ce:ImageLinkButton runat="server" ID="btnConfiguration" DefaultImageUrl="../../App_Themes/Default/images/data-config.png" NavigateUrl="~/Pages/User/ReportsConfig.aspx" />
                                <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/add.png" ID="btnReports" ToolTip="დამატება" OnClick="btnReports_OnClick" />
                            </div>
                        </div>
                        <div class="clear"></div>
                        <div>
                            <lmis:CategoriesControl runat="server" TrimNames="True" ID="categoriesControl" TargetUrl="~/Pages/User/Dashboard.aspx" />
                        </div>
                    </div>
                </td>
                <td>
                    <div class="left" style="width: 740px;">
                        <div>
                            <div>
                                <div>
                                    <table>
                                        <tr>
                                            <td style="vertical-align: top;">
                                                <lmis:ReportUnitsControl runat="server" ID="reportUnitsControl1" ChartWidth="350" ChartHeight="300" />
                                            </td>
                                            <td style="vertical-align: top;">
                                                <lmis:ReportUnitsControl runat="server" ID="reportUnitsControl2" ChartWidth="350" ChartHeight="300" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <act:ModalPopupExtender runat="server" ID="mpeReports" TargetControlID="btnReportsFake"
            Enabled="true" BackgroundCssClass="modalBackground" PopupControlID="pnlReports"
            CancelControlID="btnCancel" />
        <asp:Button runat="server" ID="btnReportsFake" Style="display: none" />
        <asp:Panel runat="server" ID="pnlReports">
            <div class="popup">
                <div class="popup_fieldset">
                    <div class="popup-title">
                        <ce:Label runat="server">Choose</ce:Label>
                    </div>
                    <div class="title_separator"></div>
                    <div class="box" style="height: 500px; overflow: auto;">
                        <asp:TreeView runat="server" ID="tvReports">
                        </asp:TreeView>
                    </div>
                </div>
                <div class="fieldsetforicons">
                    <div class="left" style="margin-right: 10px;">
                        <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save.png" ID="btnSave" ToolTip="დამატება" OnClick="btnSave_OnClick" />
                    </div>
                    <div class="left">
                        <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/close.png" ID="btnCancel" ToolTip="დახურვა" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>


</asp:Content>

