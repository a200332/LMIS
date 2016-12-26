<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SpecCategoryControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.SpecCategoryControl" %>

<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="SpecModel.ID" />
<lmis:HiddenFieldValueControl runat="server" ID="hdParentID" Property="SpecModel.ParentID" />
<div class="box">

    <ul>
        <li>
            <ce:Label runat="server">Language</ce:Label>
        </li>
        <li>
            <dx:ASPxComboBox runat="server" ID="cbxLanguage" Width="200" Property="SpecModel.Language" TextField="Key" ValueField="Value" ValueType="System.String" />
        </li>
    </ul>
    <ul>
        <li>
            <ce:Label runat="server">Title</ce:Label>
        </li>
        <li>
            <asp:TextBox runat="server" Width="200" ID="tbxTitle" Property="SpecModel.Title" />
        </li>
    </ul>
    <ul>
        <li>
            <ce:Label runat="server">Order Index</ce:Label>
        </li>
        <li>
            <dx:ASPxSpinEdit runat="server" ID="seOrderIndex" Width="200" Property="SpecModel.OrderIndex"></dx:ASPxSpinEdit>
        </li>
    </ul>

</div>
