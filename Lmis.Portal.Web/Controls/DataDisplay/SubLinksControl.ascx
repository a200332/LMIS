﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubLinksControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataDisplay.SubLinksControl" %>
<table>
    <asp:Repeater runat="server" ID="rptItems">
        <ItemTemplate>
            <tr>
                <td>
                    <a class="link-img" href='<%# Eval("Url") %>' target="blanck">
                        <asp:Image Width="40px" Height="40px" runat="server" ImageUrl='<%# GetImageUrl(Eval("ID")) %>' />
                    </a>
                </td>
                <td>
                    <a class="book" href='<%# Eval("Url") %>' target="blanck">
                        <div style="vertical-align: middle;">
                            <div class="book-title">
                                <ce:Label runat="server" CssClass="link-title" Text='<%# Eval("Title") %>' />
                            </div>
                            <div class="book-description">
                                <ce:Label runat="server" Text='<%# Eval("Description") %>' />
                            </div>
                        </div>
                    </a>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>

