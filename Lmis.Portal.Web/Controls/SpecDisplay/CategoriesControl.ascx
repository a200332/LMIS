<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoriesControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.SpecDisplay.CategoriesControl" %>

<table id="spmenu">
    <asp:Repeater runat="server" ID="rptData">
        <ItemTemplate>
            <tr>
                <td class="spmenuBlock">
                    <asp:HyperLink runat="server" NavigateUrl='<%# GetSpecUrl(Eval("ID")) %>' CssClass="listenOnFocus">
                        <%# Eval("Title") %>
                    </asp:HyperLink>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
