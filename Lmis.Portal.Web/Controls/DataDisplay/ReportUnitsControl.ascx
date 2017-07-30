<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportUnitsControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataDisplay.ReportUnitsControl" %>

<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>
<%@ Register Src="~/Controls/DataDisplay/ReportUnitControl.ascx" TagPrefix="lmis" TagName="ReportUnitControl" %>

<asp:Repeater runat="server" ID="rpReports">
    <ItemTemplate>
        <div>
            <lmis:ReportUnitControl runat="server" ID="reportUnitControl" Model='<%# GetReportUnitModel(Container.DataItem) %>' ChartWidth='<%# ChartWidth %>' ChartHeight='<%# ChartHeight %>' ChartCssClass='<%# ChartCssClass %>' />
        </div>
    </ItemTemplate>
</asp:Repeater>
