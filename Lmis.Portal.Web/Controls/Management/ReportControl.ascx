<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.ReportControl" %>

<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>
<%@ Register Src="~/Controls/Management/ReportLogicControl.ascx" TagPrefix="lmis" TagName="ReportLogicControl" %>
<%@ Register Src="~/Controls/Management/ReportLogicsControl.ascx" TagPrefix="lmis" TagName="ReportLogicsControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="ReportModel.ID" />
<div class="reports">
    <ul>
        <li style="width: 200px;">
            <ce:Label runat="server">Public</ce:Label></li>
        <li>
            <asp:CheckBox runat="server" ID="chkPublic" Width="180" Property="ReportModel.Public" />
        </li>
    </ul>
    <ul>
        <li style="width: 200px;">
            <ce:Label runat="server">Language</ce:Label></li>
        <li>
            <dx:ASPxComboBox runat="server" ID="cbxLanguage" Width="180" Property="ReportModel.Language" TextField="Key" ValueField="Value" ValueType="System.String" />
        </li>
    </ul>
    <ul>
        <li style="width: 200px;">
            <ce:Label runat="server">Name</ce:Label></li>
        <li>
            <asp:TextBox runat="server" ID="tbxName" Width="180" Property="ReportModel.Name" />
        </li>
    </ul>
    <ul>
        <li style="width: 200px;">
            <ce:Label runat="server">Category</ce:Label></li>
        <li>
            <dx:ASPxComboBox runat="server" ID="cbxCategory" Width="180" Property="ReportModel.CategoryID" ValueType="System.Guid" ValueField="ID" TextField="Name" />
        </li>
    </ul>
    <ul style="min-height: 25px; display: table; margin-bottom: 8px; height: auto;">
        <li style="width: 200px;">
            <ce:Label runat="server">Description</ce:Label></li>
        <li>
            <dx:ASPxHtmlEditor ID="heDescription" runat="server" ActiveView="Design" Width="400px" Height="300px" Property="ReportModel.Description">
                <SettingsDialogs>
                    <InsertAudioDialog>
                        <SettingsAudioUpload UploadFolder="~/DxUploads" />
                    </InsertAudioDialog>
                    <InsertFlashDialog>
                        <SettingsFlashUpload UploadFolder="~/DxUploads" />
                    </InsertFlashDialog>
                    <InsertImageDialog>
                        <SettingsImageUpload UploadFolder="~/DxUploads" />
                    </InsertImageDialog>
                    <InsertVideoDialog>
                        <SettingsVideoUpload UploadFolder="~/DxUploads" />
                    </InsertVideoDialog>
                </SettingsDialogs>
            </dx:ASPxHtmlEditor>
        </li>
    </ul>
    <ul style="min-height: 25px; display: table; margin-bottom: 8px; height: auto;">
        <li style="width: 200px;">
            <ce:Label runat="server">Interpretation</ce:Label></li>
        <li>
            <dx:ASPxHtmlEditor ID="heInterpretation" runat="server" ActiveView="Design" Width="400px" Height="300px" Property="ReportModel.Interpretation">
                <SettingsDialogs>
                    <InsertAudioDialog>
                        <SettingsAudioUpload UploadFolder="~/DxUploads" />
                    </InsertAudioDialog>
                    <InsertFlashDialog>
                        <SettingsFlashUpload UploadFolder="~/DxUploads" />
                    </InsertFlashDialog>
                    <InsertImageDialog>
                        <SettingsImageUpload UploadFolder="~/DxUploads" />
                    </InsertImageDialog>
                    <InsertVideoDialog>
                        <SettingsVideoUpload UploadFolder="~/DxUploads" />
                    </InsertVideoDialog>
                </SettingsDialogs>
            </dx:ASPxHtmlEditor>
        </li>
    </ul>
    <ul style="min-height: 25px; display: table; margin-bottom: 8px; height: auto;">
        <li style="width: 200px;">
            <ce:Label runat="server">Source</ce:Label></li>
        <li>
            <dx:ASPxHtmlEditor ID="heInformationSource" runat="server" ActiveView="Design" Width="400px" Height="300px" Property="ReportModel.InformationSource">
                <SettingsDialogs>
                    <InsertAudioDialog>
                        <SettingsAudioUpload UploadFolder="~/DxUploads" />
                    </InsertAudioDialog>
                    <InsertFlashDialog>
                        <SettingsFlashUpload UploadFolder="~/DxUploads" />
                    </InsertFlashDialog>
                    <InsertImageDialog>
                        <SettingsImageUpload UploadFolder="~/DxUploads" />
                    </InsertImageDialog>
                    <InsertVideoDialog>
                        <SettingsVideoUpload UploadFolder="~/DxUploads" />
                    </InsertVideoDialog>
                </SettingsDialogs>
            </dx:ASPxHtmlEditor>
        </li>
    </ul>
    <ul>
        <li style="width: 200px;">
            <ce:Label runat="server">Type</ce:Label></li>
        <li>
            <dx:ASPxComboBox runat="server" ID="cbxType" Property="ReportModel.Type" Width="180" ValueType="System.String">
                <Items>
                    <dx:ListEditItem Text="Grid" Value="Grid" Selected="True" />
                    <dx:ListEditItem Text="Chart" Value="Chart" />
                </Items>
            </dx:ASPxComboBox>
        </li>
    </ul>
    <ul>
        <li style="width: 200px;">
            <ce:Label runat="server">X label angle</ce:Label>
        </li>
        <li>
            <dx:ASPxSpinEdit runat="server" ID="spXLabelAngle" MinValue="0" MaxValue="90" Property="ReportModel.XLabelAngle" />
        </li>
    </ul>
</div>
<div class="wrapper"></div>
<div class="popup-title">
    <ce:Label runat="server">Logics</ce:Label>
</div>
<div class="admin-title-separator"></div>
<div class="admin-fieldset">
    <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/add.png" ID="btnNewReportLogic" ToolTip="დამატება" OnClick="btnNewReportLogic_OnClick" />
    <lmis:ReportLogicsControl runat="server" ID="reportLogicsControl" Property="ReportModel.ReportLogics" OnDeleteItem="reportLogicsControl_OnDeleteItem" OnEditItem="reportLogicsControl_OnEditItem" />
</div>
<div>
    <act:ModalPopupExtender runat="server" ID="mpeAddEditReportLogic" TargetControlID="btnAddEditReportLogicFake"
        Enabled="true" BackgroundCssClass="modalBackground" PopupControlID="pnlAddEditReportLogic"
        CancelControlID="btnCancelReportLogic" />
    <asp:Button runat="server" ID="btnAddEditReportLogicFake" Style="display: none" />
    <asp:Panel runat="server" ID="pnlAddEditReportLogic">
        <div class="popup">
            <div class="popup_fieldset">
                <div class="popup-title">
                    <ce:Label runat="server">Choose</ce:Label>
                </div>
                <div class="title_separator"></div>
                <div class="box">
                    <lmis:ReportLogicControl runat="server" ID="reportLogicControl" OnDataChanged="reportLogicControl_OnDataChanged" />
                </div>
            </div>
            <div class="fieldsetforicons">
                <div class="left" style="margin-right: 10px;">
                    <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save.png" ID="btnSaveReportLogic" ToolTip="დამატება" OnClick="btnSaveReportLogic_OnClick" />
                </div>
                <div class="left">
                    <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/close.png" ID="btnCancelReportLogic" ToolTip="დახურვა" />
                </div>
            </div>
        </div>
    </asp:Panel>
</div>
