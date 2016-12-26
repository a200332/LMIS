<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubCareerControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.SubCareerControl" %>
<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="CareerModel.ID" />
<lmis:HiddenFieldValueControl runat="server" ID="hdParentID" Property="CareerModel.ParentID" />

<ul>
    <li>
        <ce:Label runat="server">Language</ce:Label>
    </li>
    <li>
        <dx:ASPxComboBox runat="server" ID="cbxLanguage" Width="200" Property="CareerModel.Language" TextField="Key" ValueField="Value" ValueType="System.String" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Title</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" ID="tbxTitle" Width="200" Property="CareerModel.Title" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Order Index</ce:Label>
    </li>
    <li>
        <dx:ASPxSpinEdit runat="server" ID="seOrderIndex" Width="200" Property="CareerModel.OrderIndex"></dx:ASPxSpinEdit>
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Description</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" Width="200" ID="tbxDescription" Property="CareerModel.Description" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Url</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" ID="tbxUrl" Width="200" Property="CareerModel.Url" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Icon</ce:Label>
    </li>
    <li>
        <asp:FileUpload runat="server" Width="200" ID="fuImage" Property="CareerModel.Image" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Icon</ce:Label>
    </li>
    <li>
        <asp:FileUpload runat="server" Width="200" ID="FileUpload1" Property="ProjectModel.Image" />
    </li>
</ul>