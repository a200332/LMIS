<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportUnitControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataDisplay.ReportUnitControl" %>

<asp:Panel runat="server" ID="pnlGrid">
	<dx:ASPxGridView runat="server" ID="mainGrid" Width="740">
		<Settings ShowHeaderFilterButton="True"></Settings>
	</dx:ASPxGridView>
</asp:Panel>
<asp:Panel runat="server" ID="pnlChart">
	<div>
		<dx:ASPxComboBox runat="server" ID="cbxCaption"></dx:ASPxComboBox>
		<dx:ASPxComboBox runat="server" ID="cbxXYSeries"></dx:ASPxComboBox>
		<ce:ImageLinkButton runat="server" ID="btnApply" DefaultImageUrl="~/App_Themes/Default/Images/search.png" ToolTip="Apply"></ce:ImageLinkButton>
	</div>
	<div>
		<asp:Chart ID="mainChart" runat="server" Width="1200" Height="400" IsMapEnabled="True" Palette="BrightPastel">
			<Titles>
				<asp:Title Name="Default" Text="" TextStyle="Shadow" Font="Times New Roman, 15pt" Docking="Top">
				</asp:Title>
				<asp:Title Name="Left" Text="1" Font="Times New Roman, 10pt" Docking="Left">
				</asp:Title>
				<asp:Title Name="Bottom" Text="2" Font="Times New Roman, 10pt" Docking="Bottom">
				</asp:Title>
			</Titles>
			
			<Series>
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
	</div>
</asp:Panel>
