<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SpecDataControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.SpecDataControl" %>
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
    <tr>
        <td>
            <ce:Label runat="server" Text="Full Text" /></td>
        <td>
            <dx:ASPxHtmlEditor ID="heDescription" runat="server" ActiveView="Design" Width="400px" Height="300px" Property="SpecModel.FullText">
            </dx:ASPxHtmlEditor>
        </td>
    </tr>
</table>
