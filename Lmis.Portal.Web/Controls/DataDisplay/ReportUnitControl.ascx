﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportUnitControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataDisplay.ReportUnitControl" %>

<asp:Panel runat="server" ID="pnlGrid">
	<dx:ASPxGridView runat="server" ID="mainGrid" Width="740">
	</dx:ASPxGridView>
</asp:Panel>
<asp:Panel runat="server" ID="pnlChart">
	<asp:Chart ID="mainChart" runat="server" Width="740" Height="300">
		<Titles>
			<asp:Title Name="Default" Text="" TextStyle="Shadow" Font="Times New Roman, 15pt" Docking="Top">
			</asp:Title>
			<asp:Title Name="Left" Text="1" Font="Times New Roman, 10pt" Docking="Left">
			</asp:Title>
			<asp:Title Name="Bottom" Text="2" Font="Times New Roman, 10pt" Docking="Bottom">
			</asp:Title>
		</Titles>

		<Series>
			<asp:Series ChartArea="MainChartArea" Name="MainSeries" ChartType="FastLine" XValueMember="XValue" YValueMembers="YValue" Legend="Default"></asp:Series>
		</Series>

		<ChartAreas>
			<asp:ChartArea Name="MainChartArea">
				<AxisX Interval="1" LabelAutoFitStyle="WordWrap" IsMarginVisible="False">
					<MajorGrid LineColor="LightGray"></MajorGrid>
				</AxisX>
				<AxisY Interval="500" LabelAutoFitStyle="WordWrap" IsMarginVisible="False">
					<MajorGrid LineColor="LightGray"></MajorGrid>
				</AxisY>
			</asp:ChartArea>
		</ChartAreas>

		<Legends>
			<asp:Legend Name="Default" Docking="Bottom" TableStyle="Wide" LegendStyle="Table" TitleAlignment="Near" />
		</Legends>
	</asp:Chart>
</asp:Panel>
