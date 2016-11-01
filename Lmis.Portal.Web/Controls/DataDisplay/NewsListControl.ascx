<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsListControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataDisplay.NewsListControl" %>
<ul>
    <asp:Repeater runat="server" ID="rptItems">
        <ItemTemplate>
            <li>
                <asp:HyperLink runat="server" ID="lnkNews" NavigateUrl='<%# GetNewsUrl(Eval("ID")) %>'>
                <ul>
                    <li class="news-link-title"><%# Eval("Title") %></li>
                    <li class="news-link-date"><%# String.Format("{0:dd.MM.yyyy}", Eval("NewsDate")) %></li>
                    <li class="news-link-txt"><%# Eval("Description") %></li>
                </ul>
                </asp:HyperLink>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>

