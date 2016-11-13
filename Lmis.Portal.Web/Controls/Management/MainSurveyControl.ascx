<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MainSurveyControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.MainSurveyControl" %>
<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="SurveyModel.ID" />
<lmis:HiddenFieldValueControl runat="server" ID="hdParentID" Property="SurveyModel.ParentID" />

<table>
    <tr>
        <td>
            <ce:Label runat="server">Language</ce:Label></td>
        <td>
            <dx:ASPxComboBox runat="server" ID="cbxLanguage" Width="180" Property="SurveyModel.Language" TextField="Key" ValueField="Value" ValueType="System.String" />
        </td>
    </tr>
    <tr>
        <td>
            <ce:Label runat="server">Title</ce:Label></td>
        <td>
            <asp:TextBox runat="server" ID="tbxTitle" Property="SurveyModel.Title" /></td>
    </tr>
    <tr>
        <td>
            <ce:Label runat="server">Description</ce:Label></td>
        <td>
            <asp:TextBox runat="server" ID="tbxDescription" Property="SurveyModel.Description" /></td>
    </tr>
    <tr>
        <td>
            <ce:Label runat="server">Order Index</ce:Label></td>
        <td>
            <dx:ASPxSpinEdit runat="server" ID="seOrderIndex" Property="SurveyModel.OrderIndex"></dx:ASPxSpinEdit>
        </td>
    </tr>
</table>
