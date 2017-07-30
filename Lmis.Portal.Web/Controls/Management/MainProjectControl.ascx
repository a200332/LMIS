<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MainProjectControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.MainProjectControl" %>
<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="ProjectModel.ID" />
<lmis:HiddenFieldValueControl runat="server" ID="hdParentID" Property="ProjectModel.ParentID" />
<ul>
    <li>
        <ce:Label runat="server">Language</ce:Label>
    </li>
    <li>
        <dx:ASPxComboBox runat="server" ID="cbxLanguage" Width="200" Property="ProjectModel.Language" TextField="Key" ValueField="Value" ValueType="System.String" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Title</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" Width="200" ID="tbxTitle" Property="ProjectModel.Title" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Number</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" Width="200" ID="tbxNumber" Property="ProjectModel.Number" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Order Index</ce:Label>
    </li>
    <li>
        <dx:ASPxSpinEdit runat="server" Width="200" ID="seOrderIndex" Property="ProjectModel.OrderIndex"></dx:ASPxSpinEdit>
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Description</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" Width="200" ID="tbxDescription" Property="ProjectModel.Description" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Icon</ce:Label>
    </li>
    <li>
        <asp:FileUpload runat="server" Width="200" ID="fuImage" Property="ProjectModel.Image" />
    </li>
</ul>
