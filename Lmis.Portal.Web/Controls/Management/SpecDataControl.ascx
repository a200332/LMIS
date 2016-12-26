<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SpecDataControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.SpecDataControl" %>
<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="SpecModel.ID" />
<lmis:HiddenFieldValueControl runat="server" ID="hdParentID" Property="SpecModel.ParentID" />

<div class="box">
    <ul>
        <li>
            <ce:Label runat="server">Language</ce:Label>
        </li>
        <li>
            <dx:ASPxComboBox runat="server" ID="cbxLanguage" Width="200" Property="SpecModel.Language" TextField="Key" ValueField="Value" ValueType="System.String" />
        </li>
    </ul>
    <ul>
        <li>
            <ce:Label runat="server">Title</ce:Label>
        </li>
        <li>
            <asp:TextBox runat="server" ID="tbxTitle" Width="200" Property="SpecModel.Title" />
        </li>
    </ul>
    <ul>
        <li>
            <ce:Label runat="server">Order Index</ce:Label>
        </li>
        <li>
            <dx:ASPxSpinEdit runat="server" ID="seOrderIndex" Width="200" Property="SpecModel.OrderIndex"></dx:ASPxSpinEdit>
        </li>
    </ul>
</div>
<table>

    <tr>
        <td>
            <ce:Label runat="server" Text="Full Text" /></td>
        <td>
            <dx:ASPxHtmlEditor ID="heDescription" runat="server" ActiveView="Design" Width="400px" Height="300px" Property="SpecModel.FullText">
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
        </td>
    </tr>
</table>
