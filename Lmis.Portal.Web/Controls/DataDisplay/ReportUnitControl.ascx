<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportUnitControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataDisplay.ReportUnitControl" %>

<div style="margin-top: 15px; border: 1px solid #e5e5e5;">
	<div style="width: 205px; height: 3px; background-color: #29abe2;"></div>

	<div style="margin: 8px;">
		<asp:Panel runat="server" ID="pnlGrid">
			<div class="left">
				<asp:Label runat="server" ID="lblGridTitle" Font-Names="Times New Roman" Font-Size="13px" Font-Bold="True"></asp:Label>
			</div>
			<div style="text-align: right;">
				<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/excel.png" ID="btnGridExport" ToolTip="Excel" OnClick="btnExport_OnClick" />
			</div>
			<div>
				<dx:ASPxGridView runat="server" ID="mainGrid" Width="100%">
					<Settings ShowHeaderFilterButton="True" ShowGroupPanel="False" />
					<SettingsBehavior AllowFocusedRow="True" AllowSort="True" AllowGroup="True" />
					<Styles>
						<Cell HorizontalAlign="Left" />
						<AlternatingRow Enabled="True" />
						<Header BackColor="#b4e2f7"></Header>
						<GroupPanel BackColor="#b4e2f7"></GroupPanel>
					</Styles>
				</dx:ASPxGridView>
			</div>
		</asp:Panel>
		<asp:Panel runat="server" ID="pnlChart">
			<div class="left">
				<asp:Label runat="server" ID="lblChartTitle" Font-Names="Times New Roman" Font-Size="13px" Font-Bold="True"></asp:Label>
			</div>
			<div style="text-align: right;">
				<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/filter.png" ID="btnCaptions" ToolTip="Series Filter" />
				<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/filter.png" ID="btnXYSeries" ToolTip="Y Filter" />
				<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save.png" ID="btnImage" ToolTip="Save Image" />
				<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/excel.png" ID="btnChartExport" ToolTip="Excel" OnClick="btnExport_OnClick" />
				<a href="#" runat="server" id="lnkChartImage" title="Chart">
					<asp:Image runat="server" ImageUrl="~/App_Themes/Default/Images/chart.png" />
				</a>
				<a href="#" runat="server" id="lnkChartGrid" title="Grid">
					<asp:Image runat="server" ImageUrl="~/App_Themes/Default/Images/grid.png" />
				</a>
			</div>
			<div class="clear"></div>
			<div>
				<div runat="server" id="dvChartImage" style="display: block;">
					<asp:Chart ID="mainChart" runat="server" Width="710" Height="350" IsMapEnabled="True">
						<%--<Titles>
							<asp:Title Name="Default" Text="" TextStyle="Shadow" Font="Times New Roman, 15pt" Docking="Top">
							</asp:Title>
							<asp:Title Name="Left" Text="1" Font="Times New Roman, 10pt" Docking="Left">
							</asp:Title>
							<asp:Title Name="Bottom" Text="2" Font="Times New Roman, 10pt" Docking="Bottom">
							</asp:Title>
						</Titles>--%>

						<Series>
						</Series>

						<ChartAreas>
							<asp:ChartArea Name="MainChartArea">
								<AxisX Interval="1" LabelAutoFitStyle="WordWrap" IsMarginVisible="False">
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
					<div>
						<ce:Label runat="server" ID="lblXYDescription"></ce:Label>
					</div>
				</div>
				<div runat="server" id="dvChartGrid" style="display: none;">
					<dx:ASPxGridView runat="server" ID="chartGrid" Width="100%">
						<Settings ShowHeaderFilterButton="True" ShowGroupPanel="False" />
						<SettingsBehavior AllowSort="True" AllowGroup="True" />
						<Styles>
							<Cell HorizontalAlign="Left" />
							<AlternatingRow Enabled="True" />
							<Header BackColor="#b4e2f7"></Header>
							<GroupPanel BackColor="#b4e2f7"></GroupPanel>
						</Styles>
					</dx:ASPxGridView>
				</div>
			</div>

			<div>
				<act:ModalPopupExtender runat="server" ID="mpeCaptions" TargetControlID="btnCaptions"
					Enabled="true" BackgroundCssClass="modalBackground" PopupControlID="pnlCaptions"
					CancelControlID="btnCaptionClose" />
				<asp:Panel runat="server" ID="pnlCaptions">
					<div class="popup">
						<div class="popup_fieldset">
							<div class="popup-title">Filter</div>
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
				<act:ModalPopupExtender runat="server" ID="mpeXYSeries" TargetControlID="btnXYSeries"
					Enabled="true" BackgroundCssClass="modalBackground" PopupControlID="pnlXYSeries"
					CancelControlID="btnXYSeriesClose" />
				<asp:Panel runat="server" ID="pnlXYSeries">
					<div class="popup">
						<div class="popup_fieldset">
							<div class="popup-title">Filter</div>
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
		</asp:Panel>
		<div style="margin-top: 8px;">
			<div>
				<table>
					<tr runat="server" id="trDescription">
						<td>
							<ce:Label runat="server">Description:</ce:Label>&nbsp;</td>
						<td>
							<div runat="server" id="dvDescription"></div>
						</td>
					</tr>
					<tr runat="server" id="trInterpretation">
						<td>
							<ce:Label runat="server">Interpretation:</ce:Label>&nbsp;</td>
						<td>
							<div runat="server" id="dvInterpretation"></div>
						</td>
					</tr>
					<tr runat="server" id="trInformationSource">
						<td>
							<ce:Label runat="server">Information source:</ce:Label>&nbsp;</td>
						<td>
							<div runat="server" id="dvInformationSource"></div>
						</td>
					</tr>
				</table>
			</div>
		</div>
	</div>
</div>


