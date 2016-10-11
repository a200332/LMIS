﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExpressionControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Common.ExpressionControl" %>
<ul>
    <li><ce:Label runat="server">Expression:</ce:Label></li>
    <li>
        <asp:TextBox runat="server" ID="txtExpression" Property="ExpressionModel.Expression" Width="180" />
    </li>
</ul>
<ul>
    <li><ce:Label runat="server">Type:</ce:Label></li>
    <li>
        <asp:DropDownList runat="server" Width="180" ID="ddlType" Property="ExpressionModel.OutputType">
            <Items>
                <asp:ListItem Text="Unspecified" Value="Unspecified" />
                <asp:ListItem Text="Text" Value="Text" />
                <asp:ListItem Text="Number" Value="Number" />
                <asp:ListItem Text="DateTime" Value="DateTime" />
            </Items>
        </asp:DropDownList>
    </li>
</ul>

