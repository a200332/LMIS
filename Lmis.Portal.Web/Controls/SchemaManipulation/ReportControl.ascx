<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.SchemaManipulation.ReportControl" %>

<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>
<%@ Register Src="~/Controls/SchemaManipulation/ReportLogicControl.ascx" TagPrefix="lmis" TagName="ReportLogicControl" %>
<%@ Register Src="~/Controls/SchemaManipulation/ReportLogicsControl.ascx" TagPrefix="lmis" TagName="ReportLogicsControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="ReportModel.ID" />
<lmis:HiddenFieldValueControl runat="server" ID="hdCategoryID" Property="ReportModel.CategoryID" />

<div>
	<table>
		<tr>
			<td>Name</td>
			<td>
				<asp:TextBox runat="server" ID="tbxName" Property="ReportModel.Name" />
			</td>
		</tr>
		<tr>
			<td>Category</td>
			<td>
				<dx:ASPxComboBox runat="server" ID="cbxCategory" Enabled="False" Property="ReportModel.CategoryID" ValueType="System.Guid" ValueField="ID" TextField="Name" />
			</td>
		</tr>
		<tr>
			<td>Type</td>
			<td>
				<dx:ASPxComboBox runat="server" ID="cbxType" Property="ReportModel.Type" ValueType="System.String">
					<Items>
						<dx:ListEditItem Text="Grid" Value="Grid" Selected="True" />
						<dx:ListEditItem Text="Chart" Value="Chart" />
					</Items>
				</dx:ASPxComboBox>
			</td>
		</tr>
		<tr>
			<td>Logics</td>
			<td>
				<div>
					<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/add.png" ID="btnNewReportLogic" ToolTip="დამატება" OnClick="btnNewReportLogic_OnClick" />
				</div>
				<div>
					<lmis:ReportLogicsControl runat="server" ID="reportLogicsControl" Property="ReportModel.ReportLogics" />
				</div>
			</td>
		</tr>
	</table>
</div>
<div>
	<act:ModalPopupExtender runat="server" ID="mpeAddEditReportLogic" TargetControlID="btnAddEditReportLogicFake"
		Enabled="true" BackgroundCssClass="modalBackground" PopupControlID="pnlAddEditReportLogic"
		CancelControlID="btnCancelReportLogic" />
	<asp:Button runat="server" ID="btnAddEditReportLogicFake" Style="display: none" />
	<asp:Panel runat="server" ID="pnlAddEditReportLogic">
		<div class="popup">
			<div>
			</div>
			<div class="popup_fieldset">
				<h2>Report Logic</h2>
				<div class="title_separator"></div>
				<div class="box">
					<lmis:ReportLogicControl runat="server" ID="reportLogicControl" OnDataChanged="reportLogicControl_OnDataChanged" />
				</div>
			</div>
			<div class="fieldsetforicons">
				<div class="left">
					<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save.png" ID="btnSaveReportLogic" ToolTip="დამატება" OnClick="btnSaveReportLogic_OnClick" />
				</div>
				<div class="right">
					<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/close.png" ID="btnCancelReportLogic" ToolTip="დახურვა" />
				</div>
			</div>
		</div>
	</asp:Panel>
</div>
