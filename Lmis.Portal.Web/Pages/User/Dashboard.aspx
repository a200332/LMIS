<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Lmis.Portal.Web.Pages.User.Dashboard" %>

<%@ Register Src="~/Controls/DataDisplay/ReportUnitsControl.ascx" TagPrefix="lmis" TagName="ReportUnitsControl" %>
<%@ Register Src="~/Controls/DataDisplay/CategoriesControl.ascx" TagPrefix="lmis" TagName="CategoriesControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="border-left: 1px solid #e5e5e5; display: table;">
        <table>
            <tr>
                <td style="vertical-align: top;">
                    <div class="left" style="width: 256px;">
                        <div class="align-left data-config">
                            <ce:ImageLinkButton runat="server" ID="btnFullscreen" Text="Fullscreen" DefaultImageUrl="../../App_Themes/Default/images/fullsc.png" NavigateUrl="~/Pages/User/DashboardEx.aspx" />
                            <ce:ImageLinkButton runat="server" ID="btnConfiguration" Text="Configuration" DefaultImageUrl="../../App_Themes/Default/images/data-config.png" NavigateUrl="~/Pages/User/ReportsConfig.aspx" />
                        </div>
                        <div>
                            <lmis:CategoriesControl runat="server" ID="categoriesControl" ForceOverflow="force-overflow" TreeListItemStyle="treelistitemstyle" TreeListScrollBar="treelistscrollbar" TargetUrl="~/Pages/User/Dashboard.aspx" />
                        </div>
                    </div>
                </td>
                <td>
                    <div class="left" style="width: 740px;">

                        <div>
                            <lmis:ReportUnitsControl runat="server" ID="reportUnitsControl" ChartWidth="700" ChartHeight="600" ChartCssClass="chartimg" />
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

