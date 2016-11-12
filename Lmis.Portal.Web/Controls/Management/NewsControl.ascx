<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.NewsControl" %>

<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="NewsModel.ID" />

<table>
    <tr>
        <td>
            <ce:Label runat="server">Title</ce:Label></td>
        <td>
            <asp:TextBox runat="server" ID="tbxTitle" Property="NewsModel.Title" /></td>
    </tr>
    <tr>
        <td>
            <ce:Label runat="server">Description</ce:Label></td>
        <td>
            <asp:TextBox runat="server" ID="tbxDescription" Property="NewsModel.Description" /></td>
    </tr>
    <tr>
        <td>
            <ce:Label runat="server">NewsDate</ce:Label></td>
        <td>
            <dx:ASPxDateEdit runat="server" ID="deNewsDate" Property="NewsModel.NewsDate"></dx:ASPxDateEdit>
        </td>
    </tr>
    <tr>
        <td>
            <ce:Label runat="server">Attachment</ce:Label></td>
        <td>
            <asp:FileUpload runat="server" ID="fuAttachment" Property="NewsModel.Attachment" /></td>
    </tr>
    <tr>
        <td>
            <ce:Label runat="server">Image</ce:Label></td>
        <td>
            <asp:FileUpload runat="server" ID="fuImage" Property="NewsModel.Image" /></td>
    </tr>
    <tr>
        <td>
            <ce:Label runat="server">FullText</ce:Label></td>
        <td>
            <dx:ASPxHtmlEditor ID="heFullText" runat="server" ActiveView="Design" Width="800px" Height="500px" Property="NewsModel.FullText">
                <SettingsHtmlEditing AllowScripts="True" AllowIFrames="True" AllowFormElements="True" AllowIdAttributes="True" AllowedDocumentType="Both" AllowStyleAttributes="True" AllowEditFullDocument="True" AllowHTML5MediaElements="True" AllowYouTubeVideoIFrames="True" AllowObjectAndEmbedElements="True" />
                <Settings AllowHtmlView="True" AllowInsertDirectImageUrls="True" AllowContextMenu="True" AllowScriptExecutionInPreview="True" />
                <SettingsResize AllowResize="True" />
            </dx:ASPxHtmlEditor>
        </td>
    </tr>
</table>
