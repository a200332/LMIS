<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.CategoryControl" %>
<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="CategoryModel.ID" />
<ul>
    <li>
        <ce:Label runat="server">Number</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" ID="tbxNumber" Width="200"  Property="CategoryModel.Number" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Category Name</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" ID="tbxName" Width="200"  Property="CategoryModel.Name"></asp:TextBox>
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Language</ce:Label>
    </li>
    <li>
        <dx:ASPxComboBox runat="server" ID="cbxLanguage" Width="200"  Property="CategoryModel.Language" TextField="Key" ValueField="Value" ValueType="System.String" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Parent</ce:Label>
    </li>
    <li>
        <dx:ASPxComboBox runat="server" ID="cbxCategory" Width="200"  Property="CategoryModel.ParentID" ValueType="System.Guid" ValueField="ID" TextField="Name" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Order Index</ce:Label>
    </li>
    <li>
        <dx:ASPxSpinEdit runat="server" ID="seOrderIndex" Width="200"  Property="CategoryModel.OrderIndex"></dx:ASPxSpinEdit>
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Category Icon</ce:Label>
    </li>
    <li>
        <asp:FileUpload runat="server" Width="200" ID="fuImage" Property="CategoryModel.Image" />
    </li>
</ul>


