<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LegislationControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Others.LegislationControl" %>
<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="LegislationModel.ID" />

<table>
    <tr>
        <td>
            <ce:Label runat="server">Language</ce:Label></td>
        <td>
            <dx:ASPxComboBox runat="server" ID="cbxLanguage" Width="180" Property="LegislationModel.Language" TextField="Key" ValueField="Value" ValueType="System.String" />
        </td>
    </tr>
    <tr>
        <td>
            <ce:Label runat="server">Title</ce:Label></td>
        <td>
            <asp:TextBox runat="server" ID="tbxTitle" Property="LegislationModel.Title" /></td>
    </tr>
    <tr>
        <td>
            <ce:Label runat="server">Description</ce:Label></td>
        <td>
            <asp:TextBox runat="server" ID="tbxDescription" Property="LegislationModel.Description" /></td>
    </tr>
    <tr>
        <td>
            <ce:Label runat="server">File</ce:Label></td>
        <td>
            <asp:FileUpload runat="server" ID="fuFileData" Property="LegislationModel.FileData" /></td>
    </tr>
</table>
