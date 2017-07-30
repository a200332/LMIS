<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DashboardEx.aspx.cs" Inherits="Lmis.Portal.Web.Pages.User.DashboardEx" %>

<%@ Register Src="~/Controls/DataDisplay/ReportUnitsControl.ascx" TagPrefix="lmis" TagName="ReportUnitsControl" %>
<%@ Register Src="~/Controls/DataDisplay/CategoriesControl.ascx" TagPrefix="lmis" TagName="CategoriesControl" %>

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

        <div style="display: table; height: 100%; position: absolute;">

            <div class="header" style="border-bottom: 2px solid #6b537c; margin: 20px;">
                <div class="header-items">
                    <div class="left" style="margin: 10px 10px 15px 10px; text-align: left; alignment: left;">
                        <asp:HyperLink runat="server" NavigateUrl="~/Default.aspx">
                            <asp:Image ImageUrl="~/App_Themes/Default/images/logo.png" runat="server" ID="imgLogo" />
                        </asp:HyperLink>
                    </div>

                </div>
            </div>
            <table style="margin: 20px;">
                <tr>
                    <td style="vertical-align: top;">
                        <div class="left">
                            <div class="align-left data-config">
                                <ce:ImageLinkButton runat="server" ID="btnBack" Text="Full Screen Exit" DefaultImageUrl="../../App_Themes/Default/images/fullscexit.png" NavigateUrl="~/Pages/User/Dashboard.aspx" />
                                <ce:ImageLinkButton runat="server" ID="btnConfiguration" Text="Configuration" DefaultImageUrl="../../App_Themes/Default/images/data-config.png" NavigateUrl="~/Pages/User/ReportsConfigEx.aspx" />
                            </div>
                            <div>
                                <lmis:CategoriesControl runat="server" ID="categoriesControl" ForceOverflow="" TreeListItemStyle="" TreeListScrollBar="" TargetUrl="~/Pages/User/DashboardEx.aspx" />
                            </div>
                        </div>
                    </td>
                    <td>
                        <div class="left" style="width: 800px; margin-right: 20px;">
                            <div class="chartsize">
                                <lmis:ReportUnitsControl runat="server" ID="reportUnitsControl" ChartWidth="800" ChartHeight="600" ChartCssClass="chartimg" />
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
