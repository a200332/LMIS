<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExpressionsListControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Common.ExpressionsListControl" %>

<table>
    <tr>
        <td>Expression:</td>
        <td>
            <asp:TextBox runat="server" ID="txtExpression" Width="164px" />
        </td>
        <td>Type:</td>
        <td>
            <asp:DropDownList runat="server" ID="ddlType">
                <Items>
                    <asp:ListItem Text="Unspecified" Value="Unspecified" />
                    <asp:ListItem Text="Text" Value="Text" />
                    <asp:ListItem Text="Number" Value="Number" />
                    <asp:ListItem Text="DateTime" Value="DateTime" />
                </Items>
            </asp:DropDownList>
        </td>
        <td>
            <asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/App_Themes/default/images/add.png" text="Save" OnClick="btnSave_OnClick" />
            <asp:ImageButton runat="server" ID="btnDelete" ImageUrl="~/App_Themes/default/images/delete.png" text="Save" OnClick="btnDelete_OnClick" />
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <asp:ListBox ID="lstExpressions" Width="449" runat="server" DataTextField="Expression" DataValueField="Expression" AutoPostBack="True" OnSelectedIndexChanged="lstExpressions_OnSelectedIndexChanged"></asp:ListBox>
        </td>
    </tr>
</table>



