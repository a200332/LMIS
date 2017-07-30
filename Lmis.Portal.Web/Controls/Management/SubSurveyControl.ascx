<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubSurveyControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.SubSurveyControl" %>
<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="SurveyModel.ID" />
<lmis:HiddenFieldValueControl runat="server" ID="hdParentID" Property="SurveyModel.ParentID" />
<ul>
    <li>
        <ce:Label runat="server">Language</ce:Label>
    </li>
    <li>
        <dx:ASPxComboBox runat="server" ID="cbxLanguage" Width="200" Property="SurveyModel.Language" TextField="Key" ValueField="Value" ValueType="System.String" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Title</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" Width="200" ID="tbxTitle" Property="SurveyModel.Title" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Number</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" Width="200" ID="tbxNumber" Property="SurveyModel.Number" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Description</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" Width="200" ID="tbxDescription" Property="SurveyModel.Description" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Order Index</ce:Label>
    </li>
    <li>
        <dx:ASPxSpinEdit runat="server" ID="seOrderIndex" Width="200" Property="SurveyModel.OrderIndex"></dx:ASPxSpinEdit>
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Url</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" Width="200" ID="tbxUrl" Property="SurveyModel.Url" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">File</ce:Label>
    </li>
    <li>
        <asp:FileUpload runat="server" Width="200" ID="fuFileData" Property="SurveyModel.FileData" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Icon</ce:Label>
    </li>
    <li>
        <asp:FileUpload runat="server" Width="200" ID="fuImage" Property="SurveyModel.Image" />
    </li>
</ul>
