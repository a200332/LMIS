<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SpecCategoryControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.SpecCategoryControl" %>

<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="SpecModel.ID" />
<lmis:HiddenFieldValueControl runat="server" ID="hdParentID" Property="SpecModel.ParentID" />

<table>
    <tr>
        <td>
            <ce:Label runat="server">Language</ce:Label></td>
        <td>
            <dx:ASPxComboBox runat="server" ID="cbxLanguage" Width="180" Property="SpecModel.Language" TextField="Key" ValueField="Value" ValueType="System.String" />
        </td>
    </tr>
    <tr>
        <td>
            <ce:Label runat="server">Title</ce:Label></td>
        <td>
            <asp:TextBox runat="server" ID="tbxTitle" Property="SpecModel.Title" /></td>
    </tr>
    <tr>
        <td>
            <ce:Label runat="server">Order Index</ce:Label></td>
        <td>
            <dx:ASPxSpinEdit runat="server" ID="seOrderIndex" Property="SpecModel.OrderIndex"></dx:ASPxSpinEdit>
        </td>
    </tr>
</table>
