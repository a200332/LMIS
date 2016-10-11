<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NamedExpressionControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Common.NamedExpressionControl" %>
<ul>
    <li>Name:</li>
    <li>            <asp:TextBox runat="server" ID="txtName" Property="NamedExpressionModel.Name" Width="180" />
</li>
</ul><ul>
    <li>Expression:</li>
    <li>            <asp:TextBox runat="server" ID="txtExpression" Property="NamedExpressionModel.Expression" Width="180" />
</li>
</ul><ul>
    <li>Type:</li>
    <li>   <asp:DropDownList runat="server" Width="180" ID="ddlType" Property="NamedExpressionModel.OutputType">
                <Items>
                    <asp:ListItem Text="Unspecified" Value="Unspecified" />
                    <asp:ListItem Text="Text" Value="Text" />
                    <asp:ListItem Text="Number" Value="Number" />
                    <asp:ListItem Text="DateTime" Value="DateTime" />
                </Items>
            </asp:DropDownList>          
</li>
</ul>
