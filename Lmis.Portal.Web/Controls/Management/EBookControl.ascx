<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EBookControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.EBookControl" %>

<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="EBookModel.ID" />
<ul>
    <li>
        <ce:Label runat="server">Title</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" ID="tbxTitle" Property="EBookModel.Title" />
    </li>

</ul>
<ul>
    <li>
        <ce:Label runat="server">Url</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" ID="tbxUrl" Property="EBookModel.Url" />
    </li>

</ul>
<ul>
    <li>
        <ce:Label runat="server">Description</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" ID="tbxDescription" Property="EBookModel.Description" />
    </li>

</ul>
