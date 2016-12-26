<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VideoControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.VideoControl" %>

<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="VideoModel.ID" />
<ul>
    <li>
        <ce:Label runat="server">Title</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" ID="tbxTitle" Property="VideoModel.Title" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Url</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" ID="tbxUrl" Property="VideoModel.Url" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Description</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" ID="tbxDescription" Property="VideoModel.Description" />
    </li>
</ul>

