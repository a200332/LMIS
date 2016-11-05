<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="AddEditReport.aspx.cs" Inherits="Lmis.Portal.Web.Pages.Management.AddEditReport" %>

<%@ Register Src="~/Controls/Management/ReportControl.ascx" TagPrefix="lmis" TagName="ReportControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td style="text-align: left;">
                <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save.png" ID="btnSaveReport" ToolTip="დამატება" OnClick="btnSaveReport_OnClick" />
                &nbsp;&nbsp;&nbsp;
                <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/close.png" ID="btnCancelReport" ToolTip="დახურვა" OnClick="btnCancelReport_OnClick" />
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <lmis:ReportControl runat="server" ID="reportControl" />
            </td>
        </tr>
    </table>
    <div class="wrapper"></div>
</asp:Content>

