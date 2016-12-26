<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CareersControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataDisplay.CareersControl" %>
<table>
    <asp:Repeater runat="server" ID="rptItems">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:HyperLink CssClass="link-img"  runat="server" NavigateUrl='<%# GetTargetUrl(Container.DataItem) %>' Target='<%# GetTarget(Container.DataItem) %>'>
                        <asp:Image Width="40px" Height="40px" runat="server" ImageUrl='<%# GetImageUrl(Eval("ID")) %>' />
                    </asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink CssClass="book"  runat="server" NavigateUrl='<%# GetTargetUrl(Container.DataItem) %>' Target='<%# GetTarget(Container.DataItem) %>'>
                        <div style="vertical-align: middle;">
                            <div class="book-title">
                                <ce:Label runat="server" CssClass="link-title" Text='<%# Eval("Title") %>' />
                            </div>
                            <div class="book-description">
                                <ce:Label runat="server" Text='<%# Eval("Description") %>' />
                            </div>
                        </div>
                    </asp:HyperLink>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>