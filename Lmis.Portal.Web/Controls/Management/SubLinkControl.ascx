<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubLinkControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.SubLinkControl" %>

<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="LinkModel.ID" />
<lmis:HiddenFieldValueControl runat="server" ID="hdParentID" Property="LinkModel.ParentID" />
<ul>
    <li>
        <ce:Label runat="server">Title</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" Width="200" ID="tbxTitle" Property="LinkModel.Title" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Url</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" Width="200" ID="tbxUrl" Property="LinkModel.Url" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Order Index</ce:Label>
    </li>
    <li>
        <dx:ASPxSpinEdit runat="server" ID="seOrderIndex" Width="200" Property="LinkModel.OrderIndex"></dx:ASPxSpinEdit>
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Description</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" ID="tbxDescription" Width="200" Property="LinkModel.Description" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Image</ce:Label>
    </li>
    <li>
        <asp:FileUpload runat="server" Width="200" ID="fuImage" Property="LinkModel.Image"></asp:FileUpload>
    </li>
</ul>
