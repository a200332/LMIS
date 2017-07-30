<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MainUserReportControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.MainUserReportControl" %>
<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="ProjectModel.ID" />
<lmis:HiddenFieldValueControl runat="server" ID="hdParentID" Property="ProjectModel.ParentID" />
<ul>
    <li>
        <ce:Label runat="server">Language</ce:Label>
    </li>
    <li>
        <dx:ASPxComboBox runat="server" ID="cbxLanguage" Width="200" Property="UserReportModel.Language" TextField="Key" ValueField="Value" ValueType="System.String" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Title</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" Width="200" ID="tbxTitle" Property="UserReportModel.Title" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Number</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" Width="200" ID="tbxNumber" Property="UserReportModel.Number" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Order Index</ce:Label>
    </li>
    <li>
        <dx:ASPxSpinEdit runat="server" Width="200" ID="seOrderIndex" Property="UserReportModel.OrderIndex"></dx:ASPxSpinEdit>
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Description</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" Width="200" ID="tbxDescription" Property="UserReportModel.Description" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Icon</ce:Label>
    </li>
    <li>
        <asp:FileUpload runat="server" Width="200" ID="fuImage" Property="UserReportModel.Image" />
    </li>
</ul>