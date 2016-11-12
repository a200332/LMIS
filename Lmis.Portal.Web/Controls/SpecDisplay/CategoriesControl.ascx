<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoriesControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.SpecDisplay.CategoriesControl" %>

<table>
    <asp:Repeater runat="server" ID="rptData">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:HyperLink runat="server" NavigateUrl='<%# GetSpecUrl(Eval("ID")) %>'><%# Eval("Title") %></asp:HyperLink>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
