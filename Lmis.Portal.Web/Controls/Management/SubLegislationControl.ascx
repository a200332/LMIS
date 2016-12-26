<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubLegislationControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.SubLegislationControl" %>
<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="LegislationModel.ID" />
<lmis:HiddenFieldValueControl runat="server" ID="hdParentID" Property="LegislationModel.ParentID" />

<ul>
    <li>
        <ce:Label runat="server">Language</ce:Label>
    </li>
    <li>
        <dx:ASPxComboBox runat="server" ID="cbxLanguage" Width="200" Property="LegislationModel.Language" TextField="Key" ValueField="Value" ValueType="System.String" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Title</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" ID="tbxTitle" Width="200" Property="LegislationModel.Title" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Description</ce:Label>
    </li>
    <li>
        <asp:TextBox runat="server" ID="tbxDescription" Width="200" Property="LegislationModel.Description" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Order Index</ce:Label>
    </li>
    <li>
        <dx:ASPxSpinEdit runat="server" ID="seOrderIndex" Width="200" Property="LegislationModel.OrderIndex"></dx:ASPxSpinEdit>
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">File</ce:Label>
    </li>
    <li>
        <asp:FileUpload runat="server" Width="200" ID="fuFileData" Property="LegislationModel.FileData" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Icon</ce:Label>
    </li>
    <li>
        <asp:FileUpload runat="server" Width="200" ID="fuImage" Property="LegislationModel.Image" />
    </li>
</ul>