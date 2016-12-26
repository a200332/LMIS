<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MainLinkControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.MainLinkControl" %>

<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="LinkModel.ID" />
<ul>
    <li>
        <ce:Label runat="server">Title</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" ID="tbxTitle" Width="200" Property="LinkModel.Title"></asp:TextBox>
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Url</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" ID="tbxUrl" Width="200" Property="LinkModel.Url"></asp:TextBox>
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Order Index</ce:Label>
    </li>
    <li>
        <dx:ASPxSpinEdit runat="server" Width="200" ID="seOrderIndex" Property="LinkModel.OrderIndex"></dx:ASPxSpinEdit>
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Image</ce:Label>
    </li>
    <li>
        <asp:FileUpload runat="server" ID="fuImage" Width="200" Property="LinkModel.Image"></asp:FileUpload>
    </li>
</ul>

