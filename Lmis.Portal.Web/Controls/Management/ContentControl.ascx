<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.ContentControl" %>
<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="SurveyModel.ID" />

<table>
    <tr>
        <td>
            <ce:Label runat="server">Title</ce:Label></td>
        <td>
            <asp:TextBox runat="server" ID="tbxTitle" Property="ContentModel.Title" /></td>
    </tr>
    <tr>
        <td>
            <ce:Label runat="server">File</ce:Label></td>
        <td>
            <asp:FileUpload runat="server" ID="fuFileData" Property="ContentModel.Attachment" /></td>
    </tr>
</table>