<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EBookControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.EBookControl" %>

<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="EBookModel.ID" />
<ul>
    <li>
        <ce:Label runat="server">Language</ce:Label>
    </li>
    <li>
        <dx:ASPxComboBox runat="server" ID="cbxLanguage" Width="200" Property="EBookModel.Language" TextField="Key" ValueField="Value" ValueType="System.String" />
    </li>
</ul>
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
<ul>
    <li>
        <ce:Label runat="server">Order Index</ce:Label>
    </li>
    <li>
        <dx:ASPxSpinEdit runat="server" ID="seOrderIndex" Width="200" Property="EBookModel.OrderIndex"></dx:ASPxSpinEdit>
    </li>
</ul>
