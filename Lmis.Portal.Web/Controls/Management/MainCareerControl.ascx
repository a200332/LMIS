<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MainCareerControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.MainCareerControl" %>
<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="CareerModel.ID" />
<lmis:HiddenFieldValueControl runat="server" ID="hdParentID" Property="CareerModel.ParentID" />

<table>
    <tr>
        <td>
            <ce:Label runat="server">Language</ce:Label></td>
        <td>
            <dx:ASPxComboBox runat="server" ID="cbxLanguage" Width="180" Property="CareerModel.Language" TextField="Key" ValueField="Value" ValueType="System.String" />
        </td>
    </tr>
    <tr>
        <td>
            <ce:Label runat="server">Title</ce:Label></td>
        <td>
            <asp:TextBox runat="server" ID="tbxTitle" Property="CareerModel.Title" /></td>
    </tr>
    <tr>
        <td>
            <ce:Label runat="server">Order Index</ce:Label></td>
        <td>
            <dx:ASPxSpinEdit runat="server" ID="seOrderIndex" Property="CareerModel.OrderIndex"></dx:ASPxSpinEdit>
        </td>
    </tr>
    <tr>
        <td>
            <ce:Label runat="server">Description</ce:Label></td>
        <td>
            <asp:TextBox runat="server" ID="tbxDescription" Property="CareerModel.Description" /></td>
    </tr>
</table>