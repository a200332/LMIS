<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportLogicControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.SchemaManipulation.ReportLogicControl" %>
<table>
	<tr>
		<td>Type</td>
		<td>
			<dx:ASPxComboBox runat="server" ID="cbxType" Property="ReportLogicModel.Type" ValueType="System.String">
				<Items>
					<dx:ListEditItem Text="Line" Value="Line" />
					<dx:ListEditItem Text="Point" Value="Point" />
					<dx:ListEditItem Text="FastPoint" Value="FastPoint" />
					<dx:ListEditItem Text="Spline" Value="Spline" />
					<dx:ListEditItem Text="StepLine" Value="StepLine" />
					<dx:ListEditItem Text="FastLine" Value="FastLine" />
					<dx:ListEditItem Text="Bar" Value="Bar" />
					<dx:ListEditItem Text="StackedBar" Value="StackedBar" />
					<dx:ListEditItem Text="StackedBar100" Value="StackedBar100" />
					<dx:ListEditItem Text="Column" Value="Column" />
					<dx:ListEditItem Text="StackedColumn" Value="StackedColumn" />
					<dx:ListEditItem Text="StackedColumn100" Value="StackedColumn100" />
					<dx:ListEditItem Text="Area" Value="Area" />
					<dx:ListEditItem Text="SplineArea" Value="SplineArea" />
					<dx:ListEditItem Text="StackedArea" Value="StackedArea" />
					<dx:ListEditItem Text="StackedArea100" Value="StackedArea100" />
					<dx:ListEditItem Text="Pie" Value="Pie" />
					<dx:ListEditItem Text="Doughnut" Value="Doughnut" />
					<dx:ListEditItem Text="Stock" Value="Stock" />
					<dx:ListEditItem Text="Candlestick" Value="Candlestick" />
					<dx:ListEditItem Text="Range" Value="Range" />
					<dx:ListEditItem Text="SplineRange" Value="SplineRange" />
					<dx:ListEditItem Text="RangeBar" Value="RangeBar" />
					<dx:ListEditItem Text="RangeColumn" Value="RangeColumn" />
					<dx:ListEditItem Text="Radar" Value="Radar" />
					<dx:ListEditItem Text="Polar" Value="Polar" />
					<dx:ListEditItem Text="ErrorBar" Value="ErrorBar" />
					<dx:ListEditItem Text="BoxPlot" Value="BoxPlot" />
				</Items>
			</dx:ASPxComboBox>
		</td>
	</tr>
	<tr>
		<td>Table</td>
		<td>
			<dx:ASPxComboBox runat="server" ID="cbxTable" Property="ReportLogicModel.TableID" ValueType="System.Guid" ValueField="ID" TextField="Name" AutoPostBack="True" OnSelectedIndexChanged="cbxTable_OnSelectedIndexChanged" />
		</td>
	</tr>
	<tr>
		<td>Logic</td>
		<td>
			<dx:ASPxComboBox runat="server" ID="cbxLogic" Property="ReportLogicModel.LogicID" ValueType="System.Guid" ValueField="ID" TextField="Name" AutoPostBack="True" OnSelectedIndexChanged="cbxLogic_OnSelectedIndexChanged" />
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<fieldset>
				<legend>Bindings</legend>
				<div>
					<table>
						<tr>
							<td>
								<asp:Panel runat="server" ID="pnlGridBinding">
									<table>
										<tr>
											<td>Caption</td>
											<td>
												<asp:TextBox runat="server" ID="tbxGridCaption" /></td>
											<td>Source</td>
											<td>
												<dx:ASPxComboBox runat="server" ID="cbxGridSource" ValueType="System.String" />
											</td>
										</tr>
									</table>
								</asp:Panel>
								<asp:Panel runat="server" ID="pnlChartBinding">
									<table>
										<tr>
											<td>Caption</td>
											<td>
												<asp:TextBox runat="server" ID="tbxChartCaption" /></td>
											<td>XValue</td>
											<td>
												<dx:ASPxComboBox runat="server" ID="cbxChartXValue" ValueType="System.String" />
											</td>
											<td>YValue</td>
											<td>
												<dx:ASPxComboBox runat="server" ID="cbxChartYValue" ValueType="System.String" />
											</td>
										</tr>
									</table>
								</asp:Panel>
							</td>
							<td>
								<ce:ImageLinkButton runat="server" ToolTip="Edit" Target="_blank" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/edit.png" ID="btnEdit" OnClick="btnSaveBinding_OnClick" />
							</td>
						</tr>
					</table>
					<asp:Panel runat="server" ID="pnlChartBindings">
						<dx:ASPxGridView ID="gvChartBindings"
							runat="server"
							AutoGenerateColumns="False"
							ClientInstanceName="gvChartBindings"
							KeyFieldName="ID"
							Width="100%"
							ClientIDMode="AutoID"
							EnableRowsCache="False"
							EnableViewState="False">
							<Columns>
								<dx:GridViewDataTextColumn Caption=" " FieldName="" VisibleIndex="0">
									<DataItemTemplate>
										<ce:ImageLinkButton runat="server" ToolTip="Edit" Target="_blank" CommandArgument='<%# Eval("Key") %>' DefaultImageUrl="~/App_Themes/Default/images/edit.png" ID="btnEdit" OnCommand="btnEditBinding_OnCommand" Visible="False" />
										<ce:ImageLinkButton runat="server" ToolTip="Delete" Target="_blank" CommandArgument='<%# Eval("Key") %>' DefaultImageUrl="~/App_Themes/Default/images/delete.png" ID="btnDelete" OnCommand="btnDeleteBinding_OnCommand" />
									</DataItemTemplate>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn Caption="Caption" FieldName="Caption" />
								<dx:GridViewDataTextColumn Caption="XValue" FieldName="XValue" />
								<dx:GridViewDataTextColumn Caption="YValue" FieldName="YValue" />
							</Columns>
						</dx:ASPxGridView>
					</asp:Panel>
					<asp:Panel runat="server" ID="pnlGridBindings">
						<dx:ASPxGridView ID="gvGridBindings"
							runat="server"
							AutoGenerateColumns="False"
							ClientInstanceName="gvGridBindings"
							KeyFieldName="ID"
							Width="100%"
							ClientIDMode="AutoID"
							EnableRowsCache="False"
							EnableViewState="False">
							<Columns>
								<dx:GridViewDataTextColumn Caption=" " FieldName="" VisibleIndex="0">
									<DataItemTemplate>
										<ce:ImageLinkButton runat="server" ToolTip="Edit" Target="_blank" CommandArgument='<%# Eval("Key") %>' DefaultImageUrl="~/App_Themes/Default/images/edit.png" ID="btnEdit" OnCommand="btnEditBinding_OnCommand" Visible="False" />
										<ce:ImageLinkButton runat="server" ToolTip="Delete" Target="_blank" CommandArgument='<%# Eval("Key") %>' DefaultImageUrl="~/App_Themes/Default/images/delete.png" ID="btnDelete" OnCommand="btnDeleteBinding_OnCommand" />
									</DataItemTemplate>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn Caption="Caption" FieldName="Caption" />
								<dx:GridViewDataTextColumn Caption="Source" FieldName="Source" />
							</Columns>
						</dx:ASPxGridView>
					</asp:Panel>
				</div>
			</fieldset>
		</td>
	</tr>
</table>
