<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ColumnControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.ColumnControl" %>
<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="ColumnModel.ID" />
<lmis:HiddenFieldValueControl runat="server" ID="hdTableID" Property="ColumnModel.TableID" />
<ul>
    <li><ce:Label runat="server">Name</ce:Label></li>
    <li>
        <asp:TextBox runat="server" ID="tbxName" Property="ColumnModel.Name"></asp:TextBox></li>
</ul>
<ul>
    <li><ce:Label runat="server">Type</ce:Label></li>
    <li>
        <asp:DropDownList runat="server" Width="195" ID="ddlType" Property="ColumnModel.Type">
            <Items>
                <asp:ListItem Text="String" Value="String" />
                <asp:ListItem Text="Integer" Value="Integer" />
                <asp:ListItem Text="Float" Value="Float" />
                <asp:ListItem Text="DateTime" Value="DateTime" />
            </Items>
        </asp:DropDownList></li>
</ul>
