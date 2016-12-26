<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.NewsControl" %>

<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="NewsModel.ID" />
<div class="box">
    <ul>
        <li>
            <ce:Label runat="server">Title</ce:Label>
        </li>
        <li>
            <asp:TextBox runat="server" ID="tbxTitle" Width="200" Property="NewsModel.Title" />
        </li>
    </ul>
    <ul>
        <li>
            <ce:Label runat="server">Description</ce:Label>
        </li>
        <li>
            <asp:TextBox runat="server" Width="200" ID="tbxDescription" Property="NewsModel.Description" />
        </li>
    </ul>
    <ul>
        <li>
            <ce:Label runat="server">NewsDate</ce:Label>
        </li>
        <li>
            <dx:ASPxDateEdit runat="server" ID="deNewsDate" Width="200" Property="NewsModel.NewsDate"></dx:ASPxDateEdit>
        </li>
    </ul>
    <ul>
        <li>
            <ce:Label runat="server">Attachment</ce:Label>
        </li>
        <li>
            <asp:FileUpload runat="server" ID="fuAttachment" Width="200" Property="NewsModel.Attachment" />
        </li>
    </ul>
    <ul>
        <li>
            <ce:Label runat="server">Image</ce:Label>
        </li>
        <li>
            <asp:FileUpload runat="server" ID="fuImage" Width="200" Property="NewsModel.Image" />
        </li>
    </ul>

</div>
<ce:Label runat="server">FullText</ce:Label>
<dx:ASPxHtmlEditor ID="heFullText" runat="server" ActiveView="Design" Width="600" Height="300" Property="NewsModel.FullText">
    <SettingsHtmlEditing AllowScripts="True" AllowIFrames="True" AllowFormElements="True" AllowIdAttributes="True" AllowedDocumentType="Both" AllowStyleAttributes="True" AllowEditFullDocument="True" AllowHTML5MediaElements="True" AllowYouTubeVideoIFrames="True" AllowObjectAndEmbedElements="True" />
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
