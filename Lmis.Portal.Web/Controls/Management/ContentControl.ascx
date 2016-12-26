<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.ContentControl" %>
<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="SurveyModel.ID" />
<ul>
    <li>
        <ce:Label runat="server">Title</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" ID="tbxTitle" Width="200" Property="ContentModel.Title" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">File</ce:Label>
    </li>
    <li>
        <asp:FileUpload runat="server" Width="200" ID="fuFileData" Property="ContentModel.Attachment" />
    </li>
</ul>

