<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportFullscreen.aspx.cs" Inherits="Lmis.Portal.Web.Pages.User.ReportFullscreen" %>

<%@ Register Src="~/Controls/DataDisplay/ReportUnitControl.ascx" TagPrefix="lmis" TagName="ReportUnitControl" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="height: 100%;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/jquery-1.9.1.min.js" />
                <asp:ScriptReference Path="~/Scripts/jquery.cookie.js" />
                <asp:ScriptReference Path="~/Scripts/jquery.bxslider.js" />
                <asp:ScriptReference Path="~/Scripts/jquery.signalR-2.2.1.min.js" />
            </Scripts>
        </asp:ScriptManager>

        <lmis:ReportUnitControl runat="server" ID="reportUnitControl" ChartWidth="1200" ChartHeight="600" ChartCssClass="chartimg" />

        <script type="text/javascript">
            $("#reportUnitControl_dvData").attr("style", "");
            $("#reportUnitControl_dvReport").attr("style", "");
            $("#reportUnitControl_mainChart").attr("style", "");
            $("#reportUnitControl_tblHeader").attr("style", "");
            $("#reportUnitControl_dvReportInfo").attr("style", "");
            $("#reportUnitControl_pnlChartImage").attr("style", "");
            $("#reportUnitControl_dvSubContainer").attr("style", "");
            $("#reportUnitControl_dvMainContainer").attr("style", "");

            $("#reportUnitControl_dvData").css({ "width": "100%", "height": "80%" });
            $("#reportUnitControl_dvReport").css({ "width": "100%", "height": "100%" });
            $("#reportUnitControl_mainChart").css({ "width": "100%", "height": "95%" });
            $("#reportUnitControl_pnlChartImage").css({ "width": "100%", "height": "100%", "text-align":"center" });
            $("#reportUnitControl_dvSubContainer").css({ "width": "100%", "height": "100%", "text-align": "center" });
            $("#reportUnitControl_dvMainContainer").css({ "width": "100%", "height": "100%" });

            $("#reportUnitControl_dvReportInfo").css({ "padding": "8px" });

            $("#reportUnitControl_tblHeader").attr("align", "center");
            $("#reportUnitControl_dvReportInfo").attr("align", "center");
        </script>
    </form>
</body>
</html>
