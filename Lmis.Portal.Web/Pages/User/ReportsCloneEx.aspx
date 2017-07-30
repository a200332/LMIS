<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportsCloneEx.aspx.cs" Inherits="Lmis.Portal.Web.Pages.User.ReportsCloneEx" %>

<%@ Register Src="~/Controls/DataDisplay/CategoriesControl.ascx" TagPrefix="lmis" TagName="CategoriesControl" %>
<%@ Register Src="~/Controls/DataDisplay/ReportUnitsControl.ascx" TagPrefix="lmis" TagName="ReportUnitsControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/jquery-1.9.1.min.js" />
                <asp:ScriptReference Path="~/Scripts/jquery.cookie.js" />
                <asp:ScriptReference Path="~/Scripts/jquery.bxslider.js" />
                <asp:ScriptReference Path="~/Scripts/jquery.signalR-2.2.1.min.js" />
                <asp:ScriptReference Path="~/signalr/hubs" />
            </Scripts>
        </asp:ScriptManager>

        <div style="border-left: 1px solid #e5e5e5; display: table;">
            <div class="header" style="border-bottom: 2px solid #6b537c; margin: 20px;">
                <div class="header-items">
                    <div class="left" style="margin: 10px 10px 15px 10px; text-align: left; alignment: left;">
                        <asp:HyperLink runat="server" NavigateUrl="~/Default.aspx">
                            <asp:Image ImageUrl="~/App_Themes/Default/images/logo.png" runat="server" ID="imgLogo" />
                        </asp:HyperLink>
                    </div>

                </div>
            </div>
            <table>
                <tr>
                    <td style="vertical-align: top;">
                        <div class="left" style="width: 256px;">
                            <div class="align-left config-txt">
                                <div class="left config-icons">
                                    <ce:ImageLinkButton runat="server" ID="btnConfiguration" DefaultImageUrl="../../App_Themes/Default/images/data-config.png" NavigateUrl="~/Pages/User/ReportsConfigEx.aspx" />
                                    <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/add.png" ID="btnReports" ToolTip="დამატება" OnClick="btnReports_OnClick" />
                                </div>
                            </div>
                            <div class="clear"></div>
                            <div>
                                <lmis:CategoriesControl runat="server" ID="categoriesControl" TargetUrl="~/Pages/User/DashboardEx.aspx" />
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
                                                    <lmis:ReportUnitsControl runat="server" ID="reportUnitsControl2" ChartWidth="350" ChartHeight="300" ChartCssClass="chartimg" />
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
    </form>
</body>
</html>
