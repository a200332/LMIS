<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AboutUs.aspx.cs" Inherits="Lmis.Portal.Web.Pages.Management.AboutUs" MasterPageFile="~/Admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save.png" ID="btnSave" ToolTip="დამატება" OnClick="btnSave_OnClick" />
    </div>
    <div>
        <dx:ASPxHtmlEditor ID="htmlEditor" runat="server" ActiveView="Design" Width="800px" Height="500px">
            <SettingsHtmlEditing AllowScripts="True"
                AllowIFrames="True"
                AllowFormElements="True"
                AllowIdAttributes="True"
                AllowedDocumentType="Both"
                AllowStyleAttributes="False"
                AllowEditFullDocument="True"
                AllowHTML5MediaElements="True"
                AllowYouTubeVideoIFrames="True"
                AllowObjectAndEmbedElements="True" />
            <Settings AllowHtmlView="True" AllowInsertDirectImageUrls="True" AllowContextMenu="True" AllowScriptExecutionInPreview="True" />
            <SettingsResize AllowResize="True" />
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
    </div>
</asp:Content>
