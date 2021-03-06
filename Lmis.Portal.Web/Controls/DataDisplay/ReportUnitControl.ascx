﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportUnitControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataDisplay.ReportUnitControl" %>

<%@ Register Src="~/Controls/DataDisplay/ReportGridsControl.ascx" TagPrefix="lmis" TagName="ReportGridsControl" %>

<div id="dvMainContainer" runat="server" style="margin-top: 15px; text-align: left; border: 1px solid #e5e5e5; height: 100%;">
    <div id="dvSubContainer" runat="server" style="margin: 8px; font-size: 11px; height: 100%;">
        <div align="center" runat="server" id="dvHeader">
            <table style="width: 100%;" runat="server" id="tblHeader">
                <tr>
                    <td style="padding-right:6px;">
                        <ce:Label runat="server" ID="lblReportTitle" Font-Names="Times New Roman" Font-Size="13px" Font-Bold="True"></ce:Label>
                    </td>
                    <td>
                        <div align="right">
                            <table>
                                <tr>
                                    <td runat="server" id="tdGridCommands" style="width:60px; text-align:right; padding-right:2px;">
                                        <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/fullsc.png" ID="btnFullscreen" ToolTip="Fullscreen" Target="_blank" />
                                        <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/download.png" ID="btnExportReport" ToolTip="Excel" />
                                    </td>
                                    <td runat="server" id="tdChartCommands" style="width:90px;">
                                        <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/x.png" ID="btnCaptions" ToolTip="Series Filter" />
                                        <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/y.png" ID="btnXYSeries" ToolTip="Y Filter" />
                                        <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/chart-type.png" ID="btnReportTypes" ToolTip="Type" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div runat="server" id="dvReport" style="height: 100%;">
            <div class="clear"></div>
            <div runat="server" id="dvData">
                <asp:Panel runat="server" ID="pnlChartImage">
                    <asp:Chart ID="mainChart" runat="server" IsMapEnabled="True" AntiAliasing="All" OnDataBound="mainChart_OnDataBound">
                        <Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="Default">
                                <AxisX Interval="1" LabelAutoFitStyle="WordWrap">
                                    <MajorGrid LineColor="LightGray"></MajorGrid>
                                </AxisX>
                                <AxisY LabelAutoFitStyle="WordWrap">
                                    <MajorGrid LineColor="LightGray"></MajorGrid>
                                </AxisY>
                            </asp:ChartArea>
                        </ChartAreas>
                        <Legends>
                            <asp:Legend Name="Default" Docking="Bottom" TableStyle="Wide" LegendStyle="Table" TitleAlignment="Near" />
                        </Legends>
                    </asp:Chart>
                    <div style="margin-left: 15px;">
                        <ce:Label runat="server" ID="lblXYDescription"></ce:Label>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlMainGrid">
                    <lmis:ReportGridsControl runat="server" ID="reportGridsControl" />
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlError" Visible="False">
                    <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
                </asp:Panel>
            </div>
            <div id="dvReportInfo" runat="server" style="margin-top: 8px; margin-left: 15px;">
                <table>
                    <tr runat="server" id="trDescription" style="display: flex;">
                        <td>
                            <ce:Label runat="server">Description:</ce:Label>&nbsp;</td>
                        <td>
                            <div runat="server" id="dvDescription"></div>
                        </td>
                    </tr>
                    <tr runat="server" id="trInterpretation" style="display: flex;">
                        <td>
                            <ce:Label runat="server">Interpretation:</ce:Label>&nbsp;</td>
                        <td>
                            <div runat="server" id="dvInterpretation"></div>
                        </td>
                    </tr>
                    <tr runat="server" id="trInformationSource" style="display: flex;">
                        <td>
                            <ce:Label runat="server">Information source:</ce:Label>&nbsp;</td>
                        <td>
                            <div runat="server" id="dvInformationSource"></div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div>
            <act:ModalPopupExtender runat="server" ID="mpeXYSeries" TargetControlID="btnXYSeries"
                Enabled="true" BackgroundCssClass="modalBackground" PopupControlID="pnlXYSeries"
                CancelControlID="btnXYSeriesClose" />
            <asp:Panel runat="server" ID="pnlXYSeries">
                <div class="popup">
                    <div class="popup_fieldset">
                        <div class="popup-title">
                            <ce:Label runat="server">Choose</ce:Label>
                        </div>
                        <div class="title_separator"></div>
                        <div class="box">
                            <asp:CheckBoxList runat="server" ID="lstXYSeries" />
                        </div>
                    </div>
                    <div class="fieldsetforicons">
                        <div class="left" style="padding-right: 10px;">
                            <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save.png" ID="btnXYSeriesOK" ToolTip="დამატება" />
                        </div>
                        <div class="left">
                            <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/close.png" ID="btnXYSeriesClose" ToolTip="დახურვა" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div>
            <act:ModalPopupExtender runat="server" ID="mpeCaptions" TargetControlID="btnCaptions"
                Enabled="true" BackgroundCssClass="modalBackground" PopupControlID="pnlCaptions"
                CancelControlID="btnCaptionClose" />
            <asp:Panel runat="server" ID="pnlCaptions">
                <div class="popup">
                    <div class="popup_fieldset">
                        <div class="popup-title">
                            <ce:Label runat="server">Choose</ce:Label>
                        </div>
                        <div class="title_separator"></div>
                        <div class="box">
                            <asp:CheckBoxList runat="server" ID="lstCaptions" />
                        </div>
                    </div>
                    <div class="fieldsetforicons">
                        <div class="left" style="padding-right: 10px;">
                            <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save.png" ID="btnCaptionOK" ToolTip="დამატება" />
                        </div>
                        <div class="left">
                            <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/close.png" ID="btnCaptionClose" ToolTip="დახურვა" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div>
            <act:ModalPopupExtender runat="server" ID="mpeExportReport" TargetControlID="btnExportReport"
                Enabled="true" BackgroundCssClass="modalBackground" PopupControlID="pnlExportReport"
                CancelControlID="btnExportReportClose" />
            <asp:Panel runat="server" ID="pnlExportReport">
                <div class="popup">
                    <div class="popup_fieldset">
                        <div class="popup-title">
                            <ce:Label runat="server">Choose</ce:Label>
                        </div>
                        <div class="title_separator"></div>
                        <div class="box">
                            <asp:RadioButtonList runat="server" ID="lstFileTypes">
                                <Items>
                                    <asp:ListItem Text="Excel" Value="Excel" />
                                    <asp:ListItem Text="CSV" Value="CSV" />
                                    <asp:ListItem Text="PDF" Value="PDF" />
                                    <asp:ListItem Text="Image" Value="Image" />
                                </Items>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="fieldsetforicons">
                        <div class="left" style="padding-right: 10px;">
                            <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save.png" ID="btnExportReportOK" ToolTip="დამატება" OnClick="btnExportReportOK_OnClick" />
                        </div>
                        <div class="left">
                            <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/close.png" ID="btnExportReportClose" ToolTip="დახურვა" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div>
            <act:ModalPopupExtender runat="server" ID="mpeReportTypes" TargetControlID="btnReportTypes"
                Enabled="true" BackgroundCssClass="modalBackground" PopupControlID="pnlReportTypes"
                CancelControlID="btnReportTypesClose" />
            <asp:Panel runat="server" ID="pnlReportTypes">
                <div class="popup">
                    <div class="popup_fieldset">
                        <div class="popup-title">
                            <ce:Label runat="server">Choose</ce:Label>
                        </div>
                        <div class="title_separator"></div>
                        <div class="box">
                            <asp:RadioButtonList runat="server" ID="lstReportTypes">
                                <Items>
                                    <asp:ListItem Text="Grid" Value="Grid" />
                                    <asp:ListItem Text="Line" Value="Line" />
                                    <asp:ListItem Text="Point" Value="Point" />
                                    <asp:ListItem Text="FastPoint" Value="FastPoint" />
                                    <asp:ListItem Text="Spline" Value="Spline" />
                                    <asp:ListItem Text="StepLine" Value="StepLine" />
                                    <asp:ListItem Text="FastLine" Value="FastLine" />
                                    <asp:ListItem Text="Bar" Value="Bar" />
                                    <asp:ListItem Text="StackedBar" Value="StackedBar" />
                                    <asp:ListItem Text="StackedBar100" Value="StackedBar100" />
                                    <asp:ListItem Text="Column" Value="Column" />
                                    <asp:ListItem Text="StackedColumn" Value="StackedColumn" />
                                    <asp:ListItem Text="StackedColumn100" Value="StackedColumn100" />
                                    <asp:ListItem Text="Area" Value="Area" />
                                    <asp:ListItem Text="SplineArea" Value="SplineArea" />
                                    <asp:ListItem Text="StackedArea" Value="StackedArea" />
                                    <asp:ListItem Text="StackedArea100" Value="StackedArea100" />
                                    <asp:ListItem Text="Pie" Value="Pie" />
                                    <asp:ListItem Text="Doughnut" Value="Doughnut" />
                                    <asp:ListItem Text="Stock" Value="Stock" />
                                    <asp:ListItem Text="Candlestick" Value="Candlestick" />
                                    <asp:ListItem Text="Range" Value="Range" />
                                    <asp:ListItem Text="SplineRange" Value="SplineRange" />
                                    <asp:ListItem Text="RangeBar" Value="RangeBar" />
                                    <asp:ListItem Text="RangeColumn" Value="RangeColumn" />
                                    <asp:ListItem Text="Radar" Value="Radar" />
                                </Items>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="fieldsetforicons">
                        <div class="left" style="padding-right: 10px;">
                            <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save.png" ID="btnReportTypesOK" ToolTip="დამატება" />
                        </div>
                        <div class="left">
                            <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/close.png" ID="btnReportTypesClose" ToolTip="დახურვა" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</div>


