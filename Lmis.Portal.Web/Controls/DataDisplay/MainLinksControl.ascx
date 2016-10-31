<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MainLinksControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataDisplay.MainLinksControl" %>
<ul class="mainlink">
    <asp:Repeater runat="server" ID="rptItems">
        <ItemTemplate>
            <li>
                <asp:HyperLink NavigateUrl='<%# GetSubLinksUrl(Container.DataItem) %>' runat="server">
					<asp:Panel runat="server" style='<%# GetPanelStyle(Container.DataItem)%>'>
					    <ce:Label runat="server" Text='<%# Eval("Title") %>'/>
					</asp:Panel>
                </asp:HyperLink>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
